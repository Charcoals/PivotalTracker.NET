namespace PivotalTrackerDotNet.Domain
{
    public class PrimaryResource : PivotalTrackerResource
    {
        public StoryType StoryType { get; set; }
        public string Url { get; set; }
    }
}