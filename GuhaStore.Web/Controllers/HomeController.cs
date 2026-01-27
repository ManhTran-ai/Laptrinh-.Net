using System.Diagnostics;
using GuhaStore.Application.Services;
using GuhaStore.Core.Entities;
using GuhaStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GuhaStore.Web.Models;

namespace GuhaStore.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(
        ILogger<HomeController> logger,
        IProductService productService,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        // Load featured/new products
        var products = await _productService.GetActiveProductsAsync();
        ViewData["NewProducts"] = products.OrderByDescending(p => p.Id).Take(8).ToList();
        ViewData["BestSellers"] = products.OrderByDescending(p => p.Id).Take(8).ToList(); // Simplified: use newest as bestsellers
        ViewData["SaleProducts"] = products.Where(p => p.SalePrice.HasValue && p.SalePrice < p.Price).OrderByDescending(p => p.SalePrice).Take(8).ToList();
        ViewData["FeaturedProducts"] = products.OrderByDescending(p => p.Id).Take(8).ToList();

        // Load categories
        var categories = await _unitOfWork.Categories.GetAllAsync();
        ViewData["Categories"] = categories.ToList();

        // Load brands
        ViewData["Brands"] = await _unitOfWork.Brands.GetAllAsync();


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult TestRoute()
    {
        return Content("Test route is working!");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
