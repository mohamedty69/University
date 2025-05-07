using Uni.DAL.Entity;
using Uni.DAL.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Uni.DAL.Repo.Abstraction;
using Uni.DAL.Repo.Impelementation;
using Uni.BLL.Service.Abstraction;
using Uni.BLL.Service.Impelementation;
using Uni.BLL.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext FIRST (before Identity)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity (simplified - you had duplicate registrations)
builder.Services.AddIdentity<Student, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(typeof(DomainProfile));
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


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

// Add these middleware in correct order
app.UseAuthentication(); // You were missing this!
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

//using System;
//using Uni.DAL.Entity;
//using Uni.DAL.DB;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();
//builder.Services.AddDbContext<AppDbContext>(options =>
//               options.UseSqlServer("name=DefaultConnection"));

//builder.Services.AddIdentity<Student, IdentityRole>()
//                .AddEntityFrameworkStores<AppDbContext>()
//                .AddDefaultTokenProviders();



//builder.Services.AddIdentityCore<Student>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddRoles<IdentityRole>()
//                .AddEntityFrameworkStores<AppDbContext>()
//                .AddTokenProvider<DataProtectorTokenProvider<Student>>(TokenOptions.DefaultProvider);


//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
