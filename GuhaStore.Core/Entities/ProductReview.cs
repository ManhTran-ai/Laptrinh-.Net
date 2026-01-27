namespace GuhaStore.Core.Entities;

public class ProductReview
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; } // 1-5 stars
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public Product? Product { get; set; }
    public User? User { get; set; }
}
