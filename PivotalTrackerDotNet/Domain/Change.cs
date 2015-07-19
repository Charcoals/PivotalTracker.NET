using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Change
    {
        public ResourceKind Kind { get; set; }
        public ChangeType ChangeType { get; set; }
        public int Id { get; set; }

        public Dictionary<string, string> NewValues { get; set; }
        public Dictionary<string, string> OriginalValues { get; set; }


        public string Name { get; set; }
        public StoryType StoryType { get; set; }
    }
}