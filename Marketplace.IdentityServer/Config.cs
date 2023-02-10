using IdentityModel;
using IdentityServer4.Models;

namespace Marketplace.IdentityServer;

public static class Config
{
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
                ClientId = "ui_client",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "Marketplace.WebAPI" },
                AllowAccessTokensViaBrowser = true, 
                RequireClientSecret = false,
                RequirePkce = true
            }
        };
}

