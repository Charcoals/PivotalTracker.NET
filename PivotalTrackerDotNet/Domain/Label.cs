using System;

namespace PivotalTrackerDotNet.Domain {
    public class Label {
        public static implicit operator Label(string name) {
            return new Label {Name = name};
        }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public int ID { get; set; }

		public string toString() {
			return String.Format("{0} - {1}", ID, Name);
		}
    }
}
