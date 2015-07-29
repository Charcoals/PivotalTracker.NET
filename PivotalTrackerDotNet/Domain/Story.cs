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
            var values = new JObject(
                  new JProperty("name", this.Name)
                , new JProperty("story_type", this.StoryType.ToString().ToLower())
                , new JProperty("description", this.Description)
                , new JProperty("requested_by_id", this.RequestedById)
                , new JProperty("current_state", this.CurrentState.ToString().ToLower())
            );

            if (this.Owners != null)
                values.Add(new JProperty("owner_ids", new JArray(this.Owners.Select(o => o.Id))));
            else
                values.Add(new JProperty("owner_ids", new JArray(this.OwnerIds)));

            if (this.LabelIds != null)
                values.Add(new JProperty("label_ids", new JArray(this.LabelIds)));
            else
                values.Add(new JProperty("labels", new JArray(this.Labels.Select(l => l.Name))));

            if (this.AcceptedAt.HasValue)
                values.Add(new JProperty("accepted_at", this.AcceptedAt));

            if (this.Estimate.HasValue)
                values.Add(new JProperty("estimate", this.Estimate));

            // Can only be set on releases
            if (this.StoryType == StoryType.Release && this.Deadline.HasValue)
                values.Add(new JProperty("deadline", this.Deadline));

            // TODO: Add scheduling properties
            ////new JProperty("group", Group.ToString().ToLower()), --- public enum StoryScheduleGroup { Scheduled, Unscheduled, Current }
            ////new JProperty("before_id", BeforeId),
            ////new JProperty("after_id", AfterId)

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
