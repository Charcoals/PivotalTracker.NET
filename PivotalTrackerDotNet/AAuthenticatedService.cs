using RestSharp;

namespace PivotalTrackerDotNet
{
    public abstract class AAuthenticatedService
    {
        protected readonly string m_token;
        protected RestClient RestClient;
        protected AAuthenticatedService(string token)
        {
            m_token = token;
            RestClient = new RestClient {BaseUrl = PivotalTrackerRestEndpoint.SSLENDPOINT};
        }

        protected RestRequest BuildGetRequest()
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-TrackerToken", m_token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        protected RestRequest BuildPutRequest()
        {
            var request = new RestRequest(Method.PUT);
            request.AddHeader("X-TrackerToken", m_token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        protected RestRequest BuildDeleteRequest()
        {
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("X-TrackerToken", m_token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        protected RestRequest BuildPostRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-TrackerToken", m_token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }
    }
}