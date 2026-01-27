namespace GuhaStore.Core.Entities;

public class Cart
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? SessionId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public string? ProductImage { get; set; }
    public int Quantity { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public User? User { get; set; }
    public Product? Product { get; set; }
}
