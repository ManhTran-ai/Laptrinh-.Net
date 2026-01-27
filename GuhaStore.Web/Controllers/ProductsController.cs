using GuhaStore.Application.Services;
using GuhaStore.Core.Entities;
using GuhaStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GuhaStore.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IProductService productService, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index(int? categoryId, int? brandId, string? searchTerm, int page = 1, int pageSize = 12, string sortBy = "newest", int? minPrice = null, int? maxPrice = null, bool onSaleOnly = false)
    {
        ViewBag.CategoryId = categoryId;
        ViewBag.BrandId = brandId;
        ViewBag.SearchTerm = searchTerm;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.SortBy = sortBy;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.OnSaleOnly = onSaleOnly;

        IEnumerable<Product> products;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            products = await _productService.SearchProductsAsync(searchTerm);
        }
        else if (categoryId.HasValue)
        {
            products = await _productService.GetProductsByCategoryAsync(categoryId.Value);
        }
        else if (brandId.HasValue)
        {
            products = await _productService.GetProductsByBrandAsync(brandId.Value);
        }
        else
        {
            products = await _productService.GetActiveProductsAsync();
        }

        if (minPrice.HasValue)
            products = products.Where(p => (p.SalePrice ?? p.Price) >= minPrice.Value);
        if (maxPrice.HasValue)
            products = products.Where(p => (p.SalePrice ?? p.Price) <= maxPrice.Value);
        if (onSaleOnly)
            products = products.Where(p => p.SalePrice.HasValue && p.SalePrice < p.Price);

        // Apply sorting
        products = sortBy switch
        {
            "price-asc" => products.OrderBy(p => p.SalePrice ?? p.Price),
            "price-desc" => products.OrderByDescending(p => p.SalePrice ?? p.Price),
            "name" => products.OrderBy(p => p.Name),
            "sales" => products.OrderByDescending(p => p.Id), // Simplified: sort by newest instead of sales
            _ => products.OrderByDescending(p => p.Id)
        };

        var totalCount = products.Count();
        products = products.Skip((page - 1) * pageSize).Take(pageSize);

        ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ViewBag.TotalCount = totalCount;

        // Load categories and brands for filter
        ViewBag.Categories = await _unitOfWork.Categories.GetAllAsync();
        ViewBag.Brands = await _unitOfWork.Brands.GetAllAsync();

        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        // Load related products
        if (product.CategoryId.HasValue)
        {
            var relatedProducts = await _productService.GetProductsByCategoryAsync(product.CategoryId.Value);
            ViewBag.RelatedProducts = relatedProducts.Where(p => p.Id != id && p.IsActive).Take(4);
        }
        else
        {
            ViewBag.RelatedProducts = new List<Product>();
        }

        // Load product reviews
        var reviews = await _unitOfWork.ProductReviews.GetAllAsync();
        ViewBag.Reviews = reviews.Where(r => r.ProductId == id).OrderByDescending(r => r.CreatedAt).ToList();

        return View(product);
    }

    [HttpGet]
    public IActionResult Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index), new { searchTerm = term });
    }
}

