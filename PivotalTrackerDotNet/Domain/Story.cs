using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain {
    public class Story {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public StoryType StoryType { get; set; }
        public int Estimate { get; set; }
        public StoryStatus CurrentState { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string RequestedBy { get; set; }
        public List<Note> Notes { get; set; }
        public List<Task> Tasks { get; set; }
    }

    public enum StoryStatus { UnScheduled, UnStarted, Started, Finished, Delivered, Accepted, Rejected }
    public enum StoryType { Bug, Chore, Feature }
    //  <story>
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
    //</story>
}
