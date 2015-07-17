using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Account
    {
        public Account()
        {
            this.Projects = new List<MembershipSummary>();
        }

        public string Kind { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Username { get; set; }
        public TimeZone TimeZone { get; set; }
        public string ApiToken { get; set; }
        public bool HasGoogleIdentity { get; set; }
        public List<MembershipSummary> Projects { get; set; }
        public string Email { get; set; }
        public bool ReceivesInAppNotifications { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        /*
        {
            "kind":"me",
            "id":548577,
            "name":"pivotaltrackerdotnet",
            "initials":"PI",
            "username":"pivy",
            "time_zone":{
                "kind":"time_zone",
                "olson_name":"America/Los_Angeles",
                "offset":"-07:00"
            },
            "api_token":"bbe863ce69a4603590a5b2d235d5f650",
            "has_google_identity":false,
            "projects":[
                {
                    "kind":"membership_summary",
                    "id":1558559,
                    "project_id":456295,
                    "project_name":"My Sample Project",
                    "project_color":"71be00",
                    "role":"owner",
                    "last_viewed_at":"2014-02-21T13:23:42Z"
                },
                {
                    "kind":"membership_summary",
                    "id":1558573,
                    "project_id":456301,
                    "project_name":"ShallWeMeet",
                    "project_color":"00a3d6",
                    "role":"owner",
                    "last_viewed_at":"2013-10-02T13:34:35Z"
                }
            ],
            "email":"pivotaltrackerdotnet@gmail.com",
            "receives_in_app_notifications":true,
            "created_at":"2012-01-18T02:02:22Z",
            "updated_at":"2015-07-17T11:59:38Z"
        }
         */
    }
}