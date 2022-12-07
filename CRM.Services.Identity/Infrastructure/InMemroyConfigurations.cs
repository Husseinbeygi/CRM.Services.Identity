using IdentityServer4.Models;

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

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("pass".Sha256())
                    },


                    Claims =  new List<ClientClaim>
                    {
                        new ClientClaim
                        {
                           Type = "Role",
                           Value = "Admin"
                        }
                    },

                    // scopes that client has access to
                    AllowedScopes = { "leads" }
            }
        };
}
