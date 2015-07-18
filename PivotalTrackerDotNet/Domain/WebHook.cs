using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain
{
    public class WebHook
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string WebhookUrl { get; set; }
        public string WebhookVersion { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Kind { get; set; }

        /*
           {
               "kind": "webhook",
               "id": 27,
               "project_id": 99,
               "webhook_url": "http://example.com:3000/test_postbin",
               "webhook_version": "v5",
               "created_at": "2015-07-07T12:00:00Z",
               "updated_at": "2015-07-07T12:00:00Z"
           }
        */
    }
}
