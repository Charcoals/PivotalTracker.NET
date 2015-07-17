using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class ProjectService : AAuthenticatedService, IProjectService
    {
        private const string ProjectsEndpoint  = "projects";
        private const string AcitivityEndpoint = "projects/{0}/activity?limit={1}";
        private const string ProjectEpics      = "projects/{0}/epics";

        public ProjectService(string token) : base(token)
        {
        }

        public List<Activity> GetRecentActivity(int projectId)
        {
            return this.GetRecentActivity(projectId, 30);
        }

        public List<Activity> GetRecentActivity(int projectId, int limit)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(AcitivityEndpoint, projectId, limit);
            var response = RestClient.Execute<List<Activity>>(request);
            return response.Data;
        }

        public List<Project> GetProjects()
        {
            var request = BuildGetRequest();
            request.Resource = ProjectsEndpoint;

            return RestClient.ExecuteRequestWithChecks<List<Project>>(request);
        }

        public List<Epic> GetAllProjectEpics(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectEpics, projectId);

            return RestClient.ExecuteRequestWithChecks<List<Epic>>(request);
        }
    }
}
