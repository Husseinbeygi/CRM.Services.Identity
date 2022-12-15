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
					ClientId = "184cf96c-5e2a-457f-bb8c-9514fe4e085d",

					ClientName =  "CRMFrontend",

					AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,

					ClientSecrets =
					{
						new Secret("7dcd9407-10b0-42e9-99bd-13ff89590e0c".Sha256())
					},

					RedirectUris=new List<string> 
					{
						"https://localhost:7083/authentication/login-callback"
					},

					Claims =  new List<ClientClaim>
					{
						new ClientClaim
						{
						   Type = System.Security.Claims.ClaimTypes.Role,
						   Value = "Admin"
						},
						new ClientClaim
						{
							Type = System.Security.Claims.ClaimTypes.Locality,
							Value = Thread.CurrentThread.CurrentUICulture.Name
						}
					},

					RequirePkce = true,
                    // scopes that client has access to
                    AllowedScopes = { "leads","openid","profile" }
			}
		};
}
