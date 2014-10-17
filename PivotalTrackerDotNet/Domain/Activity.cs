using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain
{
    public class Activity
    {
        public string kind { get; set; }
        public string guid { get; set; }
        public int project_version { get; set; }
        public string message { get; set; }
        public string highlight { get; set; }
        public List<Change> changes { get; set; }
        public List<PrimaryResource> primary_resources { get; set; }
        public Project project { get; set; }
        public PerformedBy performed_by { get; set; }
        public DateTime occurred_at { get; set; }

        public class PerformedBy
        {
            public string kind { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string initials { get; set; }
        }

        public class PrimaryResource
        {
            public string kind { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string story_type { get; set; }
            public string url { get; set; }
        }

        public class Change
        {
            public string kind { get; set; }
            public string change_type { get; set; }
            public int id { get; set; }
            public Values original_values { get; set; }
            public Values new_values { get; set; }
            public string name { get; set; }
            public string story_type { get; set; }
        }

        public class Values
        {
            public DateTime updated_at { get; set; }
            public string current_state { get; set; }
            public int before_id { get; set; }
            public int? after_id { get; set; }
        }
    }
}
