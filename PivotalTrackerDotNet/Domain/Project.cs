using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp.Deserializers;

namespace PivotalTrackerDotNet.Domain
{
    public class Project
    {
        [JsonProperty("project_id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore] // Readonly
        public int Version { get; set; }

        [JsonProperty("iteration_length")]
        public int IterationLength { get; set; }

        [JsonProperty("week_start_day")]
        public DayOfWeek WeekStartDay { get; set; }

        [JsonProperty("point_scale")]
        public string PointScale { get; set; }

        [JsonIgnore] // Readonly
        public bool PointScaleIsCustom { get; set; }

        [JsonProperty("bugs_and_chores_are_estimatable")]
        public bool BugsAndChoresAreEstimatable { get; set; }

        [JsonProperty("automatic_planning")]
        public bool AutomaticPlanning { get; set; }

        [JsonIgnore]
        [Obsolete]
        public bool EnableFollowing { get; set; }

        [JsonProperty("enable_tasks")]
        public bool EnableTasks { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("time_zone")]
        public TimeZone TimeZone { get; set; }

        [JsonProperty("velocity_averaged_over")]
        public int VelocityAveragedOver { get; set; }

        [JsonIgnore]
        public DateTimeOffset? ShownIterationsStartTime { get; set; } // Excluded by default

        [JsonIgnore]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("number_of_done_iterations_to_show")]
        public int NumberOfDoneIterationsToShow { get; set; }

        [JsonIgnore]
        public bool HasGoogleDomain { get; set; }

        [JsonPropertyAttribute("description")]
        public string Description { get; set; }

        [JsonPropertyAttribute("profile_content")]
        public string ProfileContent { get; set; }

        [JsonProperty("enable_incoming_emails")]
        public bool EnableIncomingEmails { get; set; }

        [JsonProperty("initial_velocity")]
        public int InitialVelocity { get; set; }

        [JsonProperty("project_type")]
        public string ProjectType { get; set; } // The project's type should be private, public, or demo.

        [JsonProperty("public")]
        [DeserializeAs(Name = "public")]
        public bool IsPublic { get; set; }

        [JsonProperty("atom_enabled")]
        public bool AtomEnabled { get; set; }

        [JsonIgnore]
        public int CurrentIterationNumber { get; set; }

        [JsonIgnore]
        public int CurrentVelocity { get; set; }

        [JsonIgnore]
        public float CurrentVolatility { get; set; } // Excluded by default

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonIgnore]
        public AccountingType? AccountingType { get; set; } // Excluded by default

        [JsonIgnore]
        public bool? Featured { get; set; } // Excluded by default

        [JsonIgnore]
        public List<int> StoryIds { get; set; } // Excluded by default

        [JsonIgnore]
        public List<int> EpicIds { get; set; } // Excluded by default

        [JsonIgnore]
        public List<int> MembershipIds { get; set; } // Excluded by default

        [JsonIgnore]
        public List<ProjectMembership> Memberships { get; set; } // Excluded by default

        [JsonIgnore]
        public List<int> LabelIds { get; set; } // Excluded by default

        [JsonIgnore]
        public List<Label> Labels { get; set; } // Excluded by default

        [JsonIgnore]
        public List<int> IntegrationIds { get; set; } // Excluded by default

        [JsonIgnore]
        public List<int> IterationOverrideNumbers  { get; set; } // Excluded by default

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Kind { get; set; }

        public string ToJson()
        {
            using (var writer = new StringWriter())
            {
                var serializer = JsonSerializer.Create();
                serializer.Serialize(writer, this);

                return writer.ToString();
            }
        }

        /*
        {
            "id":456295,
            "kind":"project",
            "name":"My Sample Project",
            "version":330,
            "iteration_length":1,
            "week_start_day":"Monday",
            "point_scale":"0,1,2,3",
            "point_scale_is_custom":false,
            "bugs_and_chores_are_estimatable":false,
            "automatic_planning":true,
            "enable_tasks":true,
            "time_zone":{
                "kind":"time_zone",
                "olson_name":"America/Los_Angeles",
                "offset":"-07:00"
            },
            "velocity_averaged_over":3,
            "number_of_done_iterations_to_show":12,
            "has_google_domain":false,
            "profile_content":"This is a demo project, created by Tracker, with example stories for a simple shopping web site.",
            "enable_incoming_emails":true,
            "initial_velocity":10,
            "public":false,
            "atom_enabled":false,
            "project_type":"demo",
            "start_time":"2012-01-02T08:00:00Z",
            "created_at":"2012-01-18T02:02:47Z",
            "updated_at":"2013-04-13T11:59:26Z",
            "account_id":352837,
            "current_iteration_number":185,
            "enable_following":true
        },
        */
    }
}
