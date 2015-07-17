using RestSharp;

namespace PivotalTrackerDotNet
{
    using System;

    public abstract class AAuthenticatedService
    {
        private readonly string token;
        private readonly RestClient restClient;

        protected AAuthenticatedService(string token)
        {
            this.token      = token;
            this.restClient = new RestClient { BaseUrl = new Uri(PivotalTrackerRestEndpoint.SSLENDPOINT) };
        }

        protected string Token
        {
            get { return this.token; }
        }

        protected RestClient RestClient
        {
            get { return this.restClient; }
        }

        protected RestRequest BuildGetRequest()
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-TrackerToken", this.Token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        protected RestRequest BuildPutRequest()
        {
            var request = new RestRequest(Method.PUT);
            request.AddHeader("X-TrackerToken", this.Token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        protected RestRequest BuildDeleteRequest()
        {
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("X-TrackerToken", this.Token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        protected RestRequest BuildPostRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-TrackerToken", this.Token);
            request.RequestFormat = DataFormat.Json;
            return request;
        }
    }
}