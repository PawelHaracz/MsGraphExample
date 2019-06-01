using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Polly;
using Microsoft.Identity.Client;
namespace GraphExample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var scope = new[] {
            "User.Read",
            "Mail.Send",
            "Files.ReadWrite"
            };

            var graphServiceClient = GraphClientFactory.GetGraphServiceClient("", "https://login.microsoftonline.com/common", scope);

            if(graphServiceClient == null)
            {
                throw new ArgumentException(nameof(graphServiceClient));
            }

            var user = await graphServiceClient.Me.Request().GetAsync();
            string userId = user.Id;
            string mailAddress = user.UserPrincipalName;
            string displayName = user.DisplayName;

            Console.WriteLine("Hello, " + displayName + ". Would you like to send an email to yourself or someone else?");
            Console.WriteLine("Enter the address to which you'd like to send a message. If you enter nothing, the message will go to your address.");
        }
    }
    

}
