using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plan { get; set; }
        public AccountStatus Status { get; set; }
        public int? DaysLeft { get; set; }
        public bool OverTheLimit { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public List<int> ProjectIds { get; set; } // This field is excluded by default
        public List<Project> Projects { get; set; } // This field is excluded by default
        public string Kind { get; set; }
    }
}
