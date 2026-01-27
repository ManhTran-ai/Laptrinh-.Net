using Microsoft.AspNetCore.Mvc;
using GuhaStore.Core.Interfaces;
using GuhaStore.Core.Entities;
using GuhaStore.Web.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace GuhaStore.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartService _cartService;

    public AccountController(IUnitOfWork unitOfWork, ICartService cartService)
    {
        _unitOfWork = unitOfWork;
        _cartService = cartService;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(u => u.Username == model.Username);

        // Debug logging
        Console.WriteLine($"Login attempt: Username={model.Username}, Password={model.Password}, User found: {user != null}");

        // Auto-create admin account if doesn't exist
        if (user == null && model.Username == "tai" && model.Password == "12345678")
        {
            Console.WriteLine("Creating admin account...");
            user = new User
            {
                Username = "tai",
                Email = "tai@admin.com",
                PasswordHash = "12345678", // Plain text for simplicity
                FullName = "Administrator",
                Role = "admin",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            Console.WriteLine("Admin account created successfully");
        }

        if (user == null)
        {
            Console.WriteLine("User not found");
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            return View(model);
        }

        Console.WriteLine($"User found: {user.Username}, Role: {user.Role}, PasswordHash: {user.PasswordHash}");

        // Check password - handle both BCrypt hash and plain text
        bool passwordValid = false;
        try
        {
            // Try BCrypt verification first
            if (BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
                passwordValid = true;
                Console.WriteLine("BCrypt verification successful");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"BCrypt verification failed: {ex.Message}");
            // If BCrypt fails, try direct comparison (for plain text passwords)
            if (user.PasswordHash == model.Password)
            {
                passwordValid = true;
                Console.WriteLine("Direct comparison successful");
            }
        }

        // Special case for tai/12345678
        if (user.Username == "tai" && model.Password == "12345678")
        {
            passwordValid = true;
            Console.WriteLine("Special case for tai/12345678");
        }

        if (!passwordValid)
        {
            Console.WriteLine("Password validation failed");
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            return View(model);
        }

        Console.WriteLine("Authentication successful, redirecting...");

        if (!user.IsActive)
        {
            ModelState.AddModelError("", "Tài khoản đã bị khóa");
            return View(model);
        }

        // Set session
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("UserRole", user.Role);

        Console.WriteLine($"Session set: UserId={user.Id}, Username={user.Username}, Role={user.Role}");

        // Skip cart migration for now to avoid potential exceptions
        // await _cartService.MigrateSessionCartToUserAsync(user.Id, HttpContext.Session.Id);

        // Update cart count
        HttpContext.Session.SetInt32("CartCount", 0);

        TempData["SuccessMessage"] = $"Chào mừng trở lại, {user.Username}!";

        Console.WriteLine("About to redirect...");

        // Redirect based on role
        if (user.Role == "admin")
        {
            Console.WriteLine("Redirecting to Admin...");
            return RedirectToAction("Index", "Admin");
        }

        Console.WriteLine("Redirecting to Home...");
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Check existing user
        var existingUser = await _unitOfWork.Users.GetFirstOrDefaultAsync(u =>
            u.Username == model.Username || u.Email == model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc email đã tồn tại");
            return View(model);
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            FullName = model.Username,
            Phone = model.Phone,
            Role = "customer",
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Set session
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetInt32("CartCount", 0);

        TempData["SuccessMessage"] = "Đăng ký tài khoản thành công!";

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["SuccessMessage"] = "Đã đăng xuất thành công!";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult MyAccount()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> OrderHistory()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var orderService = HttpContext.RequestServices.GetRequiredService<IOrderService>();
        var orders = await orderService.GetUserOrdersAsync(userId.Value);
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> OrderDetail(int orderId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var orderService = HttpContext.RequestServices.GetRequiredService<IOrderService>();
        var order = await orderService.GetOrderByIdAsync(orderId);

        if (order == null || order.UserId != userId)
        {
            return NotFound();
        }

        return View(order);
    }

    private async Task MigrateSessionCartToUserAsync(int userId)
    {
        var sessionId = HttpContext.Session.Id;
        var allCarts = await _unitOfWork.Carts.GetAllAsync();
        var sessionCarts = allCarts.Where(c => c.SessionId == sessionId && c.UserId == null).ToList();

        foreach (var sessionCart in sessionCarts)
        {
            // Check if user already has this product in cart
            var existingCart = allCarts.FirstOrDefault(c =>
                c.UserId == userId && c.ProductId == sessionCart.ProductId);

            if (existingCart != null)
            {
                // Update quantity
                existingCart.Quantity += sessionCart.Quantity;
                await _unitOfWork.Carts.UpdateAsync(existingCart);
            }
            else
            {
                // Assign to user
                sessionCart.UserId = userId;
                sessionCart.SessionId = null;
                await _unitOfWork.Carts.UpdateAsync(sessionCart);
            }
        }

        // Remove session carts after migration
        foreach (var sessionCart in sessionCarts)
        {
            await _unitOfWork.Carts.DeleteAsync(sessionCart);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}

