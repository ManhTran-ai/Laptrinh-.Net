using Microsoft.AspNetCore.Mvc;
using GuhaStore.Core.Interfaces;

namespace GuhaStore.Web.Controllers;

public class AdminController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

        public async Task<IActionResult> Index()
        {
            // Get dashboard statistics
        var totalProducts = (await _unitOfWork.Products.GetAllAsync()).Count();
        var totalUsers = (await _unitOfWork.Users.GetAllAsync()).Count();
        var totalOrders = (await _unitOfWork.Orders.GetAllAsync()).Count();

        ViewBag.TotalProducts = totalProducts;
        ViewBag.TotalUsers = totalUsers;
        ViewBag.TotalOrders = totalOrders;

        return View();
    }

    public IActionResult Test()
    {
        return Content("Admin Test Route Working!");
    }

        // PRODUCTS CRUD for admin
        public async Task<IActionResult> Products()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userId == null || userRole != "admin") return RedirectToAction("Login", "Account");

            var products = (await _unitOfWork.Products.GetAllAsync()).ToList();
            // load categories and brands to display names in admin list
            var categories = (await _unitOfWork.Categories.GetAllAsync()).ToDictionary(c => c.Id);
            var brands = (await _unitOfWork.Brands.GetAllAsync()).ToDictionary(b => b.Id);

            foreach (var p in products)
            {
                if (p.CategoryId.HasValue && categories.TryGetValue(p.CategoryId.Value, out var cat))
                {
                    p.Category = cat;
                }
                if (p.BrandId.HasValue && brands.TryGetValue(p.BrandId.Value, out var br))
                {
                    p.Brand = br;
                }
            }

            // Use explicit view path to match Views/Admin/Products/Index.cshtml
            return View("~/Views/Admin/Products/Index.cshtml", products.OrderByDescending(p => p.CreatedAt));
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin") return RedirectToAction("Login", "Account");
            // provide categories and brands for selects
            var categories = _unitOfWork.Categories.GetAllAsync().Result;
            var brands = _unitOfWork.Brands.GetAllAsync().Result;
            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            return View("~/Views/Admin/Products/CreateProduct.cshtml", new GuhaStore.Core.Entities.Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(GuhaStore.Core.Entities.Product product, IFormFile? imageFile)
        {
            // server-side validation
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                ModelState.AddModelError("Name", "Tên sản phẩm là bắt buộc.");
            }
            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Giá phải lớn hơn 0.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _unitOfWork.Categories.GetAllAsync();
                ViewBag.Brands = await _unitOfWork.Brands.GetAllAsync();
                return View(product);
            }

            // handle image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.ImageUrl = "~/uploads/" + fileName;
            }

            product.CreatedAt = DateTime.Now;
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            TempData["SuccessMessage"] = "Tạo sản phẩm thành công.";
            return RedirectToAction(nameof(Products));
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return NotFound();
            ViewBag.Categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.Brands = await _unitOfWork.Brands.GetAllAsync();
            return View("~/Views/Admin/Products/EditProduct.cshtml", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, GuhaStore.Core.Entities.Product product, IFormFile? imageFile)
        {
            if (id != product.Id) return NotFound();
            // server validation
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                ModelState.AddModelError("Name", "Tên sản phẩm là bắt buộc.");
            }
            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Giá phải lớn hơn 0.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _unitOfWork.Categories.GetAllAsync();
                ViewBag.Brands = await _unitOfWork.Brands.GetAllAsync();
                // preserve existing image when returning to view
                var existingProdForReturn = await _unitOfWork.Products.GetByIdAsync(id);
                if (existingProdForReturn != null && string.IsNullOrEmpty(product.ImageUrl))
                {
                    product.ImageUrl = existingProdForReturn.ImageUrl;
                }
                return View(product);
            }

            // fetch existing product and apply updates to avoid overwriting ImageUrl when no new file is provided
            var existing = await _unitOfWork.Products.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // handle image upload - replace only if a new file was provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await imageFile.CopyToAsync(stream);
                }
                existing.ImageUrl = "~/uploads/" + fileName;
            }

            // update scalar fields
            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.SalePrice = product.SalePrice;
            existing.CategoryId = product.CategoryId;
            existing.BrandId = product.BrandId;
            existing.StockQuantity = product.StockQuantity;
            existing.IsActive = product.IsActive;

            await _unitOfWork.Products.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công.";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin") return RedirectToAction("Login", "Account");
            var prod = await _unitOfWork.Products.GetByIdAsync(id);
            if (prod != null)
            {
                await _unitOfWork.Products.DeleteAsync(prod);
            }
            await _unitOfWork.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa sản phẩm thành công.";
            return RedirectToAction(nameof(Products));
        }

        // ORDERS management
        public async Task<IActionResult> Orders()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return View("~/Views/Admin/Orders/Index.cshtml", orders.OrderByDescending(o => o.CreatedAt));
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View("~/Views/Admin/Orders/OrderDetails.cshtml", order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin") return RedirectToAction("Login", "Account");
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null) return NotFound();
            order.Status = status;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công.";
            return RedirectToAction(nameof(Orders));
        }

        
}
