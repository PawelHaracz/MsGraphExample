using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace GraphExample
{
    public class MsalAuthenticationProvider : IAuthenticationProvider
    {
        private IPublicClientApplication _clientApplication;
        private string[] _scopes;

        public MsalAuthenticationProvider(IPublicClientApplication clientApplication, string[] scopes)
        {
            _clientApplication = clientApplication;
            _scopes = scopes;
        }

        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var authentication = await GetAuthenticationAsync();
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authentication.CreateAuthorizationHeader());
        }

        /// <summary>
        /// Acquire Token for user
        /// </summary>
        public async Task<AuthenticationResult> GetAuthenticationAsync()
        {
            AuthenticationResult authResult = null;
            var accounts = await _clientApplication.GetAccountsAsync();

            try
            {
                authResult = await _clientApplication.AcquireTokenSilent(_scopes, accounts.FirstOrDefault()).ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    authResult = await _clientApplication.AcquireTokenInteractive(_scopes).ExecuteAsync();
                }
                catch (MsalException)
                {
                    throw;
                }
            }

            return authResult;
        }

    }
}
