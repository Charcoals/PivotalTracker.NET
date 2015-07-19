namespace PivotalTrackerDotNet.Domain
{
    public abstract class PivotalTrackerResource
    {
        public ResourceKind Kind { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}