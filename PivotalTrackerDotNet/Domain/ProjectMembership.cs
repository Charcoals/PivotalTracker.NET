using System;

namespace PivotalTrackerDotNet.Domain
{
    public class ProjectMembership
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int ProjectId { get; set; }

        public ProjectRole Role { get; set; }
        public string ProjectColor { get; set; }
        public DateTimeOffset LastViewedAt { get; set; }
        public bool WantsCommentNotificationEmails { get; set; }
        public bool WillReceiveMentionNotificationsOrEmails { get; set; }
        public string Kind { get; set; }

        /*
            {
                "kind":"project_membership",
                "id":1558559,
                "person":{
                    "kind":"person",
                    "id":548577,
                    "name":"pivotaltrackerdotnet",
                    "email":"pivotaltrackerdotnet@gmail.com",
                    "initials":"PI",
                    "username":"pivy"
                },
                "project_id":456295,
                "role":"owner",
                "project_color":"71be00",
                "last_viewed_at":"2014-02-21T13:23:42Z",
                "wants_comment_notification_emails":true,
                "will_receive_mention_notifications_or_emails":true
            }
         */
    }
}
