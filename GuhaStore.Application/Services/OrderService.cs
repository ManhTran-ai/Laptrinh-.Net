using GuhaStore.Core.Entities;
using GuhaStore.Core.Interfaces;
using GuhaStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GuhaStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;

    public OrderService(IUnitOfWork unitOfWork, ApplicationDbContext context, ICartService cartService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _cartService = cartService;
    }

    public async Task<Order> CreateOrderAsync(Order order, List<Cart> cartItems)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Generate order code if not set
            if (string.IsNullOrEmpty(order.OrderCode))
            {
                order.OrderCode = await GenerateOrderCodeAsync();
            }

            // Set order date
            order.CreatedAt = DateTime.Now;

            // Add order
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // Add order items and update inventory
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.ProductPrice,
                    Total = cartItem.Quantity * cartItem.ProductPrice
                };

                await _unitOfWork.OrderItems.AddAsync(orderItem);

                // Update product stock quantity
                var product = await _context.Products.FindAsync(orderItem.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= orderItem.Quantity;
                    await _unitOfWork.SaveChangesAsync();
                }

            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return order;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<Order?> GetOrderByCodeAsync(string orderCode)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderCode == orderCode);
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidateCartInventoryAsync(int? userId, string? sessionId)
    {
        return await _cartService.ValidateCartAsync(userId, sessionId);
    }

    public async Task<string> GenerateOrderCodeAsync()
    {
        var random = new Random();
        string orderCode;
        bool exists;

        do
        {
            orderCode = "ORD" + random.Next(100000, 999999).ToString();
            exists = await _context.Orders.AnyAsync(o => o.OrderCode == orderCode);
        } while (exists);

        return orderCode;
    }
}

