using System;

namespace PivotalTrackerDotNet.Domain
{
    public static class Extensions
    {
        public static DateTime? ConvertTime(this string date)
        {
            return !string.IsNullOrWhiteSpace(date) ? (DateTime?)DateTime.Parse(date) : null;            
        }
    }
}