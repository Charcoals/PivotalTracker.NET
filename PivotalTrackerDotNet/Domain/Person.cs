namespace PivotalTrackerDotNet.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
        public string UserName { get; set; }
        public string Kind { get; set; }

        /*
            {
                "kind":"person",
                "id":548577,
                "name":"pivotaltrackerdotnet",
                "email":"pivotaltrackerdotnet@gmail.com",
                "initials":"PI",
                "username":"pivy"
            }
         */
    }
}
