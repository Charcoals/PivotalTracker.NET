using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
			var response = RestClient.Execute(request);
		    var persons = new List<Person>();
            var serializer = new RestSharpXmlDeserializer();
            var el = XElement.Parse(response.Content);
            persons.AddRange(el.Elements("membership").Select(person => serializer.Deserialize<Person>(person.ToString())));
		    return persons;
		}
	}
}
