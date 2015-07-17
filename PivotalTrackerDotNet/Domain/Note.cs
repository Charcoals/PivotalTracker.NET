using System;

namespace PivotalTrackerDotNet.Domain
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string NotedAt { get; set; }

        public DateTime? Timestamp
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NotedAt))
                    return DateTime.Parse(this.NotedAt.Replace(" UTC", string.Empty)).ToLocalTime();

                return null;
            }
        }

        // <note>
        //   <id type="integer">18904723</id>
        //   <text>asdf</text>
        //   <author>pivotaltrackerdotnet</author>
        //   <noted_at type="datetime">2012/02/21 18:48:30 UTC</noted_at>
        // </note>
    }
}
