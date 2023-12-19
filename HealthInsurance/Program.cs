using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HealthInsurance.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Nethereum.Web3;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HealthInsuranceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HealthInsuranceContext") ?? throw new InvalidOperationException("Connection string 'HealthInsuranceContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
