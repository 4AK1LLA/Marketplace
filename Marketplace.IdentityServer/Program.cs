using Marketplace.IdentityServer;
using Marketplace.IdentityServer.Data;
using Marketplace.IdentityServer.Interfaces;
using Marketplace.IdentityServer.Seeding;
using Marketplace.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEmailAddressValidator, EmailAddressValidator>();

builder.Services.AddDbContext<IdentityContext>(opt => 
{
    opt.UseInMemoryDatabase("InMemoryIdentity");
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

builder.Services.AddAuthentication()
    .AddFacebook(opt =>
    {
        opt.AppId = builder.Configuration["ExternalProviders:Facebook:AppId"];
        opt.AppSecret = builder.Configuration["ExternalProviders:Facebook:AppSecret"];
    })
    .AddGoogle(opt =>
    {
        opt.ClientId = builder.Configuration["ExternalProviders:Google:ClientId"];
        opt.ClientSecret = builder.Configuration["ExternalProviders:Google:ClientSecret"];
        opt.ClaimActions.MapJsonKey("image", "picture");
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    await Seeder.SeedAsync(userManager);
}

app.UseStaticFiles();

app.UseIdentityServer();

app.MapControllerRoute("LoginRoute", "{controller=Auth}/{action=Login}");

app.Run();
