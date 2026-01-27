using Microsoft.EntityFrameworkCore;
using GuhaStore.Core.Entities;
using GuhaStore.Core.Interfaces;
using GuhaStore.Infrastructure.Data;

namespace GuhaStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;

    public ProductService(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.ProductReviews)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.BrandId == brandId && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<Product>();

        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.IsActive &&
                (p.Name.Contains(query) ||
                 p.Description.Contains(query)))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.Id) // Order by newest as we don't have sales tracking
            .Take(8)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
            return new List<Product>();

        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.Id != productId &&
                p.IsActive &&
                (p.CategoryId == product.CategoryId ||
                 p.BrandId == product.BrandId))
            .Take(4)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetActiveProductsByCategoryAsync(int categoryId)
    {
        return await GetProductsByCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Product>> GetActiveProductsByBrandAsync(int brandId)
    {
        return await GetProductsByBrandAsync(brandId);
    }
}

