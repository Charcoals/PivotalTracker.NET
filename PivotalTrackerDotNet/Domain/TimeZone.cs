using System;

namespace PivotalTrackerDotNet.Domain
{
    public class TimeZone
    {
        /// <summary>Gets or sets the kind.</summary>
        /// <value>The kind.</value>
        public string Kind { get; set; }

        /// <summary>Gets or sets the Olson time zone name.</summary>
        /// <value>The Olson time zone name.</value>
        /// <remarks>See <a href="https://en.wikipedia.org/wiki/Tz_database">Tz database</a></remarks>
        public string OlsonName { get; set; }

        /// <summary>Gets or sets the offset.</summary>
        /// <value>The offset.</value>
        public string Offset { get; set; }
    }
}