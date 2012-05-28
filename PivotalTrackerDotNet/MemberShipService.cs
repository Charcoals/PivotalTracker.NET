using System.Collections.Generic;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet {
    public interface IMembershipService
    {
        List<Person> GetMembers(int projectId);
    }

	public class MembershipService : AAuthenticatedService, IMembershipService {
		const string MemberShipEndpoint = "projects/{0}/memberships";
		public MembershipService(AuthenticationToken token)
			: base(token) {
		}

		public List<Person> GetMembers(int projectId) {
			var request = BuildGetRequest();
			request.Resource = string.Format(MemberShipEndpoint, projectId);
			var response = RestClient.Execute<List<Person>>(request);
			return response.Data;
		}
	}
}
