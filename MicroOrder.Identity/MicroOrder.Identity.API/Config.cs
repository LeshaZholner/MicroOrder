using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MicroOrder.Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            ];

        public static IEnumerable<ApiScope> ApiScopes =>
            [
                new ApiScope("orderserviceapi.fullaccess"),
                new ApiScope("productserviceapi.fullaccess"),
            ];

        public static IEnumerable<ApiResource> ApiResources =>
            [
                new ApiResource("orderserviceapi", "Order Service API")
                {
                    Scopes = { "orderserviceapi.fullaccess" },
                },
                new ApiResource("productserviceapi", "Product Service API")
                {
                    Scopes = { "productserviceapi.fullaccess" },
                }
            ];

        public static IEnumerable<Client> Clients =>
            [
                new Client
                {
                    ClientId = "microorder.postman",
                    ClientName = "MicroOrder Postman",

                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("5D1F2BB2-ABA6-4CF4-9D63-E4ABE7A6D058".Sha256()) },

                    RedirectUris = { "https://localhost:5001/callback" },
                    PostLogoutRedirectUris = { "https://localhost:5001/logout" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "orderserviceapi.fullaccess",
                        "productserviceapi.fullaccess"
                    }
                },
                new Client
                {
                    ClientId = "microorder.webapp",
                    ClientName = "MicroOrder Web App",

                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("".Sha256()) },
                    ClientUri = "https://localhost:5002",

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "orderserviceapi.fullaccess",
                        "productserviceapi.fullaccess"
                    }
                }
            ];
    }
}


