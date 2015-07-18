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

        [Test]
        public void GetProject()
        {
            var project = this.service.GetProject(456295);
            Assert.IsNotNull(project);
        }

        [Test]
        public void GetProjectWithMemberships()
        {
            var project = this.service.GetProject(456295, ProjectIncludeFields.Memberships);
            Assert.IsNotNull(project);
        }

        [Test]
        public void GetProjectLabels()
        {
            var labels = this.service.GetProjectLabels(456295);
            Assert.IsNotNull(labels);
        }

        [Test]
        public void GetProjectMembers()
        {
            var memberships = this.service.GetProjectMemberships(456295);
            Assert.IsNotNull(memberships);
        }

        [Test]
        public void GetProjectWebHooks()
        {
            var webhooks = this.service.GetProjectWebHooks(456295);
            Assert.IsNotNull(webhooks);
        }

        [Test]
        public void GetProjectIntegrations()
        {
            var integrations = this.service.GetProjectIntegrations(456295);
            Assert.IsNotNull(integrations);
        }
    }
}
