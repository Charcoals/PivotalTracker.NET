using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Iteration
    {
        public int Number { get; set; }
        public int ProjectId { get; set; }
        public int Length { get; set; }
        public float TeamStrength { get; set; }

        public List<int> StoryIds { get; set; }
        public List<Story> Stories { get; set; }

        public DateTimeOffset? Start { get; set; }
        public DateTimeOffset? Finish { get; set; }

        public string Kind { get; set; }

        /*
            [
              {
                "kind": "iteration",
                "number": 1,
                "project_id": 456301,
                "team_strength": 1,
                "stories": [],
                "start": "2015-07-27T07:00:00Z",
                "finish": "2015-08-03T07:00:00Z"
              }
            ]
         */
    }
}