﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>{
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };
        }
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource ("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client {
                    ClientId = "ro.client",
                        // no interactive user, use the clientid/secret for authentication
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        // secret for authentication
                        ClientSecrets = {
                            new Secret ("secret".Sha256 ())
                            },
                            // scopes that client has access to
                            AllowedScopes = { "api1" }
                            },new Client
                                    {
                                        ClientId = "mvc",
                                        ClientName = "MVC Client",
                                        AllowedGrantTypes = GrantTypes.Hybrid,

                                        ClientSecrets =
                                        {
                                            new Secret("secret".Sha256())
                                        },

                                        RedirectUris           = { "http://localhost:5002/signin-oidc" },
                                        PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                                        AllowedScopes =
                                        {
                                            IdentityServerConstants.StandardScopes.OpenId,
                                            IdentityServerConstants.StandardScopes.Profile,
                                            "api1"
                                        },
                                        AllowOfflineAccess = true
                                    }
        };
        }
        public static List<TestUser> GetUsers()
        {


            return new List<TestUser> {
                new TestUser {
                    SubjectId = "1",
                        Username = "alice",
                        Password = "password"
                        ,Claims = new[]{
                            new Claim("moew","asdasd")
                        ,    new Claim("gou","gou 123")
                        }
                },
                new TestUser {
                    SubjectId = "2",
                        Username = "bob",
                        Password = "password"
                        ,Claims = new[]{ new Claim("moew","123123213") }
                }
            };
        }

    }
}
