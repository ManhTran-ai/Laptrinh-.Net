using GuhaStore.Core.Entities;

namespace GuhaStore.Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order, List<Cart> cartItems);
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order?> GetOrderByCodeAsync(string orderCode);
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
    Task UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> ValidateCartInventoryAsync(int? userId, string? sessionId);
    Task<string> GenerateOrderCodeAsync();
}

