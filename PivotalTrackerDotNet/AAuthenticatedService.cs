using System.Diagnostics;
using System.Globalization;

using PivotalTrackerDotNet.Domain;

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
            this.restClient.AddHandler("application/json", new DictionaryDeserializer());
            this.restClient.AddHandler("text/json", new DictionaryDeserializer());
            this.restClient.AddHandler("text/x-json", new DictionaryDeserializer());
            this.restClient.AddHandler("text/javascript", new DictionaryDeserializer());
            this.restClient.AddHandler("*+json", new DictionaryDeserializer());
        }

        protected AAuthenticatedService(AuthenticationToken token) : this(token.Value)
        {
        }

        protected string Token
        {
            get { return this.token; }
        }

        protected RestClient RestClient
        {
            [DebuggerStepThrough]
            get { return this.restClient; }
        }

        protected RestRequest BuildGetRequest(string uri = null)
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-TrackerToken", this.Token);
            request.RequestFormat = DataFormat.Json;

            if (!string.IsNullOrEmpty(uri))
                request.Resource = uri;

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