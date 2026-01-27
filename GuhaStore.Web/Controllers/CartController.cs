using Microsoft.AspNetCore.Mvc;
using GuhaStore.Core.Interfaces;
using GuhaStore.Core.Entities;

namespace GuhaStore.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public CartController(ICartService cartService, IProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
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

        var total = await _cartService.GetCartTotalAsync(userId, sessionId);

        ViewBag.CartTotal = total;
        ViewBag.CartItems = cartItems;

        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        if (quantity <= 0)
        {
            TempData["ErrorMessage"] = "Số lượng không hợp lệ.";
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        var userId = HttpContext.Session.GetInt32("UserId");
        var sessionId = HttpContext.Session.Id;

        // Get product details
        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null || !product.IsActive)
        {
            TempData["ErrorMessage"] = "Sản phẩm không tồn tại.";
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        if (product.StockQuantity < quantity)
        {
            TempData["ErrorMessage"] = "Không đủ hàng trong kho.";
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        // Check if item already exists in cart
        var existingCart = await _cartService.GetCartItemByProductAsync(userId, sessionId, productId);

        if (existingCart != null)
        {
            // Update quantity
            existingCart.Quantity += quantity;
            if (existingCart.Quantity > product.StockQuantity)
            {
                TempData["ErrorMessage"] = "Không đủ hàng trong kho.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }
            await _cartService.UpdateCartItemAsync(existingCart);
        }
        else
        {
            // Create new cart item
            var cart = new Cart
            {
                UserId = userId,
                SessionId = userId.HasValue ? null : sessionId,
                ProductId = productId,
                ProductName = product.Name,
                ProductPrice = product.SalePrice ?? product.Price,
                ProductImage = product.ImageUrl,
                Quantity = quantity
            };
            await _cartService.AddToCartAsync(cart);
        }

        // Update cart count in session for display
        var cartCount = await _cartService.GetCartItemCountAsync(userId, sessionId);
        HttpContext.Session.SetInt32("CartCount", cartCount);

        TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng.";

        return RedirectToAction("Details", "Products", new { id = productId });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
    {
        if (quantity <= 0)
        {
            TempData["ErrorMessage"] = "Số lượng không hợp lệ.";
            return RedirectToAction(nameof(Index));
        }

        var userId = HttpContext.Session.GetInt32("UserId");
        var sessionId = HttpContext.Session.Id;

        var cartItem = await _cartService.GetCartItemAsync(cartId);
        if (cartItem == null)
        {
            TempData["ErrorMessage"] = "Sản phẩm không tồn tại trong giỏ hàng.";
            return RedirectToAction(nameof(Index));
        }

        // Check if user owns this cart item
        if ((userId.HasValue && cartItem.UserId != userId) ||
            (!userId.HasValue && cartItem.SessionId != sessionId))
        {
            TempData["ErrorMessage"] = "Không có quyền truy cập.";
            return RedirectToAction(nameof(Index));
        }

        // Check stock
        var product = await _productService.GetProductByIdAsync(cartItem.ProductId);
        if (product == null || quantity > product.StockQuantity)
        {
            TempData["ErrorMessage"] = "Không đủ hàng trong kho.";
            return RedirectToAction(nameof(Index));
        }

        cartItem.Quantity = quantity;
        await _cartService.UpdateCartItemAsync(cartItem);

        // Update cart count in session
        var cartCount = await _cartService.GetCartItemCountAsync(userId, sessionId);
        HttpContext.Session.SetInt32("CartCount", cartCount);

        TempData["SuccessMessage"] = "Cập nhật số lượng thành công.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveItem(int cartId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var sessionId = HttpContext.Session.Id;

        var cartItem = await _cartService.GetCartItemAsync(cartId);
        if (cartItem == null)
        {
            TempData["ErrorMessage"] = "Sản phẩm không tồn tại trong giỏ hàng.";
            return RedirectToAction(nameof(Index));
        }

        // Check if user owns this cart item
        if ((userId.HasValue && cartItem.UserId != userId) ||
            (!userId.HasValue && cartItem.SessionId != sessionId))
        {
            TempData["ErrorMessage"] = "Không có quyền truy cập.";
            return RedirectToAction(nameof(Index));
        }

        await _cartService.RemoveFromCartAsync(cartId);

        // Update cart count in session
        var cartCount = await _cartService.GetCartItemCountAsync(userId, sessionId);
        HttpContext.Session.SetInt32("CartCount", cartCount);

        TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var sessionId = HttpContext.Session.Id;

        await _cartService.ClearCartAsync(userId, sessionId);

        // Update cart count in session
        HttpContext.Session.SetInt32("CartCount", 0);

        TempData["SuccessMessage"] = "Đã xóa tất cả sản phẩm khỏi giỏ hàng.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetCartCount()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var sessionId = HttpContext.Session.Id;

        var count = await _cartService.GetCartItemCountAsync(userId, sessionId);
        return Json(new { count });
    }
}

