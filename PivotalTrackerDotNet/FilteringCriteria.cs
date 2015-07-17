using System;
using System.Text;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class FilteringCriteria
    {
        private readonly StringBuilder filters;

        private FilteringCriteria()
        {
            this.filters = new StringBuilder();
        }

        public static FilteringCriteria FilterBy
        {
            get { return new FilteringCriteria(); }
        }

        public FilteringCriteria Requester(string requester)
        {
            if (!string.IsNullOrWhiteSpace(requester)) this.AddFilter(string.Format("requester:\"{0}\"", requester));
            return this;
        }

        public FilteringCriteria Owner(string owner)
        {
            if (!string.IsNullOrWhiteSpace(owner)) this.AddFilter(string.Format("owner:\"{0}\"", owner));
            return this;
        }

        public FilteringCriteria Label(Label label)
        {
            if (!string.IsNullOrWhiteSpace(label.Name)) this.AddFilter(string.Format("label:\"{0}\"", label.Name));
            return this;
        }

        public FilteringCriteria Type(StoryType type)
        {
            this.AddFilter(string.Format("type:{0}", type.ToString().ToLower()));
            return this;
        }

        public FilteringCriteria State(StoryStatus state)
        {
            this.AddFilter(string.Format("state:{0}", state.ToString().ToLower()));
            return this;
        }

        public FilteringCriteria CreatedSince(DateTime since)
        {
            this.AddFilter(string.Format("created_since:\"{0}\"", since.ToString("u")));
            return this;
        }

        public FilteringCriteria ModifiedSince(DateTime since)
        {
            this.AddFilter(string.Format("modified_since:\"{0}\"", since.ToString("u")));
            return this;
        }

        public override string ToString()
        {
            return this.filters.ToString();
        }

        private void AddFilter(string filter)
        {
            if (this.filters.Length > 0)
            {
                this.filters.Append(" ");
            }

            this.filters.Append(filter);
        }
    }
}