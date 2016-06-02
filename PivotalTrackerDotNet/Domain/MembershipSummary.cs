using System;

namespace PivotalTrackerDotNet.Domain
{
    public class MembershipSummary
    {
        public string Kind { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectColor { get; set; }
        public string Role { get; set; }
        public DateTimeOffset LastViewedAt { get; set; }
    }
}