using System.Collections.Generic;
using System.Linq;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class MembershipService : AAuthenticatedService, IMembershipService
    {
        private const string MemberShipEndpoint = "projects/{0}/memberships";

        public MembershipService(string token) : base(token)
        {
        }

        public MembershipService(AuthenticationToken token) : base(token)
        {
        }

        public List<ProjectMembership> GetMembers(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(MemberShipEndpoint, projectId);

            return RestClient.ExecuteRequestWithChecks<List<ProjectMembership>>(request);
        }
    }
}
