using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WIS___PI___v2.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WIS___PI___v2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WIS___PI___v2Context") ?? throw new InvalidOperationException("Connection string 'WIS___PI___v2Context' not found.")));

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
