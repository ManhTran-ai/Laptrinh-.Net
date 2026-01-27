using Microsoft.EntityFrameworkCore.Storage;
using GuhaStore.Core.Entities;
using GuhaStore.Core.Interfaces;
using GuhaStore.Infrastructure.Data;
using GuhaStore.Infrastructure.Repositories;

namespace GuhaStore.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    private IRepository<User>? _users;
    private IRepository<Product>? _products;
    private IRepository<Category>? _categories;
    private IRepository<Brand>? _brands;
    private IRepository<Cart>? _carts;
    private IRepository<Order>? _orders;
    private IRepository<OrderItem>? _orderItems;
    private IRepository<ProductReview>? _productReviews;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<User> Users => _users ??= new Repository<User>(_context);
    public IRepository<Product> Products => _products ??= new Repository<Product>(_context);
    public IRepository<Category> Categories => _categories ??= new Repository<Category>(_context);
    public IRepository<Brand> Brands => _brands ??= new Repository<Brand>(_context);
    public IRepository<Cart> Carts => _carts ??= new Repository<Cart>(_context);
    public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);
    public IRepository<OrderItem> OrderItems => _orderItems ??= new Repository<OrderItem>(_context);
    public IRepository<ProductReview> ProductReviews => _productReviews ??= new Repository<ProductReview>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}

