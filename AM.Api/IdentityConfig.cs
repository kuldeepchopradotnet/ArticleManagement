﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AM.Api
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
         {
             new ApiResource("fiver_auth_api", "Fiver.Security.AuthServer.Api")
         };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
         {
             new IdentityResources.OpenId(),
             new IdentityResources.Profile(),
         };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
         {
                new Client
                {
                    ClientId = "ro.angular",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "api1"
                    }
                },
            new Client
             {
                 ClientId = "fiver_auth_client",
                 ClientName = "Fiver.Security.AuthServer.Client",
                 ClientSecrets = { new Secret("secret".Sha256()) },

                 AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                 AllowOfflineAccess = true,
                 RequireConsent = false,

                 RedirectUris = { "http://localhost:5002/signin-oidc" },
                 PostLogoutRedirectUris =
                   { "http://localhost:5002/signout-callback-oidc" },

                 AllowedScopes =
                 {
                     IdentityServerConstants.StandardScopes.OpenId,
                     IdentityServerConstants.StandardScopes.Profile,
                     "fiver_auth_api"
                 },
             }
         };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
         {
             new TestUser
             {
                 SubjectId = "1",
                 Username = "james",
                 Password = "password",
                 Claims = new List<Claim>
                 {
                     new Claim("name", "James Bond"),
                     new Claim("website", "https://james.com")
                 }
             }
         };
        }

    }
}
