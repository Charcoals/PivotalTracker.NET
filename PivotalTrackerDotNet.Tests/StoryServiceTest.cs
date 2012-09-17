using System;
using PivotalTrackerDotNet.Domain;
using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests
{
    [TestFixture]
    public class StoryServiceTest
    {
        private StoryService storyService = null;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            storyService = new StoryService(AuthenticationService.Authenticate(Constants.Username, Constants.Password));
        }

        [TearDown]
        public void Cleanup()
        {
            var stories = storyService.GetAllStories(Constants.ProjectId);
            foreach (var storey in stories)
            {
                storyService.RemoveStory(Constants.ProjectId, storey.Id);
            }
        }

        [Test]
        public void CanRetrieveSingleStory()
        {
            var savedStory = storyService.AddNewStory(Constants.ProjectId, new Story
                                                                               {
                                                                                   Name = "Nouvelle histoire",
                                                                                   RequestedBy = "pivotaltrackerdotnet",
                                                                                   StoryType = StoryType.Feature,
                                                                                   Description = "bla bla bla and more bla",
                                                                                   ProjectId = Constants.ProjectId,
                                                                                   Estimate = 2
                                                                               });
            storyService.AddNewTask(new Task
            {
                Description = "wololo",
                ParentStoryId = savedStory.Id,
                ProjectId = savedStory.ProjectId
            });

            var retrieved = storyService.GetStory(Constants.ProjectId, savedStory.Id);
            Assert.NotNull(retrieved);
            Assert.AreEqual(Constants.ProjectId, retrieved.ProjectId);
            Assert.AreEqual(savedStory.Id, retrieved.Id);
            Assert.AreEqual(savedStory.StoryType, retrieved.StoryType);
            Assert.AreEqual(savedStory.Name, retrieved.Name);
            Assert.AreEqual(savedStory.Estimate, retrieved.Estimate);
            Assert.AreEqual(1, retrieved.Tasks.Count);
//           Assert.AreEqual(savedStory.Id, retrieved.Tasks[0].ParentStoryId);

        }

        [Test]
        public void CanRetrieveAllStories()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story);

            var stories = storyService.GetAllStories(Constants.ProjectId);
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test, Ignore("The code works, but the lag from pivotal is such that it requires to sleep for close to a minute to pass")]
        public void CanRetrieveAllStories_Paging()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Bug,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId
            };

            storyService.AddNewStory(Constants.ProjectId, story);
            storyService.AddNewStory(Constants.ProjectId, story2);
            System.Threading.Thread.Sleep(30000);//There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally

            var stories = storyService.GetAllStories(Constants.ProjectId, 2, 0);
            Assert.NotNull(stories);
            Assert.AreEqual(2, stories.Count);

            stories = storyService.GetAllStories(Constants.ProjectId, 1, 1);
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
        }

        [Test]
        public void CanGetAllStoriesMatchingFilter_FreeForm()
        {
            var story1 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Bug,
                Description = "some story",
                ProjectId = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "another story",
                ProjectId = Constants.ProjectId
            };

            var story3 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "yet another story",
                ProjectId = Constants.ProjectId
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story1);

            storyService.AddNewStory(Constants.ProjectId, story2);
            storyService.AddNewStory(Constants.ProjectId, story3);

            System.Threading.Thread.Sleep(5000);//There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = storyService.GetAllStoriesMatchingFilter(Constants.ProjectId, "type:bug requester:\"pivotaltrackerdotnet\"");
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test]
        public void CanGetAllStoriesMatchingFilter_Strict()
        {
            var story1 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Bug,
                Description = "some story",
                ProjectId = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "another story",
                ProjectId = Constants.ProjectId
            };

            var story3 = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "yet another story",
                ProjectId = Constants.ProjectId
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story1);
            savedStory.Labels = "5.1.30, plume";
            savedStory = storyService.UpdateStory(Constants.ProjectId, savedStory);

            storyService.AddNewStory(Constants.ProjectId, story2);
            storyService.AddNewStory(Constants.ProjectId, story3);

            System.Threading.Thread.Sleep(5000);//There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = storyService.GetAllStoriesMatchingFilter(Constants.ProjectId, FilteringCriteria.FilterBy.Label("5.1.30").Type(StoryType.Bug));
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test]
        public void CanRetrieveIceBoxStories()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId,

            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story);
            System.Threading.Thread.Sleep(5000);//There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = storyService.GetIceboxStories(Constants.ProjectId);
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test]
        public void CanAddAndDeleteStores()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId,
                Estimate = 9
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story);
            Assert.AreEqual(story.Name, savedStory.Name);
            Assert.AreEqual(Constants.ProjectId, savedStory.ProjectId);
            Assert.AreEqual(story.RequestedBy, savedStory.RequestedBy);
            Assert.AreEqual(story.StoryType, savedStory.StoryType);
            Assert.AreEqual(story.Description, savedStory.Description);
            //Assert.AreEqual(9, expected.Estimate);


            var deletedStory = storyService.RemoveStory(Constants.ProjectId, savedStory.Id);
            VerifyStory(savedStory, deletedStory);
        }

        [Test]
        public void CanUpdateStory()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId,
                Estimate = 9
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story);
            savedStory.Name = "Call be New name";
            savedStory.Description = "wololo";
            savedStory.Estimate = 1;
            savedStory.Labels = "laby hh,pool";


            var updatedStory = storyService.UpdateStory(Constants.ProjectId, savedStory);
            VerifyStory(savedStory, updatedStory);
        }

        [Test]
        public void CanSaveTask()
        {

            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story);


            var task = storyService.AddNewTask(new Task { Description = "stuff stuff stuff", ParentStoryId = savedStory.Id, ProjectId = Constants.ProjectId });


            var guid = Guid.NewGuid().ToString();

            task.Description = guid;

            storyService.SaveTask(task);

            var stories = storyService.GetAllStories(Constants.ProjectId);

            Assert.AreEqual(guid, stories[0].Tasks[0].Description);
        }

        [Test]
        public void CanAddGetAndDeleteNewTasks()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedBy = "pivotaltrackerdotnet",
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId
            };

            var savedStory = storyService.AddNewStory(Constants.ProjectId, story);

            var task = new Task { Description = "stuff stuff stuff", ParentStoryId = savedStory.Id, ProjectId = Constants.ProjectId };

            var savedTask = storyService.AddNewTask(task);
            Assert.AreEqual(task.Description, savedTask.Description);

            var retrievedTask = storyService.GetTask(Constants.ProjectId, savedTask.ParentStoryId, savedTask.Id);
            Assert.NotNull(retrievedTask);

            Assert.IsTrue(storyService.RemoveTask(retrievedTask.ProjectId, task.ParentStoryId, retrievedTask.Id));

        }


        private static void VerifyStory(Story expected, Story actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(Constants.ProjectId, actual.ProjectId);
            Assert.AreEqual(expected.RequestedBy, actual.RequestedBy);
            Assert.AreEqual(expected.StoryType, actual.StoryType);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Estimate, actual.Estimate);
            Assert.AreEqual(expected.Labels, actual.Labels);
        }

    }

    [TestFixture]
    public class StoryServiceLargeProjectTest
    {
        private StoryService storyService;
        private const int projectId = 456295;
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            storyService = new StoryService(AuthenticationService.Authenticate(Constants.Username, Constants.Password));
        }

        [Test]
        public void CanRetrieveAllStories()
        {
            var stories = storyService.GetAllStories(projectId);
            Assert.NotNull(stories);
            Assert.GreaterOrEqual(stories.Count, 150);
        }
    }

}