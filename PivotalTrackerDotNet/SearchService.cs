using System.Collections.Generic;
using System.Linq;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class SearchService : AAuthenticatedService
    {
        private const string MemberShipEndpoint = "projects/{0}/search";

        public SearchService(string token) : base(token)
        {
        }

        public SearchService(AuthenticationToken token) : base(token)
        {
        }

        public SearchResultContainer Search(int projectId, string query)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(MemberShipEndpoint, projectId);

            return RestClient.ExecuteRequestWithChecks<SearchResultContainer>(request);
        }
    }
}
