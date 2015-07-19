using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IProjectService
    {
        List<Activity> GetRecentActivity(int projectId);

        List<Activity> GetRecentActivity(int projectId, int limit);

        PagedResult<Activity> GetActivity(int projectId, int offset, int limit);

        List<Project> GetProjects();

        List<Project> GetProjects(params int[] accountIds);

        Project AddNewProject(Project project);

        Project GetProject(int projectId);

        Project GetProject(int projectId, ProjectIncludeFields fields);

        Project SaveProject(Project project);

        bool RemoveProject(int projectId);

        List<Label> GetProjectLabels(int projectId);

        List<ProjectMembership> GetProjectMemberships(int projectId);

        List<WebHook> GetProjectWebHooks(int projectId);

        List<Integration> GetProjectIntegrations(int projectId);

        List<Epic> GetAllProjectEpics(int projectId);

        List<Activity> GetEpicActivities(int projectId, int epicId);
    }
}