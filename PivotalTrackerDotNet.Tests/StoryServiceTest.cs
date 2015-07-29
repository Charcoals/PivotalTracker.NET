using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class StoryServiceTest
    {
        private StoryService storyService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.storyService = new StoryService(Constants.ApiToken);
        }

        [TearDown]
        public void Cleanup()
        {
            var stories = this.storyService.GetAllStories(Constants.ProjectId);
            foreach (var storey in stories)
            {
                this.storyService.RemoveStory(Constants.ProjectId, storey.Id);
            }
        }

        [Test]
        public void CanRetrieveStoriesWithNestedObjects()
        {
            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, new Story
                                                                               {
                                                                                   Name          = "Nouvelle histoire",
                                                                                   RequestedById = Constants.UserId,
                                                                                   StoryType     = StoryType.Feature,
                                                                                   Description   = "bla bla bla and more bla",
                                                                                   ProjectId     = Constants.ProjectId,
                                                                                   Estimate      = 2
                                                                               });

            this.storyService.AddNewTask(new Task
            {
                Description = "wololo",
                StoryId     = savedStory.Id,
                ProjectId   = savedStory.ProjectId
            });

            this.storyService.AddComment(savedStory.ProjectId, savedStory.Id, "Comment 1");
            this.storyService.AddComment(savedStory.ProjectId, savedStory.Id, "Comment 2");

            var items = storyService.GetAllStories(Constants.ProjectId, StoryIncludeFields.Owners | 
                                                                        StoryIncludeFields.OwnerIds | 
                                                                        StoryIncludeFields.Followers | 
                                                                        StoryIncludeFields.FollowerIds | 
                                                                        StoryIncludeFields.Comments | 
                                                                        StoryIncludeFields.Tasks | 
                                                                        StoryIncludeFields.RequestedBy);

            Assert.AreEqual(1, items.Count);
            Assert.IsNotNull(items.First().Followers);
            Assert.IsNotNull(items.First().RequestedBy);
            Assert.IsNotNull(items.First().Comments);
            Assert.IsNotNull(items.First().Tasks);
            Assert.Greater(items.First().Comments.Count, 0);
            Assert.Greater(items.First().Tasks.Count, 0);
            items.First().Comments.ForEach(c => Assert.IsNotNull(c.Person));
        }

        [Test]
        public void CanRetrieveSingleStory()
        {
            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, new Story
                                                                               {
                                                                                   Name          = "Nouvelle histoire",
                                                                                   RequestedById = Constants.UserId,
                                                                                   StoryType     = StoryType.Feature,
                                                                                   Description   = "bla bla bla and more bla",
                                                                                   ProjectId     = Constants.ProjectId,
                                                                                   Estimate      = 2
                                                                               });
            this.storyService.AddNewTask(new Task
            {
                Description = "wololo",
                StoryId     = savedStory.Id,
                ProjectId   = savedStory.ProjectId
            });

            List<Task> tasks = this.storyService.GetTasksForStory(Constants.ProjectId, savedStory);

            var retrieved = this.storyService.GetStory(Constants.ProjectId, savedStory.Id);
            Assert.NotNull(retrieved);
            Assert.AreEqual(Constants.ProjectId, retrieved.ProjectId);
            Assert.AreEqual(savedStory.Id, retrieved.Id);
            Assert.AreEqual(savedStory.StoryType, retrieved.StoryType);
            Assert.AreEqual(savedStory.Name, retrieved.Name);
            Assert.AreEqual(savedStory.Estimate, retrieved.Estimate);
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual(savedStory.Id, tasks[0].StoryId);
        }

        [Test]
        public void CanRetrieveSingleStoryWithOwner()
        {
            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, new Story
                                                                               {
                                                                                   Name          = "Nouvelle histoire",
                                                                                   RequestedById = Constants.UserId,
                                                                                   StoryType     = StoryType.Feature,
                                                                                   Description   = "bla bla bla and more bla",
                                                                                   ProjectId     = Constants.ProjectId,
                                                                                   Estimate      = 2,
                                                                                   OwnerIds      = new List<int>() { Constants.UserId }
                                                                               });

            var retrieved = this.storyService.GetStory(Constants.ProjectId, savedStory.Id, StoryIncludeFields.OwnerIds);
            Assert.NotNull(retrieved);
            Assert.AreEqual(savedStory.OwnerIds, retrieved.OwnerIds);
        }

        [Test]
        public void CanRetrieveSingleStoryWithFields()
        {
            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, new Story
                                                                               {
                                                                                   Name          = "Nouvelle histoire",
                                                                                   RequestedById = Constants.UserId,
                                                                                   StoryType     = StoryType.Feature,
                                                                                   Description   = "bla bla bla and more bla",
                                                                                   ProjectId     = Constants.ProjectId,
                                                                                   Estimate      = 2,
                                                                               });
            this.storyService.AddNewTask(new Task
            {
                Description = "wololo",
                StoryId     = savedStory.Id,
                ProjectId   = savedStory.ProjectId
            });

            this.storyService.AddComment(savedStory.ProjectId, savedStory.Id, "Comment 1");
            this.storyService.AddComment(savedStory.ProjectId, savedStory.Id, "Comment 2");

            var retrieved = this.storyService.GetStory(Constants.ProjectId, savedStory.Id, StoryIncludeFields.Tasks | StoryIncludeFields.Comments);
            Assert.NotNull(retrieved);
            Assert.AreEqual(Constants.ProjectId, retrieved.ProjectId);
            Assert.AreEqual(savedStory.Id, retrieved.Id);
            Assert.AreEqual(savedStory.StoryType, retrieved.StoryType);
            Assert.AreEqual(savedStory.Name, retrieved.Name);
            Assert.AreEqual(savedStory.Estimate, retrieved.Estimate);
            Assert.AreEqual(1, retrieved.Tasks.Count);
            Assert.AreEqual(savedStory.Id, retrieved.Tasks[0].StoryId);
            Assert.AreEqual(2, retrieved.Comments.Count);
        }

        [Test]
        public void CanRetrieveAllStories()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);
            Assert.AreEqual(StoryType.Feature, savedStory.StoryType);

            var stories = this.storyService.GetAllStories(Constants.ProjectId);
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
            Assert.AreEqual(StoryType.Feature, stories[0].StoryType);
        }

        [Test] //, Ignore("The code works, but the lag from pivotal is such that it requires to sleep for close to a minute to pass")]
        public void CanRetrieveAllStories_Paging()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Bug,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId
            };

            this.storyService.AddNewStory(Constants.ProjectId, story);
            this.storyService.AddNewStory(Constants.ProjectId, story2);
            //// System.Threading.Thread.Sleep(30000); // There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally

            var stories = this.storyService.GetAllStories(Constants.ProjectId, 2, 0);
            Assert.NotNull(stories);
            Assert.AreEqual(2, stories.Data.Count);
            Assert.AreEqual(2, stories.Pagination.Returned);
            Assert.AreEqual(2, stories.Pagination.Total);

            stories = this.storyService.GetAllStories(Constants.ProjectId, 1, 1);
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Data.Count);
            Assert.AreEqual(1, stories.Pagination.Returned);
            Assert.AreEqual(2, stories.Pagination.Total);
        }

        [Test]
        public void CanGetAllStoriesMatchingFilter_FreeForm()
        {
            var story1 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Bug,
                Description   = "some story",
                ProjectId     = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "another story",
                ProjectId     = Constants.ProjectId
            };

            var story3 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "yet another story",
                ProjectId     = Constants.ProjectId
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story1);

            this.storyService.AddNewStory(Constants.ProjectId, story2);
            this.storyService.AddNewStory(Constants.ProjectId, story3);

            System.Threading.Thread.Sleep(5000); // There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = this.storyService.GetAllStoriesMatchingFilter(Constants.ProjectId, "type:bug requester:\"pivotaltrackerdotnet\"");
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test]
        public void CanGetAllStoriesMatchingFilter_Strict()
        {
            var story1 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Bug,
                Description   = "some story",
                ProjectId     = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "another story",
                ProjectId     = Constants.ProjectId
            };

            var story3 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "yet another story",
                ProjectId     = Constants.ProjectId
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story1);
            savedStory.Labels = new List<Label> { "5.1.30", "plume" };
            savedStory = this.storyService.UpdateStory(Constants.ProjectId, savedStory);

            this.storyService.AddNewStory(Constants.ProjectId, story2);
            this.storyService.AddNewStory(Constants.ProjectId, story3);

            System.Threading.Thread.Sleep(5000); // There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = this.storyService.GetAllStoriesMatchingFilter(Constants.ProjectId, FilteringCriteria.FilterBy.Label("5.1.30").Type(StoryType.Bug));
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test]
        public void CanGetAllStoriesMatchingFilterCreatedSince_Strict()
        {
            var story1 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Bug,
                Description   = "some story",
                ProjectId     = Constants.ProjectId
            };

            var story2 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "another story",
                ProjectId     = Constants.ProjectId
            };

            var story3 = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "yet another story",
                ProjectId     = Constants.ProjectId
            };

            var savedStory1 = this.storyService.AddNewStory(Constants.ProjectId, story1);
            savedStory1.Labels = new List<Label> { "5.1.30", "plume" };
            savedStory1 = this.storyService.UpdateStory(Constants.ProjectId, savedStory1);

            var savedStory2 = this.storyService.AddNewStory(Constants.ProjectId, story2);
            var savedStory3 = this.storyService.AddNewStory(Constants.ProjectId, story3);

            System.Threading.Thread.Sleep(5000); // There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = this.storyService.GetAllStoriesMatchingFilter(Constants.ProjectId, FilteringCriteria.FilterBy.CreatedSince(new DateTime(2008, 1, 1)));
            Assert.NotNull(stories);
            Assert.AreEqual(3, stories.Count);

            var savedIds = new[] { savedStory1.Id, savedStory2.Id, savedStory3.Id }.OrderBy(i => i).ToArray();
            var storyIds = stories.Select(s => s.Id).OrderBy(i => i).ToArray();

            Assert.IsTrue(storyIds.SequenceEqual(savedIds));
        }

        [Test]
        public void CanRetrieveIceBoxStories()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId,
                CurrentState  = StoryStatus.Unscheduled,
                Estimate      = 2
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);
            System.Threading.Thread.Sleep(5000); // There is a lag in pivotal tracker's filter search. removing the slepp will cause the test to fail occasionally
            var stories = this.storyService.GetIceboxStories(Constants.ProjectId);
            Assert.NotNull(stories);
            Assert.AreEqual(1, stories.Count);
            Assert.AreEqual(savedStory.Id, stories[0].Id);
        }

        [Test]
        public void CanAddAndDeleteStories()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId,
                Estimate      = 2
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);
            Assert.AreEqual(story.Name, savedStory.Name);
            Assert.AreEqual(Constants.ProjectId, savedStory.ProjectId);
            Assert.AreEqual(story.RequestedById, savedStory.RequestedById);
            Assert.AreEqual(story.StoryType, savedStory.StoryType);
            Assert.AreEqual(story.Description, savedStory.Description);
            Assert.AreEqual(2, savedStory.Estimate);

            this.storyService.RemoveStory(Constants.ProjectId, savedStory.Id);
        }

        [Test]
        public void CanUpdateStory()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId,
                Estimate      = 2
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);
            savedStory.Name        = "Call be New name";
            savedStory.Description = "wololo";
            savedStory.Estimate    = 1;
            savedStory.Labels      = new List<Label> { "laby hh", "pool" };

            var updatedStory = this.storyService.UpdateStory(Constants.ProjectId, savedStory);
            VerifyStory(savedStory, updatedStory);
        }

        [Test]
        public void CanSaveTask()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);

            var task = this.storyService.AddNewTask(new Task { Description = "stuff stuff stuff", StoryId = savedStory.Id, ProjectId = Constants.ProjectId });

            var guid = Guid.NewGuid().ToString();

            task.Description = guid;

            var savedTask = this.storyService.SaveTask(task);
            Assert.AreEqual(task.Description, savedTask.Description);
            Assert.AreEqual(task.ProjectId, savedTask.ProjectId);
            Assert.AreEqual(task.StoryId, savedTask.StoryId);

            var tasks = this.storyService.GetTasksForStory(Constants.ProjectId, savedStory);

            Assert.AreEqual(guid, tasks[0].Description);
        }

        [Test]
        public void CanAddGetAndDeleteNewTasks()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);

            var task = new Task { Description = "stuff stuff stuff", StoryId = savedStory.Id, ProjectId = Constants.ProjectId };

            var savedTask = this.storyService.AddNewTask(task);
            Assert.AreEqual(task.Description, savedTask.Description);

            var retrievedTask = this.storyService.GetTask(Constants.ProjectId, savedTask.StoryId, savedTask.Id);
            Assert.NotNull(retrievedTask);

            Assert.IsTrue(this.storyService.RemoveTask(retrievedTask.ProjectId, task.StoryId, retrievedTask.Id));
        }

        [Test]
        public void CanGetAllIterations()
        {
            var story = new Story
            {
                Name = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType = StoryType.Feature,
                Description = "bla bla bla and more bla",
                ProjectId = Constants.ProjectId,
                Deadline = DateTimeOffset.Now,
                CurrentState = StoryStatus.Unstarted
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);

            var task = this.storyService.AddNewTask(new Task { Description = "stuff stuff stuff", StoryId = savedStory.Id, ProjectId = Constants.ProjectId });
            this.storyService.AddComment(Constants.ProjectId, savedStory.Id, "A comment");

            var iterations = this.storyService.GetAllIterations(Constants.ProjectId);
            Assert.Greater(iterations.Count, 0);
            Assert.NotNull(iterations[0].Start);
            Assert.NotNull(iterations[0].Stories);
            Assert.Greater(iterations[0].Stories.Count, 0);

            Assert.IsNull(iterations[0].Stories[0].Followers);
            Assert.IsNull(iterations[0].Stories[0].Comments);
            Assert.IsNull(iterations[0].Stories[0].Tasks);
            Assert.IsNull(iterations[0].Stories[0].RequestedBy);
        }

        [Test]
        public void CanGetAllIterationsWithStoryFields()
        {
            var story = new Story
            {
                Name          = "Nouvelle histoire",
                RequestedById = Constants.UserId,
                StoryType     = StoryType.Feature,
                Description   = "bla bla bla and more bla",
                ProjectId     = Constants.ProjectId,
                Deadline      = DateTimeOffset.Now,
                CurrentState  = StoryStatus.Unstarted
            };

            var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);

            var task = this.storyService.AddNewTask(new Task { Description = "stuff stuff stuff", StoryId = savedStory.Id, ProjectId = Constants.ProjectId });
            this.storyService.AddComment(Constants.ProjectId, savedStory.Id, "A comment");

            var iterations = this.storyService.GetAllIterations(Constants.ProjectId, StoryIncludeFields.Owners | 
                                                                                     StoryIncludeFields.OwnerIds | 
                                                                                     StoryIncludeFields.Followers | 
                                                                                     StoryIncludeFields.FollowerIds | 
                                                                                     StoryIncludeFields.Comments | 
                                                                                     StoryIncludeFields.Tasks |
                                                                                     StoryIncludeFields.RequestedBy);
            Assert.Greater(iterations.Count, 0);
            Assert.NotNull(iterations[0].Start);
            Assert.NotNull(iterations[0].Stories);
            Assert.Greater(iterations[0].Stories.Count, 0);
            Assert.NotNull(iterations[0].Stories[0].Followers);
            Assert.NotNull(iterations[0].Stories[0].Comments);
            Assert.NotNull(iterations[0].Stories[0].Tasks);
            Assert.Greater(iterations[0].Stories[0].Followers.Count, 0);
            Assert.Greater(iterations[0].Stories[0].Comments.Count, 0);
            Assert.Greater(iterations[0].Stories[0].Tasks.Count, 0);
            Assert.NotNull(iterations[0].Stories[0].RequestedBy);
        }

        [Test]
        [ExpectedException(typeof(PivotalTrackerResourceNotFoundException))]
        public void StoryNotFoundThrowsException()
        {
            var result = this.storyService.GetStory(Constants.ProjectId, 1234);
        }

        [Test]
        public void ActivityProcessingTest()
        {
            int storyId;

            {
                var story = new Story
                {
                    Name = "Nouvelle histoire",
                    RequestedById = Constants.UserId,
                    StoryType = StoryType.Feature,
                    Description = "bla bla bla and more bla",
                    ProjectId = Constants.ProjectId
                };

                var savedStory = this.storyService.AddNewStory(Constants.ProjectId, story);

                var task1 = new Task { Description = "stuff stuff stuff", StoryId = savedStory.Id, ProjectId = Constants.ProjectId };
                var task2 = new Task { Description = "stuff stuff stuff", StoryId = savedStory.Id, ProjectId = Constants.ProjectId };

                var savedTask1 = this.storyService.AddNewTask(task1);

                savedStory.Labels = new List<Label>() { "Label1", "Label2" };
                savedStory.Estimate = 3;

                savedStory = this.storyService.UpdateStory(Constants.ProjectId, savedStory);

                var savedTask2 = this.storyService.AddNewTask(task2);

                this.storyService.AddComment(savedStory.ProjectId, savedStory.Id, "Comment 1");
                this.storyService.AddComment(savedStory.ProjectId, savedStory.Id, "Comment 2");

                savedStory.Name     = "New story";
                savedStory.Deadline = DateTimeOffset.Now.AddDays(10);
                savedStory.Estimate = 3;

                savedStory = this.storyService.UpdateStory(Constants.ProjectId, savedStory);

                storyId = savedStory.Id;
            }

            var activities = this.storyService.GetStoryActivity(Constants.ProjectId, storyId);

            activities = activities.OrderBy(a => a.OccurredAt).ToList();

            var accumulatedStoryValues      = new Dictionary<string, object>();
            var accumulatedTasksValues      = new Dictionary<int, Dictionary<string, object>>();
            var accumulatedCommentValues    = new Dictionary<int, Dictionary<string, object>>();
            var accumulatedLabelValues      = new Dictionary<int, Dictionary<string, object>>();

            var deserializer = new DictionaryDeserializer();

            var foundObjects = new List<object>();

            foreach (var activity in activities)
            {
                foreach (var change in activity.Changes)
                {
                    if (change.NewValues != null && change.NewValues.Count == 1 && change.NewValues.ContainsKey("updated_at"))
                        continue; // Skip story changes without any significat changes

                    switch (change.Kind)
                    {
                        case ResourceKind.Story:
                        {
                            if (change.NewValues != null)
                                change.NewValues.ToList().ForEach(kp => accumulatedStoryValues[kp.Key] = kp.Value);
                            else if (change.OriginalValues != null)
                                change.OriginalValues.ToList().ForEach(kp => accumulatedStoryValues[kp.Key] = kp.Value);

                            var story = deserializer.Deserialize<Story>(accumulatedStoryValues);

                            if (activity.OccurredAt != DateTimeOffset.MinValue || 
                                (change.NewValues != null && !change.NewValues.ContainsKey("updated_at")))
                            {
                                story.UpdatedAt = activity.OccurredAt;
                            }

                            foundObjects.Add(story);
                            break;
                        }

                        case ResourceKind.Comment:
                        {
                            Dictionary<string, object> values;
                            if (!accumulatedTasksValues.TryGetValue(change.Id, out values))
                            {
                                values = accumulatedTasksValues[change.Id] = new Dictionary<string, object>();
                            }

                            if (change.NewValues != null)
                                change.NewValues.ToList().ForEach(kp => values[kp.Key] = kp.Value);
                            else if (change.OriginalValues != null)
                                change.OriginalValues.ToList().ForEach(kp => values[kp.Key] = kp.Value);

                            var comment = deserializer.Deserialize<Comment>(values);

                            if (activity.OccurredAt != DateTimeOffset.MinValue || 
                                (change.NewValues != null && !change.NewValues.ContainsKey("updated_at")))
                            {
                                comment.UpdatedAt = activity.OccurredAt;
                            }

                            foundObjects.Add(comment);
                            break;
                        }

                        case ResourceKind.Task:
                        {
                            Dictionary<string, object> values;
                            if (!accumulatedCommentValues.TryGetValue(change.Id, out values))
                            {
                                values = accumulatedCommentValues[change.Id] = new Dictionary<string, object>();
                            }

                            if (change.NewValues != null)
                                change.NewValues.ToList().ForEach(kp => values[kp.Key] = kp.Value);
                            else if (change.OriginalValues != null)
                                change.OriginalValues.ToList().ForEach(kp => values[kp.Key] = kp.Value);

                            var task = deserializer.Deserialize<Task>(values);

                            if (activity.OccurredAt != DateTimeOffset.MinValue || 
                                (change.NewValues != null && !change.NewValues.ContainsKey("updated_at")))
                            {
                                task.UpdatedAt = activity.OccurredAt;
                            }

                            foundObjects.Add(task);
                            break;
                        }

                        case ResourceKind.Label:
                        {
                            Dictionary<string, object> values;
                            if (!accumulatedLabelValues.TryGetValue(change.Id, out values))
                            {
                                values = accumulatedLabelValues[change.Id] = new Dictionary<string, object>();
                            }

                            if (change.NewValues != null)
                                change.NewValues.ToList().ForEach(kp => values[kp.Key] = kp.Value);
                            else if (change.OriginalValues != null)
                                change.OriginalValues.ToList().ForEach(kp => values[kp.Key] = kp.Value);

                            var label = deserializer.Deserialize<Label>(values);

                            if (activity.OccurredAt != DateTimeOffset.MinValue || 
                                (change.NewValues != null && !change.NewValues.ContainsKey("updated_at")))
                            {
                                label.UpdatedAt = activity.OccurredAt;
                            }

                            foundObjects.Add(label);
                            break;
                        }

                        default:
                            throw new Exception("Unhandled resource kind: " + change.Kind);
                    }
                }
            }

            Assert.IsNotEmpty(foundObjects);
            Assert.AreEqual(4, foundObjects.Where(o => o is Story).Count());
            Assert.AreEqual(2, foundObjects.Where(o => o is Comment).Count());
            Assert.AreEqual(2, foundObjects.Where(o => o is Task).Count());
            Assert.AreEqual(2, foundObjects.Where(o => o is Label).Count());
        }

        private static void VerifyStory(Story expected, Story actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(Constants.ProjectId, actual.ProjectId);
            Assert.AreEqual(expected.RequestedById, actual.RequestedById);
            Assert.AreEqual(expected.StoryType, actual.StoryType);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Estimate, actual.Estimate);
            Assert.AreEqual(expected.Labels.Count, actual.Labels.Count);
            Assert.AreEqual(expected.Labels, actual.Labels);
        }
    }

    // ReSharper restore InconsistentNaming
}