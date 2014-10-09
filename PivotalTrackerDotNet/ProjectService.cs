using System.Collections.Generic;
using System.Linq;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet {
    public interface IProjectService
    {
        List<Project> GetProjects();
        List<Activity> GetRecentActivity(int projectId);
        List<Activity> GetRecentActivity(int projectId, int limit);
    }

    public class ProjectService : AAuthenticatedService, IProjectService {
        const string ProjectsEndpoint = "projects";
        const string AcitivityEndpoint = "projects/{0}/activities?limit={1}";
        const string ProjectEpics = "projects/{0}/epics";

        public ProjectService(string Token) : base(Token) { }

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

            return RestClient.ExecuteRequestWithChecks<List<Project>>(request);
        }

        public List<Epic> GetAllProjectEpics(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectEpics, projectId); ;

            return RestClient.ExecuteRequestWithChecks<List<Epic>>(request);
        }
    }
}
