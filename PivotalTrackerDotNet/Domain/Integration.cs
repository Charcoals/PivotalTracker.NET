using System;

namespace PivotalTrackerDotNet.Domain
{
    public class Integration
    {
        public IntegrationType Type { get; set; }
        public string ApiUsername { get; set; }
        public string ApiPassword { get; set; }
        public string ZendeskUserEmail { get; set; }
        public string ZendeskUserPassword { get; set; }
        public string ViewId { get; set; }
        public string Company { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string StatusesToExclude { get; set; }
        public string FilterId { get; set; }
        public string Account { get; set; }
        public string ExternalApiToken { get; set; }
        public string BinId { get; set; }
        public string ExternalProjectId { get; set; }
        public string ImportApiUrl { get; set; }
        public string BasicAuthUsername { get; set; }
        public string BasicAuthPassword { get; set; }
        public bool CommentsPrivate { get; set; }
        public bool UpdateComments { get; set; }
        public bool UpdateState { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public bool CanImport { get; set; }
        public string BaseUrl { get; set; }
        public bool IsOther { get; set; }
        public string StoryName { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Kind { get; set; }

        /*
           {
               "kind": "other_integration",
               "import_api_url": "http://localhost:3000/starwars.xml",
               "id": 30,
               "project_id": 99,
               "can_import": true,
               "base_url": "http://localhost:3000",
               "is_other": true,
               "story_name": "item",
               "name": "Sounder Flats Computer Complex",
               "active": true,
               "created_at": "2015-07-07T12:00:00Z",
               "updated_at": "2015-07-07T12:00:00Z"
           }
         */
    }
}
