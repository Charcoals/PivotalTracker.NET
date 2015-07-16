using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IProjectService
    {
        List<Project> GetProjects();
        List<Activity> GetRecentActivity(int projectId);
        List<Activity> GetRecentActivity(int projectId, int limit);
        List<Epic> GetAllProjectEpics(int projectId);
    }
}