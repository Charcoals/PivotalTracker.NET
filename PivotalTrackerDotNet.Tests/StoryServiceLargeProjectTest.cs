using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests
{
    [TestFixture]
    public class StoryServiceLargeProjectTest
    {
        private StoryService storyService;
        private const int projectId = 456295;
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.storyService = new StoryService(Constants.ApiToken);
        }

        [Test]
        public void CanRetrieveAllStories()
        {
            var stories = this.storyService.GetIceboxStories(projectId);
            Assert.NotNull(stories);
            Assert.GreaterOrEqual(stories.Count, 100);
        }
    }
}