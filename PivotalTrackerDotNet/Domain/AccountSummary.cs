using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class AccountSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountStatus Status { get; set; }
        public int DaysLeft { get; set; }
        public bool OverTheLimit { get; set; }
        public int? OwnerId { get; set; } // This field is excluded by default
        public Person Owner { get; set; } // This field is excluded by default
        public List<int> AdminIds { get; set; } // This field is excluded by default
        public List<Person> Admins { get; set; } // This field is excluded by default
        public AccountPermissions? Permissions { get; set; } // This field is excluded by default
        public int? NumberOfProjects { get; set; } // This field is excluded by default
        public int? NumberOfPrivateProjects { get; set; } // This field is excluded by default
        public int? ProjectLimit { get; set; } // This field is excluded by default
        public string Kind { get; set; }
    }
}