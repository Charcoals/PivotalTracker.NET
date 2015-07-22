using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PivotalTrackerDotNet.Domain
{
    public class Story
    {
        public Story()
        {
            this.Labels = new List<Label>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonProperty(PropertyName = "story_type")]
        public StoryType StoryType { get; set; }

        public StoryStatus CurrentState { get; set; }
        public int? Estimate { get; set; }
        public DateTimeOffset? AcceptedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }

        public int? RequestedById { get; set; }
        public Person RequestedBy { get; set; }

        [Obsolete]
        public int OwnedById { get; set; }

        [JsonProperty(PropertyName = "owner_ids")]
        public List<int> OwnerIds { get; set; }
        public List<Person> Owners { get; set; } // Documented but doesn't look like it is ever returned

        [JsonProperty(PropertyName = "label_ids")]
        public List<int> LabelIds { get; set; }
        public List<Label> Labels { get; set; }

        [JsonProperty(PropertyName = "task_ids")]
        public List<int> TaskIds { get; set; } // This field is excluded by default
        public List<Task> Tasks { get; set; } // This field is excluded by default

        [JsonProperty(PropertyName = "follower_ids")]
        public List<int> FollowerIds { get; set; } // This field is excluded by default
        public List<Person> Followers { get; set; } // This field is excluded by default

        [JsonProperty(PropertyName = "comment_ids")]
        public List<int> CommentIds { get; set; } // This field is excluded by default
        public List<Comment> Comments { get; set; } // This field is excluded by default

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Unimplemented
        public int? BeforeId { get; set; } // This field is excluded by default

        // Unimplemented
        public int? AfterId { get; set; } // This field is excluded by default

        public int? IntegrationId { get; set; }
        public string ExternalId { get; set; }
        public string Url { get; set; }
        public string Kind { get; set; }

        public string ToJson()
        {
            var values = new JObject(new JProperty("name", Name),
                                new JProperty("story_type", StoryType.ToString().ToLower()),
                                new JProperty("description", Description),
                                new JProperty("requested_by_id", RequestedById),
                                new JProperty("owner_ids", new JArray(OwnerIds)),                                
                                new JProperty("labels", new JArray(Labels.Select(l => l.Name))));

            if (Estimate.HasValue)
            {
                values.Add(new JProperty("estimate", Estimate));
            }

            return values.ToString();
        }

    }

    // <story>
    //  <id type="integer">$STORY_ID</id>
    //  <project_id type="integer">$PROJECT_ID</project_id>
    //  <story_type>feature</story_type>
    //  <url>http://www.pivotaltracker.com/story/show/$STORY_ID</url>
    //  <estimate type="integer">1</estimate>
    //  <current_state>accepted</current_state>
    //  <lighthouse_id>43</lighthouse_id>
    //  <lighthouse_url>http://mylighthouseapp.com/projects/100/tickets/43</lighthouse_url>
    //  <description></description>
    //  <name>More power to shields</name>
    //  <requested_by>James Kirk</requested_by>
    //  <owned_by>Montgomery Scott</owned_by>
    //  <created_at type="datetime">2008/12/10 00:00:00 UTC</created_at>
    //  <accepted_at type="datetime">2008/12/10 00:00:00 UTC</accepted_at>
    //  <labels>label 1,label 2,label 3</labels>
    //  <attachments type="array">
    //    <attachment>
    //      <id type="integer">4</id>
    //      <filename>shield_improvements.pdf</filename>
    //      <description>How to improve the shields in 3 easy steps.</description>
    //      <uploaded_by>James Kirk</uploaded_by>
    //      <uploaded_at type="datetime">2008/12/10 00:00:00 UTC</uploaded_at>
    //      <url>http://www.pivotaltracker.com/resource/download/1295103</url>
    //    </attachment>
    //  </attachments>
    // </story>
}
