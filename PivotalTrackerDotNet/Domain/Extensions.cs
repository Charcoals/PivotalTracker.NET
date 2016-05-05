using System;
using System.Globalization;

namespace PivotalTrackerDotNet.Domain
{
    public static class Extensions
    {
        public static DateTime? ConvertTime(this string date)
        {
            return !string.IsNullOrWhiteSpace(date) ? (DateTime?)DateTime.Parse(date, CultureInfo.GetCultureInfo("en-US")) : null; // Pivotal uses US dates, so use the right converter
        }
    }
}