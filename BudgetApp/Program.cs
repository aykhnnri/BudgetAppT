using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Areas.Identity.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBContextConnection") ?? throw new InvalidOperationException("Connection string 'DBContextConnection' not found.");

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString).EnableDetailedErrors());

builder.Services.AddDefaultIdentity<BudgetAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transactions}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();