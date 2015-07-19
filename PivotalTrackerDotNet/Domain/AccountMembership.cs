using System;

namespace PivotalTrackerDotNet.Domain
{
    public class AccountMembership
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int AccountId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool Owner { get; set; }
        public bool Admin { get; set; }
        public bool ProjectCreator { get; set; }
        public bool Timekeeper { get; set; }
        public bool TimeEnterer { get; set; }
        public string Kind { get; set; }
    }
}