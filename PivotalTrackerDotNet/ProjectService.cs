using System.Collections.Generic;
using System.Linq;

using PivotalTrackerDotNet.Domain;

using RestSharp;

namespace PivotalTrackerDotNet
{
    public class ProjectService : AAuthenticatedService, IProjectService
    {
        private const string ProjectsEndpoint               = "projects";
        private const string SingleProjectEndpoint          = "projects/{0}";
        private const string ProjectLabelsEndpoint          = "projects/{0}/labels";
        private const string ProjectMembershipsEndpoint     = "projects/{0}/memberships";
        private const string ProjectWebHooksEndpoint        = "projects/{0}/webhooks";
        private const string ProjectIntegrationsEndpoint    = "projects/{0}/integrations";
        private const string AcitivityEndpoint              = "projects/{0}/activity?limit={1}";
        private const string ProjectEpics                   = "projects/{0}/epics";

        public ProjectService(string token) : base(token)
        {
        }

        public ProjectService(AuthenticationToken token) : base(token)
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

        public List<Project> GetProjects(params int[] accountIds)
        {
            var request = BuildGetRequest();
            request.Resource = ProjectsEndpoint;
            request.AddQueryParameter("account_ids", string.Join(",", accountIds));
            
            return RestClient.ExecuteRequestWithChecks<List<Project>>(request);
        }

        public Project AddNewProject(Project project)
        {
            var request = BuildPostRequest();
            request.Resource = ProjectsEndpoint;
            request.AddParameter("application/json", project.ToJson(), ParameterType.RequestBody);
            var savedProject = RestClient.ExecuteRequestWithChecks<Project>(request);
            return savedProject;
        }

        public Project GetProject(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleProjectEndpoint, projectId);
            request.AddQueryParameter("fields", ":default,shown_iterations_start_time,current_velocity,current_volatility");

            return RestClient.ExecuteRequestWithChecks<Project>(request);
        }

        public Project GetProject(int projectId, ProjectIncludeFields fields)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleProjectEndpoint, projectId);
            string fieldsQuery = ":default,shown_iterations_start_time,current_velocity,current_volatility";

            var fieldsToInclude = this.GetFieldsNames(fields);
            
            if (fieldsToInclude.Any())
                fieldsQuery += "," + string.Join(",", fieldsToInclude);
            
            request.AddQueryParameter("fields", fieldsQuery);


            return RestClient.ExecuteRequestWithChecks<Project>(request);
        }

        public Project SaveProject(Project project)
        {
            var request = BuildPutRequest();
            request.Resource = string.Format(SingleProjectEndpoint, project.Id);
            request.AddParameter("application/json", project.ToJson(), ParameterType.RequestBody);
            var savedProject = RestClient.ExecuteRequestWithChecks<Project>(request);
            return savedProject;
        }

        public bool RemoveProject(int projectId)
        {
            var request = BuildDeleteRequest();
            request.Resource = string.Format(SingleProjectEndpoint, projectId);
            var response = RestClient.ExecuteRequestWithChecks<Project>(request);
            return response == null;
        }

        public List<Label> GetProjectLabels(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectLabelsEndpoint, projectId);
            request.AddQueryParameter("fields", ":default,created_at,updated_at,counts");

            return RestClient.ExecuteRequestWithChecks<List<Label>>(request);
        }

        public List<ProjectMembership> GetProjectMemberships(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectMembershipsEndpoint, projectId);
            ////request.AddQueryParameter("fields", ":default,person_id");

            return RestClient.ExecuteRequestWithChecks<List<ProjectMembership>>(request);
        }

        public List<WebHook> GetProjectWebHooks(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectWebHooksEndpoint, projectId);

            return RestClient.ExecuteRequestWithChecks<List<WebHook>>(request);
        }

        public List<Integration> GetProjectIntegrations(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectIntegrationsEndpoint, projectId);

            return RestClient.ExecuteRequestWithChecks<List<Integration>>(request);
        }

        public List<Epic> GetAllProjectEpics(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(ProjectEpics, projectId);

            return RestClient.ExecuteRequestWithChecks<List<Epic>>(request);
        }

        private IEnumerable<string> GetFieldsNames(ProjectIncludeFields fields)
        {
            if (fields.HasFlag(ProjectIncludeFields.AccountingType))
                yield return "accounting_type";

            if (fields.HasFlag(ProjectIncludeFields.EpicIds))
                yield return "epic_ids";

            if (fields.HasFlag(ProjectIncludeFields.Epics))
                yield return "epics";

            if (fields.HasFlag(ProjectIncludeFields.Featured))
                yield return "featured";

            if (fields.HasFlag(ProjectIncludeFields.IntegrationIds))
                yield return "integration_ids";

            if (fields.HasFlag(ProjectIncludeFields.Integrations))
                yield return "integrations";

            if (fields.HasFlag(ProjectIncludeFields.IterationOverrideNumbers))
                yield return "iteration_override_numbers";

            if (fields.HasFlag(ProjectIncludeFields.LabelIds))
                yield return "label_ids";

            if (fields.HasFlag(ProjectIncludeFields.Labels))
                yield return "labels";

            if (fields.HasFlag(ProjectIncludeFields.MembershipIds))
                yield return "membership_ids";

            if (fields.HasFlag(ProjectIncludeFields.Memberships))
                yield return "memberships";

            if (fields.HasFlag(ProjectIncludeFields.StoryIds))
                yield return "story_ids";

            if (fields.HasFlag(ProjectIncludeFields.Stories))
                yield return "stories";
        }
    }
}
