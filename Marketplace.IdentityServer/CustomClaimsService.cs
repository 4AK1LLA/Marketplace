using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;

namespace Marketplace.IdentityServer
{
    public class CustomClaimsService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim>();
            Claim? userName = context.Subject.FindFirst("name");
            Claim? displayName = context.Subject.FindFirst("display_name");

            claims.Add(userName!);
            if (displayName is not null)
            {
                claims.Add(displayName);
            }

            context.IssuedClaims = claims;
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
