using System;

namespace PivotalTrackerDotNet.Domain
{
    public static class Extensions
    {
        public static DateTime? ConvertToUtc(this string date)
        {
            return !string.IsNullOrWhiteSpace(date) ? (DateTime?)DateTime.Parse(date.Replace(" UTC", "Z")) : null;            
        }
    }
}