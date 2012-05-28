using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain {
    public class Activity {
        public int Id { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; }
        public string OccurredAt { get; set; }
        public string Author { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
    //<?xml version="1.0" encoding="UTF-8"?>
    //<activities type="array">
    //  <activity>
    //    <id type="integer">1031</id>
    //    <version type="integer">175</version>
    //    <event_type>story_update</event_type>
    //    <occurred_at type="datetime">2009/12/14 14:12:09 PST</occurred_at>
    //    <author>James Kirk</author>
    //    <project_id type="integer">26</project_id>
    //    <description>James Kirk accepted &quot;More power to shields&quot;</description>
    //    <stories type="array">
    //      <story>
    //        <id type="integer">109</id>
    //        <project_id type="integer">1</project_id>
    //        <url>https://www.pivotaltracker.com/services/v3/projects/26/stories/109</url>
    //        <accepted_at type="datetime">2009/12/14 22:12:09 UTC</accepted_at>
    //        <current_state>accepted</current_state>
    //      </story>
    //    </stories>
    //  </activity>
    //</activities>
    }
}
