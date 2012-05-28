using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain {
    public class Membership {
        public int Id { get; set; }
        public Person Person { get; set; }
        public string Role { get; set; }
        //  <memberships type="array">
        //    <membership>
        //      <id>1006</id>
        //      <person>
        //        <email>kirkybaby@earth.ufp</email>
        //        <name>James T. Kirk</name>
        //        <initials>JTK</initials>
        //      </person>
        //      <role>Owner</role>
        //    </membership>
        //  </memberships>
    }
}
