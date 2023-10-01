using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]
            {
                new ApiResource("alevelwebsite.com")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("mvc")
                    }
                },

                new ApiResource("catalog")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog.catalogitem"),
                        new Scope("catalog.catalogbrand"),
                        new Scope("catalog.catalogtype"),
                        new Scope("catalog.catalogsubtype"),
                        new Scope("catalog.catalogmodel")
                    }
                },

                new ApiResource("order")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("order.order"),
                        new Scope("order.orderuser"),
                        new Scope("order.orderitem")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "mvc_pkce",
                    ClientName = "MVC PKCE Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris = { $"{configuration["MvcUrl"]}/signin-oidc" },
                    AllowedScopes = { "openid", "profile", "mvc" },
                    RequirePkce = true,
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true
                },
                new Client
                {
                    ClientId = "catalog",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["CatalogApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "catalog.catalogitem",
                        "catalog.catalogbrand",
                        "catalog.catalogtype",
                        "catalog.catalogsubtype",
                        "catalog.catalogmodel"
                    }
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc"
                    }
                },
                new Client
                {
                    ClientId = "orderswaggerui",
                    ClientName = "Order Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["OrderApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["OrderApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc",
                        "order.order",
                        "order.orderuser",
                        "order.orderitem"
                    }
                }
            };
        }
    }
}