using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests
{
    [TestFixture]
    public class StoryServiceLargeProjectTest
    {
        private const int ProjectId = 456295;
        private StoryService storyService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.storyService = new StoryService(Constants.ApiToken);
        }

        [Test]
        public void CanRetrieveAllStories()
        {
            var stories = this.storyService.GetIceboxStories(ProjectId);
            Assert.NotNull(stories);
            Assert.GreaterOrEqual(stories.Count, 100);
        }
    }
}