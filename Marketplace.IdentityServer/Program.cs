using Marketplace.IdentityServer;
using Marketplace.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(opt => 
{
    opt.UseInMemoryDatabase("Memory");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "identity_cookie";
    opt.LoginPath = "/Auth/Login";
});

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetService<UserManager<IdentityUser>>();

    var user = new IdentityUser("test@marketplace.com");
    var result = await userManager!.CreateAsync(user, "Pa$$w0rd");
}

app.UseStaticFiles();

app.UseIdentityServer();

app.MapControllerRoute("LoginRoute", "{controller=Auth}/{action=Login}");

app.Run();
