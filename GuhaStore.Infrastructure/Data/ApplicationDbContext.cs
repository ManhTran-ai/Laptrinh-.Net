using Microsoft.EntityFrameworkCore;
using GuhaStore.Core.Entities;

namespace GuhaStore.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
            entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100);
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
            entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(20).HasDefaultValue("customer");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        });

        // Category entity configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        // Brand entity configuration
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("brands");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        // Product entity configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(255);
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity").HasDefaultValue(0);
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Order entity configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderCode).HasColumnName("order_code").HasMaxLength(20).IsRequired();
            entity.HasIndex(e => e.OrderCode).IsUnique();
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(10,2)");
            entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("pending");
            entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address").HasColumnType("text");
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
            entity.Property(e => e.Notes).HasColumnName("notes").HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderItem entity configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("order_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
            entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(10,2)");
            entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(10,2)");

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Article entity removed

        // ProductReview entity configuration
        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.ToTable("product_reviews");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Rating).HasColumnName("rating").IsRequired();
            entity.Property(e => e.Comment).HasColumnName("comment").HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.ToTable(t => t.HasCheckConstraint("CK_ProductReview_Rating", "rating >= 1 AND rating <= 5"));

            entity.HasOne(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pr => pr.User)
                .WithMany(u => u.ProductReviews)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.ProductId, e.UserId }).IsUnique();
        });

        // Cart entity configuration
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("carts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id").HasMaxLength(255);
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductName).HasColumnName("product_name").HasMaxLength(200).IsRequired();
            entity.Property(e => e.ProductPrice).HasColumnName("product_price").HasColumnType("decimal(10,2)");
            entity.Property(e => e.ProductImage).HasColumnName("product_image").HasMaxLength(255);
            entity.Property(e => e.Quantity).HasColumnName("quantity").HasDefaultValue(1);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

