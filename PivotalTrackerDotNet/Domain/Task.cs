using Newtonsoft.Json.Linq;

namespace PivotalTrackerDotNet.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public int StoryId { get; set; }
        public int ProjectId { get; set; }
        public int Position { get; set; }

        public string GetIdToken()
        {
            return string.Format("{0}:{1}:{2}", ProjectId, StoryId, Id);
        }

        public string ToJson()
        {
            return new JObject(
                new JProperty("description", Description),
                new JProperty("complete", Complete))
                .ToString();
        }
    }

    // <task>
    //  <id type="integer">$TASK_ID</id>
    //  <description>find shields</description>
    //  <position>1</position>
    //  <complete>false</complete>
    //  <created_at type="datetime">2008/12/10 00:00:00 UTC</created_at>
    // </task>
}