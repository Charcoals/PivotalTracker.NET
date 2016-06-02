namespace PivotalTrackerDotNet.Domain
{
    public class SearchResultContainer
    {
        public string Query { get; set; }
        public StoriesSearchResult Stories { get; set; }
        public EpicsSearchResult  Epics { get; set; }
        public string Kind { get; set; }
    }
}
