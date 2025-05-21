using Uni.DAL.Entity;
using Uni.DAL.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Uni.DAL.Repo.Abstraction;
using Uni.DAL.Repo.Impelementation;
using Uni.BLL.Service.Abstraction;
using Uni.BLL.Service.Impelementation;
using Uni.BLL.Mapping;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Uni.BLL.Service.Implementation;

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


builder.Services.AddIdentityCore<Student>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Student>>(TokenOptions.DefaultProvider);

builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddAutoMapper(typeof(DomainProfile));
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
builder.Services.AddScoped<IFastApiService, FastApiService>();
builder.Services.AddHttpClient<FastApiService>();
builder.Services.AddHttpClient("FastAPI", client =>
{
	client.BaseAddress = new Uri("http://localhost:8000");
});



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = new[] { "Admin", "Student" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),                    // ✅ dynamic
                Name = role,
                NormalizedName = role.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()       // ✅ dynamic
            });
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
