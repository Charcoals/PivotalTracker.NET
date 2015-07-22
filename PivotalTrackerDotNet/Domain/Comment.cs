using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public int? StoryId { get; set; }
        public int? EpicId { get; set; }
        public string Text { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public List<int> FileAttachmentIds { get; set; }
        public List<FileAttachment> FileAttachments { get; set; }

        public List<int> GoogleAttachmentIds { get; set; }
        public List<GoogleAttachment> GoogleAttachments { get; set; }

        public string CommitIdentifier { get; set; }
        public string CommitType { get; set; }
        public string Kind { get; set; }
    }
}