using Duende.IdentityServer.Models;

namespace Spa.IdentityProvider;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(name: "roles", 
            userClaims: new[] { "role" }, displayName: "Your roles")
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("Spa.Api.basicAccess", "Basic access to the API")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource
            {
                Name = "Spa.Api",
                Description = "SPA API",
                Scopes = new List<string> {"Spa.Api.basicAccess" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
                {
                    ClientId = "WebClient",
                    ClientName = "External API Client",
                    ClientSecrets =
                    {
                        new Secret("test".Sha256())
                    },
                    
                    RedirectUris = {"https://localhost:3000/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:3000"},

                    AllowedScopes =
                    {
                        "openid",
                        "roles",
                        "profile",
                        "Spa.Api.basicAccess",
                    },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowOfflineAccess = true
                }
        };
}
