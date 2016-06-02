using System;
using System.Runtime.Serialization;

namespace PivotalTrackerDotNet
{
    [Serializable]
    public class PivotalTrackerException : Exception
    {
        public PivotalTrackerException(string message) : base(message)
        {
        }

        public PivotalTrackerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PivotalTrackerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}