using System;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class Iteration
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public float TeamStrength { get; set; }
        public List<Story> Stories { get; set; }

        public DateTime? StartDate
        {
            get { return Start.ConvertTime(); }
        }

        public DateTime? FinishDate
        {
            get { return Finish.ConvertTime(); }
        }
    }
}