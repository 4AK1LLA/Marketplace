using Marketplace.IdentityServer;
using Marketplace.IdentityServer.Data;
using Marketplace.IdentityServer.Interfaces;
using Marketplace.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEmailAddressValidator, EmailAddressValidator>();

builder.Services.AddDbContext<IdentityContext>(opt => 
{
    opt.UseInMemoryDatabase("Memory");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "identity_cookie";
    opt.LoginPath = "/Auth/Login";
    opt.LogoutPath = "/Auth/Logout";
});

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.GetApiScopes())
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
