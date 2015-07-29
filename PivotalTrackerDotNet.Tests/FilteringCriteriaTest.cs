using System;
using NUnit.Framework;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class FilteringCriteriaTest
    {
        [Test]
        public void CanFilterByRequester()
        {
            var requester = "JTK";
            string expected = string.Format("requester:\"{0}\"", requester);
            Assert.AreEqual(expected, FilteringCriteria.FilterBy.Requester(requester).ToString());
        }

        [Test]
        public void CanFilterByRequester_Empty()
        {
            Assert.AreEqual(string.Empty, FilteringCriteria.FilterBy.Requester(string.Empty).ToString());
        }

        [Test]
        public void CanFilterByType()
        {
            Assert.AreEqual("type:bug", FilteringCriteria.FilterBy.Type(StoryType.Bug).ToString());
        }

        [Test]
        public void CanFilterByState()
        {
            Assert.AreEqual("state:unscheduled", FilteringCriteria.FilterBy.State(StoryStatus.Unscheduled).ToString());
        }

        [Test]
        public void CanFilterByModified()
        {
            Assert.AreEqual("modified_since:\"2008-09-09 00:00:00Z\"", FilteringCriteria.FilterBy.ModifiedSince(new DateTime(2008, 9, 9)).ToString());
        }

        [Test]
        public void CanFilterByCreated()
        {
            Assert.AreEqual("created_since:\"2008-09-09 00:00:00Z\"", FilteringCriteria.FilterBy.CreatedSince(new DateTime(2008, 9, 9)).ToString());
        }

        [Test]
        public void CanFilterByOwner()
        {
            var owner = "JTK";
            string expected = string.Format("owner:\"{0}\"", owner);
            Assert.AreEqual(expected, FilteringCriteria.FilterBy.Owner(owner).ToString());
        }

        [Test]
        public void CanFilterByOwner_Empty()
        {
            Assert.AreEqual(string.Empty, FilteringCriteria.FilterBy.Owner(string.Empty).ToString());
        }

        [Test]
        public void CanCombineFilter()
        {
            var owner = "JTK";
            var requester = "Hey Hey";

            string expected = string.Format("owner:\"{0}\" requester:\"{1}\"", owner, requester);
            Assert.AreEqual(expected, FilteringCriteria.FilterBy.Owner(owner).Requester(requester).ToString());
        }
    }

    // ReSharper restore InconsistentNaming
}