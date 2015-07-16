using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IMembershipService
    {
        List<Person> GetMembers(int projectId);
    }
}