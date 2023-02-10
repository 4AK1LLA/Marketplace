using IdentityServer4.Models;

namespace Marketplace.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId()
        };


    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("Marketplace.WebAPI")
            {
                ApiSecrets =
                {
                    new Secret("marketplace_api_secret".Sha256())
                },
                Scopes = { "Marketplace.WebAPI" }
            }
        };

    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "angular_ui",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "openid", "Marketplace.WebAPI" },
                AllowAccessTokensViaBrowser = true, 
                RequireClientSecret = false,
                RequirePkce = true,

                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200" }
            }
        };
}

