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
                }
            ];
    }
}


//public static IEnumerable<Client> Clients =>
//    new Client[]
//    {
//                new Client
//                {
//                    ClientId = "microorder.orderservice",
//                    ClientName = "MicroOrder Order Service",

//                    AllowedGrantTypes = GrantTypes.ClientCredentials,
//                    ClientSecrets = { new Secret("".Sha256()) },

//                    AllowedScopes = { "openid", "profile", "scope1" }
//                }
//                // m2m client credentials flow client
//                new Client
//                {
//                    ClientId = "m2m.client",
//                    ClientName = "Client Credentials Client",

//                    AllowedGrantTypes = GrantTypes.ClientCredentials,
//                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

//                    AllowedScopes = { "scope1" }
//                },

//                // interactive client using code flow + pkce
//                new Client
//                {
//                    ClientId = "interactive",
//                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

//                    AllowedGrantTypes = GrantTypes.Code,

//                    RedirectUris = { "https://localhost:44300/signin-oidc" },
//                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
//                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

//                    AllowOfflineAccess = true,
//                    AllowedScopes = { "openid", "profile", "scope2" }
//                },
//    };