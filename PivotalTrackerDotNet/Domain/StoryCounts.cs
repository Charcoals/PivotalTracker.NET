namespace PivotalTrackerDotNet.Domain
{
    public class StoryCounts
    {
        public CountsByStoryState NumberOfZeroPointStoriesByState { get; set; }
        public CountsByStoryState SumOfStoryEstimatesByState { get; set; }
        public CountsByStoryState NumberOfStoriesByState { get; set; }
        public string Kind { get; set; }
    }
}