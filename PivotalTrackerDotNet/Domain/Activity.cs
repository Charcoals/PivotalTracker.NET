using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Activity
    {
        public ChangeKind Kind { get; set; }

        public string Guid { get; set; }

        public int ProjectVersion { get; set; }

        public string Message { get; set; }

        public string Highlight { get; set; }

        public List<Change> Changes { get; set; }

        public List<PrimaryResource> PrimaryResources { get; set; }

        public ProjectReference Project { get; set; }

        public PersonReference PerformedBy { get; set; }

        public DateTimeOffset OccurredAt { get; set; }
    }
}
