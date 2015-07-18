namespace PivotalTrackerDotNet.Domain
{
    public class CountsByStoryState
    {
        public int Delivered { get; set; }
        public int Unscheduled { get; set; }
        public int Rejected { get; set; }
        public int Finished { get; set; }
        public int Unstarted { get; set; }
        public int Planned { get; set; }
        public int Accepted { get; set; }
        public int Started { get; set; }
        public string Kind { get; set; }
    }
}