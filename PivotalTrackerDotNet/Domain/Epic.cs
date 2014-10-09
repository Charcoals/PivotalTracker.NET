using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain
{
    public class Epic
    {
        public int id { get; set; }
        public string kind { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int project_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Label label { get; set; }
    }
}
