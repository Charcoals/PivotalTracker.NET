using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IMembershipService
    {
        List<ProjectMembership> GetMembers(int projectId);
    }
}