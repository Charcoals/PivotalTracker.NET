using PivotalTrackerDotNet.Domain;
using RestSharp;

namespace PivotalTrackerDotNet
{
    public abstract class AAuthenticatedService
    {
        protected readonly AuthenticationToken m_token;
        protected RestClient RestClient;
        protected AAuthenticatedService(AuthenticationToken token, bool needsSSL = false)
        {
            m_token = token;
            RestClient = new RestClient();
            if (needsSSL) {
                RestClient.BaseUrl = PivotalTrackerRestEndpoint.SSLENDPOINT;
            } else {
                RestClient.BaseUrl = PivotalTrackerRestEndpoint.ENDPOINT;
            }
        }

        protected RestRequest BuildGetRequest()
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-TrackerToken", m_token.Guid.ToString("N"));
            request.RequestFormat = DataFormat.Xml;
            return request;
        }

        protected RestRequest BuildPutRequest()
        {
            var request = new RestRequest(Method.PUT);
            request.AddHeader("X-TrackerToken", m_token.Guid.ToString("N"));
            request.RequestFormat = DataFormat.Xml;
            return request;
        }

        protected RestRequest BuildDeleteRequest()
        {
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("X-TrackerToken", m_token.Guid.ToString("N"));
            request.RequestFormat = DataFormat.Xml;
            return request;
        }

        protected RestRequest BuildPostRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-TrackerToken", m_token.Guid.ToString("N"));
            request.RequestFormat = DataFormat.Xml;
            return request;
        }
    }
}