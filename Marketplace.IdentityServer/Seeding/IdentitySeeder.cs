using Marketplace.Shared.Claims;
using Marketplace.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Marketplace.IdentityServer.Seeding;

internal static class IdentitySeeder
{
    internal static async Task SeedAsync(UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser() 
        { 
            Id = AppConstants.DefaultUserId,
            UserName = AppConstants.DefaultUserEmail,
            Email = AppConstants.DefaultUserEmail
        };

        await userManager.CreateAsync(user, AppConstants.DefaultUserPassword);
        await userManager.AddClaimAsync(user, new Claim(AppClaimTypes.display_name.ToString(), AppConstants.DefaultUserEmail));
    }
}
