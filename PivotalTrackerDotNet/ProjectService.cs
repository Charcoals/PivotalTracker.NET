using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet {
    public interface IProjectService
    {
        List<Project> GetProjects();
        List<Activity> GetRecentActivity(int projectId);
        List<Activity> GetRecentActivity(int projectId, int limit);
    }

    public class ProjectService : AAuthenticatedService, IProjectService {
        const string ProjectsEndpoint = "projects/";
        const string AcitivityEndpoint = "projects/{0}/activities?limit={1}";

        public ProjectService(AuthenticationToken Token) : base(Token) { }

        public List<Activity> GetRecentActivity(int projectId) {
            return GetRecentActivity(projectId, 30);
        }

        public List<Activity> GetRecentActivity(int projectId, int limit) {
            var request = BuildGetRequest();
            request.Resource = string.Format(AcitivityEndpoint, projectId, limit);
            var response = RestClient.Execute<List<Activity>>(request);
            return response.Data;
        }

        public List<Project> GetProjects() {
            var request = BuildGetRequest();
            request.Resource = ProjectsEndpoint;

            var response = RestClient.Execute(request);
            var projects = new List<Project>();
            var serializer = new RestSharpXmlDeserializer();
            var el = ParseContent(response);
            projects.AddRange(el.Elements("project").Select(project => serializer.Deserialize<Project>(project.ToString())));
            return projects;
        }
    }
}
