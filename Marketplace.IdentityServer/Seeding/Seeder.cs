using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Marketplace.IdentityServer.Seeding;

internal static class Seeder
{
    private const string DefaultPassword = "Pa$$w0rd";

    internal static async Task SeedAsync(UserManager<IdentityUser> userManager)
    {
        var email = "test@marketplace.com";

        var user = new IdentityUser(email) 
        { 
            Email = email
        };

        await userManager.CreateAsync(user, DefaultPassword);
        await userManager.AddClaimAsync(user, new Claim("display_name", "Mega ADMIN"));
    }
}
