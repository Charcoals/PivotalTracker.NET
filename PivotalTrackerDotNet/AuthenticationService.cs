using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PivotalTrackerDotNet.Domain;
using RestSharp;

namespace PivotalTrackerDotNet
{
    public class AuthenticationService
    {
        private const string AuthenticationEndpoint = "tokens/active";
        public static AuthenticationToken Authenticate(string username, string password)
        {
            var client = new RestClient();
            client.BaseUrl = PivotalTrackerRestEndpoint.ENDPOINT;
            client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest();
            request.Resource = AuthenticationEndpoint;

            var response = client.Execute<AuthenticationToken>(request);
            return response.Data;
        }
    }
}