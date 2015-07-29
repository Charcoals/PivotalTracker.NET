using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class StoriesSearchResult : SearchResult
    {
        public List<Story> Stories { get; set; }
        public float TotalPoints { get; set; }
        public float TotalPointsCompleted { get; set; }
    }
}