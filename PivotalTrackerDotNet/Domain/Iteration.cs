using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Iteration
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int ProjectId { get; set; }
        public int Length { get; set; }
        public float TeamStrength { get; set; }

        public List<int> StoryIds { get; set; }
        public List<Story> Stories { get; set; }

        public DateTimeOffset? Start { get; set; }
        public DateTimeOffset? Finish { get; set; }

        public string Kind { get; set; }
    }
}