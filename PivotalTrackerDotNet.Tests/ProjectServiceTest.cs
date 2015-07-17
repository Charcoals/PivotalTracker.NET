using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests
{
    [TestFixture]
    public class ProjectServiceTest
    {
        private ProjectService service;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.service = new ProjectService(Constants.ApiToken);
        }

        [Test]
        public void GetProjects()
        {
            var result = this.service.GetProjects();
            Assert.AreEqual(2, result.Count);
        }
    }
}
