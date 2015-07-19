namespace PivotalTrackerDotNet.Domain
{
    public class GoogleAttachment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string GoogleKind { get; set; }
        public string Title { get; set; }
        public string GoogleId { get; set; }
        public string AlternateLink { get; set; }
        public string ResourceId { get; set; }
        public string Kind { get; set; }
    }
}