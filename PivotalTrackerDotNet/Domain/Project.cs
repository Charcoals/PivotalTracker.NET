using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain {
    public class Project {
        public string Kind { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int IterationLength { get; set; }
        public DayOfWeek WeekStartDay { get; set; }
        public List<Membership> Memberships { get; set; }
        public string VelocityScheme { get; set; }
        public int CurrentVelocity { get; set; }
        public string LastActivityAt { get; set; }
        public bool UseHTTPS { get; set; }        
    }
}
