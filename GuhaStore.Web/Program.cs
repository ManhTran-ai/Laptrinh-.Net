using GuhaStore.Core.Interfaces;
using GuhaStore.Infrastructure.Data;
using GuhaStore.Infrastructure.Repositories;
using GuhaStore.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Persist DataProtection keys to disk so session cookies remain valid across restarts
// (helps avoid 'Error unprotecting the session cookie' exceptions during development)
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "DataProtection-Keys")))
    .SetApplicationName("GuhaStore");

// Configure MVC
builder.Services.AddRazorPages();

// Add DbContext with MySQL (Pomelo)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Register Application Services
builder.Services.AddScoped<IFileUploadService>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    return new FileUploadService(env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
});
builder.Services.AddScoped<IEmailService, EmailService>();

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add Session middleware
app.UseSession();

// Set Path Base for /GuhaStore
// app.UsePathBase("/GuhaStore");

app.UseRouting();

// Add custom error handling middleware
// app.UseMiddleware<GuhaStore.Web.Middleware.DatabaseErrorHandlerMiddleware>();

// Add Authorization
app.UseAuthorization();

// Custom routes for shorter URLs
app.MapControllerRoute(
    name: "products",
    pattern: "products",
    defaults: new { controller = "Products", action = "Index" });

app.MapControllerRoute(
    name: "cart",
    pattern: "cart",
    defaults: new { controller = "Cart", action = "Index" });

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Test minimal API endpoint
app.MapGet("/test", () => "Hello from minimal API!");

app.Run();
