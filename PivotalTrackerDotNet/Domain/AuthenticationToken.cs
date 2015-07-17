using System;

using Newtonsoft.Json;

using RestSharp.Deserializers;

namespace PivotalTrackerDotNet.Domain
{
    public class AuthenticationToken
    {
        [JsonProperty(PropertyName = "api_token")]
        [DeserializeAs(Name = "api_token")]
        public string Value { get; set; }
        public int Id { get; set; }

        public Guid Guid
        {
            get
            {
                return new Guid(this.Value);
            }

            set
            {
                this.Value = value.ToString();
            }
        }
    }
}
