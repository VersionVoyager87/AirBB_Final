using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer;
using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});

// ✅ Database Context
builder.Services.AddDbContext<AirBBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AirBBContext")));

// ✅ Register Generic Repository for Dependency Injection
// The generic Repository<T> provides CRUD operations for all entity types
builder.Services.AddScoped(typeof(Repository<>));

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
