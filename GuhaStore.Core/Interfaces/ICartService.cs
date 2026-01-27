using GuhaStore.Core.Entities;

namespace GuhaStore.Core.Interfaces;

public interface ICartService
{
    Task<IEnumerable<Cart>> GetUserCartAsync(int userId);
    Task<IEnumerable<Cart>> GetSessionCartAsync(string sessionId);
    Task<Cart?> GetCartItemAsync(int cartId);
    Task<Cart?> GetCartItemByProductAsync(int? userId, string? sessionId, int productId);
    Task AddToCartAsync(Cart cart);
    Task UpdateCartItemAsync(Cart cart);
    Task RemoveFromCartAsync(int cartId);
    Task ClearCartAsync(int? userId, string? sessionId);
    Task<int> GetCartItemCountAsync(int? userId, string? sessionId);
    Task<decimal> GetCartTotalAsync(int? userId, string? sessionId);
    Task<bool> ValidateCartAsync(int? userId, string? sessionId);
    Task MigrateSessionCartToUserAsync(int userId, string sessionId);
}