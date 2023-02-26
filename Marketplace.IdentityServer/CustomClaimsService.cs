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
            Claim? profilePicture = context.Subject.FindFirst("profile_picture");

            claims.Add(userName!);
            if (displayName is not null)
            {
                claims.Add(displayName);
            }
            if (profilePicture is not null)
            {
                claims.Add(profilePicture);
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
