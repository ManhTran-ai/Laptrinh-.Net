using Microsoft.AspNetCore.Mvc;
using GuhaStore.Core.Interfaces;
using GuhaStore.Core.Entities;
using GuhaStore.Web.Models;
using Microsoft.AspNetCore.Http;

namespace GuhaStore.Web.Controllers;

public class CheckoutController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public CheckoutController(
        IOrderService orderService,
        ICartService cartService,
        IUnitOfWork unitOfWork,
        IEmailService emailService)
    {
        _orderService = orderService;
        _cartService = cartService;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Checkout" });
        }

        var sessionId = HttpContext.Session.Id;

        IEnumerable<Cart> cartItems;
        if (userId.HasValue)
        {
            cartItems = await _cartService.GetUserCartAsync(userId.Value);
        }
        else
        {
            cartItems = await _cartService.GetSessionCartAsync(sessionId);
        }

        if (!cartItems.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        var totalAmount = await _cartService.GetCartTotalAsync(userId, sessionId);

        var model = new CheckoutViewModel
        {
            CartItems = cartItems.ToDictionary(
                c => c.Id.ToString(),
                c => new Dictionary<string, object>
                {
                    ["ProductId"] = c.ProductId,
                    ["ProductName"] = c.ProductName,
                    ["ProductImage"] = c.ProductImage,
                    ["Quantity"] = c.Quantity,
                    ["Price"] = c.ProductPrice,
                    ["Total"] = c.Quantity * c.ProductPrice
                }),
            TotalAmount = totalAmount
        };

        // Load user info if exists
        var user = await _unitOfWork.Users.GetByIdAsync(userId.Value);
        if (user != null)
        {
            model.DeliveryName = user.FullName ?? string.Empty;
            model.DeliveryPhone = user.Phone ?? string.Empty;
            // Note: User entity doesn't have address field in simplified schema
            // You might want to add it if needed
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessOrder(CheckoutViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var sessionId = HttpContext.Session.Id;

        if (!ModelState.IsValid)
        {
            var cartItems = userId.HasValue
                ? await _cartService.GetUserCartAsync(userId.Value)
                : await _cartService.GetSessionCartAsync(sessionId);
            model.CartItems = cartItems.ToDictionary(
                c => c.Id.ToString(),
                c => new Dictionary<string, object>
                {
                    ["ProductId"] = c.ProductId,
                    ["ProductName"] = c.ProductName,
                    ["ProductImage"] = c.ProductImage,
                    ["Quantity"] = c.Quantity,
                    ["Price"] = c.ProductPrice,
                    ["Total"] = c.Quantity * c.ProductPrice
                });
            model.TotalAmount = await _cartService.GetCartTotalAsync(userId, sessionId);
            return View("Index", model);
        }

        // Validate cart inventory
        var isValid = await _orderService.ValidateCartInventoryAsync(userId, sessionId);
        if (!isValid)
        {
            ModelState.AddModelError("", "Một số sản phẩm trong giỏ hàng không còn đủ số lượng");
            var cartItems = userId.HasValue
                ? await _cartService.GetUserCartAsync(userId.Value)
                : await _cartService.GetSessionCartAsync(sessionId);
            model.CartItems = cartItems.ToDictionary(
                c => c.Id.ToString(),
                c => new Dictionary<string, object>
                {
                    ["ProductId"] = c.ProductId,
                    ["ProductName"] = c.ProductName,
                    ["ProductImage"] = c.ProductImage,
                    ["Quantity"] = c.Quantity,
                    ["Price"] = c.ProductPrice,
                    ["Total"] = c.Quantity * c.ProductPrice
                });
            model.TotalAmount = await _cartService.GetCartTotalAsync(userId, sessionId);
            return View("Index", model);
        }

        try
        {
            // Create order
            var order = new Order
            {
                UserId = userId.Value,
                TotalAmount = model.TotalAmount,
                Status = "pending",
                ShippingAddress = model.DeliveryAddress,
                Phone = model.DeliveryPhone,
                Notes = model.DeliveryNote,
                CreatedAt = DateTime.Now
            };

            var cartItems = userId.HasValue
                ? await _cartService.GetUserCartAsync(userId.Value)
                : await _cartService.GetSessionCartAsync(sessionId);

            // Create order with cart items
            await _orderService.CreateOrderAsync(order, cartItems.ToList());

            // Clear cart
            await _cartService.ClearCartAsync(userId, sessionId);

            // Note: Email service is disabled for educational project
            // await _emailService.SendOrderConfirmationAsync(order);

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Có lỗi xảy ra khi xử lý đơn hàng. Vui lòng thử lại.");
            var errorCartItems = userId.HasValue
                ? await _cartService.GetUserCartAsync(userId.Value)
                : await _cartService.GetSessionCartAsync(sessionId);
            model.CartItems = errorCartItems.ToDictionary(
                c => c.Id.ToString(),
                c => new Dictionary<string, object>
                {
                    ["ProductId"] = c.ProductId,
                    ["ProductName"] = c.ProductName,
                    ["ProductImage"] = c.ProductImage,
                    ["Quantity"] = c.Quantity,
                    ["Price"] = c.ProductPrice,
                    ["Total"] = c.Quantity * c.ProductPrice
                });
            model.TotalAmount = await _cartService.GetCartTotalAsync(userId, sessionId);
            return View("Index", model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> OrderConfirmation(int orderId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null || order.UserId != userId)
        {
            return NotFound();
        }

        return View(order);
    }
}

