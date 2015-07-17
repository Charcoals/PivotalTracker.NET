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

        public static Account GetAccount(string username, string password)
        {
            var client = new RestClient(PivotalTrackerRestEndpoint.SSLENDPOINT)
                             {
                                 Authenticator = new HttpBasicAuthenticator(username, password)
                             };

            var request  = new RestRequest(AuthenticationEndpoint);
            var response = client.ExecuteRequestWithChecks<Account>(request);

            return response;
        }

        public Account GetAccount()
        {
            var request = this.BuildGetRequest();
            request.Resource = AuthenticationEndpoint;
            var account = RestClient.ExecuteRequestWithChecks<Account>(request);
            return account;
        }
    }
}