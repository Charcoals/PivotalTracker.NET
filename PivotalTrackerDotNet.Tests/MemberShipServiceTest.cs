using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class MembershipServiceTest
    {
        private const int ProjectId = 456301;
        private MembershipService membershipService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.membershipService = new MembershipService(Constants.ApiToken);
        }

        [Test]
        public void CanRetrieveAllPersonsAllowedInAProject()
        {
            var persons = this.membershipService.GetMembers(ProjectId);
            Assert.NotNull(persons);
            Assert.AreEqual(1, persons.Count);
        }
    }

    // ReSharper restore InconsistentNaming
}
