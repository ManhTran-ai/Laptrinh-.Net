namespace GuhaStore.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "pending"; // 'pending', 'processing', 'shipped', 'delivered', 'cancelled'
    public string? ShippingAddress { get; set; }
    public string? Phone { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public User? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

