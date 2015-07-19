using System;
using System.Runtime.Serialization;

namespace PivotalTrackerDotNet
{
    [Serializable]
    public class PivotalTrackerResourceNotFoundException : PivotalTrackerException
    {
        public PivotalTrackerResourceNotFoundException(string message) : base(message)
        {
        }

        public PivotalTrackerResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PivotalTrackerResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}