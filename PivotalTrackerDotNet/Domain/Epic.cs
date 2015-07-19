using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Epic
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }

        public string Description { get; set; }

        public List<int> CommentIds { get; set; } // This field is excluded by default
        public List<Comment> Comments { get; set; } // This field is excluded by default

        public List<int> FollowerIds { get; set; } // This field is excluded by default
        public List<Person> Follower { get; set; } // This field is excluded by default

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Unimplemented
        public int AfterId { get; set; } // This field is excluded by default

        // Unimplemented
        public int BeforeId { get; set; } // This field is excluded by default

        [Obsolete]
        public float PastDoneStoryEstimates { get; set; } // This field is excluded by default

        [Obsolete]
        public float PastDoneStoriesCount { get; set; } // This field is excluded by default

        [Obsolete]
        public float PastDoneStoriesNoPointCount { get; set; } // This field is excluded by default

        public string Url { get; set; }

        public string Kind { get; set; }
    }
}