namespace PivotalTrackerDotNet.Domain
{
    public class Pagination
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Returned { get; set; }
    }
}