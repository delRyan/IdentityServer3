﻿using System.Security.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace Thinktecture.IdentityServer.Host.Config
{
    public class Factory
    {
        public static IdentityServerServiceFactory Create(
                    string issuerUri, string siteName, string publicHostAddress = "")
        {
            var users = new InMemoryUser[]
            {
                new InMemoryUser{Subject = "818727", Username = "alice", Password = "alice", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Alice"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "AliceSmith@email.com"),
                    }
                },
                new InMemoryUser{Subject = "88421113", Username = "bob", Password = "bob", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "BobSmith@email.com"),
                    }
                },
            };

            var settings = new Settings(issuerUri, siteName, publicHostAddress);
            var scopes = new InMemoryScopeService(Scopes.Get());
            var clients = new InMemoryClientService(Clients.Get());
            var userSvc = new InMemoryUserService(users);

            var fact = new IdentityServerServiceFactory
            {
                UserService = Registration.RegisterFactory<IUserService>(() => userSvc),
                CoreSettings = Registration.RegisterFactory<CoreSettings>(() => settings),
                ScopeService = Registration.RegisterFactory<IScopeService>(() => scopes),
                ClientService = Registration.RegisterFactory<IClientService>(() => clients)
            };

            //fact.Register(Registration.RegisterType<IFoo>(typeof(Foo)));
            //fact.Register(Registration.RegisterFactory<IBar>(() => new Bar()));
            //var q = new Quux();
            //fact.Register(Registration.RegisterFactory<Quux>(() => q));

            return fact;
        }
    }
}