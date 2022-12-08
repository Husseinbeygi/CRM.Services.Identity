using IdentityServer4.Models;
using Microsoft.VisualBasic;

namespace CRM.Services.Identity.Infrastructure;

public static class InMemroyConfigurations
{
    public static IEnumerable<IdentityResource> IdentityResources =>
 new IdentityResource[]
 {
 new IdentityResources.OpenId(),
 new IdentityResources.Profile()
 };

    public static IEnumerable<ApiScope> ApiScopes =>
    new List<ApiScope>
    {
                new ApiScope("leads", "Leads Api")
    };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                    ClientId = "admin@domain.local",

                    ClientName = "AdminInMemoryClient",

                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("pass".Sha256())
                    },

                    RedirectUris=new List<string>
                    {
                        "https://localhost:7282/authentication/login-callback"
                    },

                    Claims =  new List<ClientClaim>
                    {
                        new ClientClaim
                        {
                           Type = "Role",
                           Value = "Admin"
                        }
                    },

                    RequirePkce = true,
                    // scopes that client has access to
                    AllowedScopes = { "leads","openid","profile" }
            }
        };
}
