using GuhaStore.Core.Entities;

namespace GuhaStore.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<Brand> Brands { get; }
    IRepository<Cart> Carts { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    IRepository<ProductReview> ProductReviews { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

