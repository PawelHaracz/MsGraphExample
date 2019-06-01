using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace GraphExample
{ 
    public static class GraphClientFactory
    {
        public static GraphServiceClient GetGraphServiceClient(string clientId, string authority, IEnumerable<string> scopes)
        {
            var authenticationProvider = CreateAuthorizationProvider(clientId, authority, scopes);
            return new GraphServiceClient(authenticationProvider);
        }

        private static IAuthenticationProvider CreateAuthorizationProvider(string clientId, string authority, IEnumerable<string> scopes)
        {
            var clientApplication = PublicClientApplicationBuilder.Create(clientId).WithAuthority(authority).Build();
            return new MsalAuthenticationProvider(clientApplication, scopes.ToArray());
        }
    }
}
