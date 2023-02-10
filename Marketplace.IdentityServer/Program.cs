using Marketplace.IdentityServer;
using Marketplace.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
    opt.Cookie.Name = "MyIdentityCookie";
    opt.LoginPath = "/Auth/Login";
});

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetService<UserManager<IdentityUser>>();

    var user = new IdentityUser("jack");
    userManager!.CreateAsync(user, "password").Wait();
}

app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();
