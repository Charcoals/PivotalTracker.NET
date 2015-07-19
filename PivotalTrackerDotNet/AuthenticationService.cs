using PivotalTrackerDotNet.Domain;
using RestSharp;

namespace PivotalTrackerDotNet
{
    public class AuthenticationService : AAuthenticatedService, IAuthenticationService
    {
        private const string AuthenticationEndpoint = "me";

        public AuthenticationService(string token)
            : base(token)
        {
        }

        public AuthenticationService(AuthenticationToken token)
            : base(token)
        {
        }

        public static AuthenticationToken Authenticate(string username, string password)
        {
            var client = new RestClient(PivotalTrackerRestEndpoint.SSLENDPOINT)
                             {
                                 Authenticator = new HttpBasicAuthenticator(username, password)
                             };

            var request  = new RestRequest(AuthenticationEndpoint);
            request.AddQueryParameter("fields", "api_token");
            var response = client.ExecuteRequestWithChecks<AuthenticationToken>(request);

            return response;
        }

        public static Me GetAccount(string username, string password)
        {
            var client = new RestClient(PivotalTrackerRestEndpoint.SSLENDPOINT)
                             {
                                 Authenticator = new HttpBasicAuthenticator(username, password)
                             };

            var request  = new RestRequest(AuthenticationEndpoint);
            var response = client.ExecuteRequestWithChecks<Me>(request);

            return response;
        }

        public Me GetAccount()
        {
            var request = this.BuildGetRequest();
            request.Resource = AuthenticationEndpoint;
            var account = RestClient.ExecuteRequestWithChecks<Me>(request);
            return account;
        }
    }
}