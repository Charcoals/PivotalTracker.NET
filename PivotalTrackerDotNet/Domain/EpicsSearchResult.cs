using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class EpicsSearchResult : SearchResult
    {
        public List<Epic> Stories { get; set; }
    }
}