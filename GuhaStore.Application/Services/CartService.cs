using Microsoft.EntityFrameworkCore;
using GuhaStore.Core.Entities;
using GuhaStore.Core.Interfaces;
using GuhaStore.Infrastructure.Data;

namespace GuhaStore.Application.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;

    public CartService(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<IEnumerable<Cart>> GetUserCartAsync(int userId)
    {
        return await _context.Carts
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Cart>> GetSessionCartAsync(string sessionId)
    {
        return await _context.Carts
            .Where(c => c.SessionId == sessionId && c.UserId == null)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Cart?> GetCartItemAsync(int cartId)
    {
        return await _context.Carts.FindAsync(cartId);
    }

    public async Task<Cart?> GetCartItemByProductAsync(int? userId, string? sessionId, int productId)
    {
        return await _context.Carts
            .FirstOrDefaultAsync(c =>
                ((userId.HasValue && c.UserId == userId) ||
                 (sessionId != null && c.SessionId == sessionId && c.UserId == null)) &&
                c.ProductId == productId);
    }

    public async Task AddToCartAsync(Cart cart)
    {
        cart.CreatedAt = DateTime.Now;
        cart.UpdatedAt = DateTime.Now;
        await _unitOfWork.Carts.AddAsync(cart);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(Cart cart)
    {
        cart.UpdatedAt = DateTime.Now;
        await _unitOfWork.Carts.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int cartId)
    {
        var cartItem = await _unitOfWork.Carts.GetByIdAsync(cartId);
        if (cartItem != null)
        {
            await _unitOfWork.Carts.DeleteAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync(int? userId, string? sessionId)
    {
        var cartItems = await _context.Carts
            .Where(c =>
                (userId.HasValue && c.UserId == userId) ||
                (sessionId != null && c.SessionId == sessionId && c.UserId == null))
            .ToListAsync();

        foreach (var item in cartItems)
        {
            await _unitOfWork.Carts.DeleteAsync(item);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> GetCartItemCountAsync(int? userId, string? sessionId)
    {
        return await _context.Carts
            .Where(c =>
                (userId.HasValue && c.UserId == userId) ||
                (sessionId != null && c.SessionId == sessionId && c.UserId == null))
            .SumAsync(c => c.Quantity);
    }

    public async Task<decimal> GetCartTotalAsync(int? userId, string? sessionId)
    {
        return await _context.Carts
            .Where(c =>
                (userId.HasValue && c.UserId == userId) ||
                (sessionId != null && c.SessionId == sessionId && c.UserId == null))
            .SumAsync(c => c.Quantity * c.ProductPrice);
    }

    public async Task<bool> ValidateCartAsync(int? userId, string? sessionId)
    {
        var cartItems = await _context.Carts
            .Include(c => c.Product)
            .Where(c =>
                (userId.HasValue && c.UserId == userId) ||
                (sessionId != null && c.SessionId == sessionId && c.UserId == null))
            .ToListAsync();

        foreach (var item in cartItems)
        {
            // Check if product exists and is active
            if (item.Product == null || !item.Product.IsActive)
            {
                return false;
            }

            // Check if there's sufficient stock
            if (item.Quantity > item.Product.StockQuantity)
            {
                return false;
            }
        }

        return true;
    }

    public async Task MigrateSessionCartToUserAsync(int userId, string sessionId)
    {
        var sessionCartItems = await GetSessionCartAsync(sessionId);

        foreach (var sessionItem in sessionCartItems)
        {
            // Check if user already has this product in cart
            var existingItem = await GetCartItemByProductAsync(userId, null, sessionItem.ProductId);

            if (existingItem != null)
            {
                // Update quantity
                existingItem.Quantity += sessionItem.Quantity;
                await UpdateCartItemAsync(existingItem);
            }
            else
            {
                // Assign to user
                sessionItem.UserId = userId;
                sessionItem.SessionId = null;
                await UpdateCartItemAsync(sessionItem);
            }
        }

        // Remove all session cart items after migration
        foreach (var sessionItem in sessionCartItems)
        {
            await RemoveFromCartAsync(sessionItem.Id);
        }
    }
}