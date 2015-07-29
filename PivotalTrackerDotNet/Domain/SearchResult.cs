namespace PivotalTrackerDotNet.Domain
{
    public abstract class SearchResult
    {
        public int TotalHits { get; set; }
        public int TotalHitsWithDone { get; set; }
        public string Kind { get; set; }
    }
}