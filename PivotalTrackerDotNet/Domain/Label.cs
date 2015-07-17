namespace PivotalTrackerDotNet.Domain
{
    public class Label
    {
        public int? ProjectID { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public int? ID { get; set; }

        public static implicit operator Label(string name)
        {
            return new Label { Name = name };
        }

        public override bool Equals(object obj)
        {
            var other = obj as Label;

            if (this.ProjectID != null && other.ProjectID != null && this.ProjectID != other.ProjectID)
                return false;

            if (this.ID != null && other.ID != null && this.ID != other.ID)
                return false;

            if (this.Kind != null && other.Kind != null && !string.Equals(this.Kind, other.Kind))
                return false;

            return string.Equals(this.Name, other.Name);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            // TODO: Fix this
            return Name.GetHashCode();
        }
    }
}