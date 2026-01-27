namespace GuhaStore.Core.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

