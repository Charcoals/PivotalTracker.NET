using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class FilteringCriteria
    {
        private StringBuilder m_filters;
        FilteringCriteria()
        {
            m_filters = new StringBuilder();
        }

        public static FilteringCriteria FilterBy
        {
            get { return new FilteringCriteria(); }
        }

        public FilteringCriteria Requester(string requester)
        {
            if (!string.IsNullOrWhiteSpace(requester)) AddFilter(string.Format("requester:\"{0}\"", requester));
            return this;
        }

        public FilteringCriteria Owner(string owner)
        {
            if (!string.IsNullOrWhiteSpace(owner)) AddFilter(string.Format("owner:\"{0}\"", owner));
            return this;
        }

        public FilteringCriteria Label(Label label)
        {
            if (!string.IsNullOrWhiteSpace(label.Name)) AddFilter(string.Format("label:\"{0}\"", label.Name));
            return this;
        }

        public FilteringCriteria Type(StoryType type)
        {
            AddFilter(string.Format("type:{0}", type.ToString().ToLower()));
            return this;
        }

        public FilteringCriteria State(StoryStatus state)
        {
            AddFilter(string.Format("state:{0}", state.ToString().ToLower()));
            return this;
        }

        public FilteringCriteria CreatedSince(DateTime since)
        {
            AddFilter(string.Format("created_since:\"{0}\"", since.ToString("u")));
            return this;
        }

        public FilteringCriteria ModifiedSince(DateTime since)
        {
            AddFilter(string.Format("modified_since:\"{0}\"", since.ToString("u")));
            return this;
        }

        private void AddFilter(string filter)
        {
            if(m_filters.Length > 0)
            {
                m_filters.Append(" ");
            }
            m_filters.Append(filter);
        }

        public override string ToString()
        {
            return m_filters.ToString();
        }
    }
}
