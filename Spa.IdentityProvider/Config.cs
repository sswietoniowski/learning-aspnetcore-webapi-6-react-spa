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
                    ClientId = "ExternalApiClient",
                    ClientSecrets =
                    {
                        new Secret("test".Sha256())
                    },
                    AllowedScopes = {"Spa.Api.basicAccess"},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "MvcClient",
                    ClientName = "SPA MVC Client",
                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("test".Sha256())
                    },

                    RedirectUris = {"https://localhost:4000/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:4000"},

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
                },
        };
}
