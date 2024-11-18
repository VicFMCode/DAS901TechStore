using Microsoft.EntityFrameworkCore;
using TechStoreInventory.Data;

var builder = WebApplication.CreateBuilder(args);
// Redirigir logs a la consola
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddDbContext<TechStoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TechStoreConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
