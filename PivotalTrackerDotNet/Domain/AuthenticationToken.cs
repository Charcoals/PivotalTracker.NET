using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PivotalTrackerDotNet.Domain
{
    public class AuthenticationToken
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return new XElement("token",
                                new XElement("guid", Guid.ToString()),
                                new XElement("id",
                                            new XAttribute("type", "integer"),
                                            Id)).ToString();
        }
    }
}
