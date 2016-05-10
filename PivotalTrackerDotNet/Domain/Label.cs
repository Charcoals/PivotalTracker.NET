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

        public override string ToString() {
            return String.Format("{0} - {1}", ID, Name);
        }

        public override bool Equals(Object obj) {
            Label p = obj as Label;
            if ((object)p == null)
            {
                return false;
            }

            return Name.Equals(p.Name);
        }

        public bool Equals(Label p) {
            return Name.Equals(p.Name);
        }

        public override int GetHashCode() {
            return Name.GetHashCode();
        }
    }
}
