using System.Collections.Generic;
using System.Linq;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet {
    public interface IMembershipService
    {
        List<Person> GetMembers(int projectId);
    }

	public class MembershipService : AAuthenticatedService, IMembershipService {
		const string MemberShipEndpoint = "projects/{0}/memberships";
		public MembershipService(string token)
			: base(token) {
		}

		public List<Person> GetMembers(int projectId) {
			var request = BuildGetRequest();
			request.Resource = string.Format(MemberShipEndpoint, projectId);
            var jObject = RestClient.ExecuteRequestWithChecks(request);
			var persons = new List<Person>();
			persons.AddRange(jObject.Select(person => person["person"].ToObject<Person>()));
			return persons;
		}
	}
}
