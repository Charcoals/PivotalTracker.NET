using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace PivotalTrackerDotNet {
    public static class RestExtensions {
        public static JToken ExecuteRequestWithChecks(this RestClient client, RestRequest request) {
            var response = client.Execute(request);
            CheckResponse(response);
            if (response.StatusCode == HttpStatusCode.NoContent) {
                return null;
            }
            return JToken.Parse(response.Content);
        }

        public static T ExecuteRequestWithChecks<T>(this RestClient client, RestRequest request) where T : new() {
            var response = client.Execute<T>(request);
            CheckResponse(response);
            return response.Data;
        }

        private static void CheckResponse(IRestResponse response) {
            if (response.ErrorException != null) {
                throw response.ErrorException;
            }
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent) { // ignore no content errors
                var jObject = JToken.Parse(response.Content);
                var values = new Dictionary<string, string>();
                foreach (var child in jObject.Children()) {
                    if (child != null && child.Type == JTokenType.Property) {
                        var prop = child as JProperty;
                        if (prop != null) {
                            values[prop.Name] = prop.Value.ToString();
                        }
                    }
                }
                if (values.ContainsKey("error")) {
                    string errorMessage = string.Join(", ", values.Keys.Select(k => string.Format("{0}: {1}", k, values[k])));
                    throw new Exception(errorMessage);
                }
            }
        }
    }
}