using System.Collections.Generic;
using System.Linq;
using PivotalTrackerDotNet.Domain;
using RestSharp;
using Parallel = System.Threading.Tasks.Parallel;

namespace PivotalTrackerDotNet
{
    public class StoryService : AAuthenticatedService, IStoryService
    {
        private const string SpecifiedIterationEndpoint    = "projects/{0}/iterations?scope={1}";
        private const string SingleStoryEndpoint           = "projects/{0}/stories/{1}";
        private const string StoriesEndpoint               = "projects/{0}/stories";
        private const string TaskEndpoint                  = "projects/{0}/stories/{1}/tasks";
        private const string SaveNewCommentEndpoint        = "projects/{0}/stories/{1}/notes?note[text]={2}";
        private const string SingleTaskEndpoint            = "projects/{0}/stories/{1}/tasks/{2}"; // projects/$PROJECT_ID/stories/$STORY_ID/tasks/$TASK_ID
        private const string StoryStateEndpoint            = "projects/{0}/stories/{1}?story[current_state]={2}";
        private const string StoryFilterEndpoint           = StoriesEndpoint + "?filter={1}";
        private const string StoryPaginationEndpoint       = StoriesEndpoint + "?limit={1}&offset={2}";
        private const string StoryFilterPaginationEndpoint = StoryFilterEndpoint + "&limit={2}&offset={3}";
        private const string IterationEndPoint             = "projects/{0}/iterations";
        private const string IterationPaginationEndPoint   = IterationEndPoint + "?offset={1}&limit={2}";
        private const string IterationRecentEndPoint       = IterationEndPoint + "/done?offset=-{1}";

        public StoryService(string token)
            : base(token)
        {
        }

        public List<Story> GetAllStories(int projectId, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);

            return this.GetStories(request);
        }

        public List<Story> GetAllStories(int projectId, int limit, int offset, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryPaginationEndpoint, projectId, limit, offset);

            return this.GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterEndpoint, projectId, filter);

            return this.GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, int limit, int offset, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterPaginationEndpoint, projectId, filter, limit, offset);

            return this.GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, int limit, int offset)
        {
            return this.GetAllStoriesMatchingFilter(projectId, filter.ToString(), limit, offset);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter)
        {
            return this.GetAllStoriesMatchingFilter(projectId, filter.ToString());
        }

        public Story FinishStory(int projectId, int storyId)
        {
            var originalStory = this.GetStory(projectId, storyId);
            string finished = originalStory.StoryType == StoryType.Chore ? "accepted" : "finished";

            var request = BuildPutRequest();
            request.Resource = string.Format(StoryStateEndpoint, projectId, storyId, finished);

            var response = RestClient.Execute<Story>(request);
            var story = response.Data;

            return story;
        }

        public Story StartStory(int projectId, int storyId)
        {
            var request = BuildPutRequest();
            request.Resource = string.Format(StoryStateEndpoint, projectId, storyId, "started");

            var response = RestClient.Execute<Story>(request);
            var story = response.Data;

            return story;
        }

        public Story GetStory(int projectId, int storyId)
        {
            return this.FindStory(projectId, storyId);
        }

        public List<Iteration> GetAllIterations(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationEndPoint, projectId);

            return this.GetIteration(request);
        }

        public List<Iteration> GetAllIterations(int projectId, int limit, int offset)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationPaginationEndPoint, projectId, offset, limit);

            return this.GetIteration(request);
        }

        public List<Iteration> GetLastIterations(long projectId, int number)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationRecentEndPoint, projectId, number);
            return this.GetIteration(request);
        }

        public List<Iteration> GetCurrentIterations(int projectId)
        {
            return this.GetIterationsByType(projectId, "current");
        }

        public List<Iteration> GetDoneIterations(int projectId)
        {
            return this.GetIterationsByType(projectId, "done");
        }

        public List<Iteration> GetBacklogIterations(int projectId)
        {
            return this.GetIterationsByType(projectId, "backlog");
        }

        public List<Story> GetCurrentStories(int projectId)
        {
            return this.GetStoriesByIterationType(projectId, "current");
        }

        public List<Story> GetDoneStories(int projectId)
        {
            return this.GetStoriesByIterationType(projectId, "done");
        }

        public List<Story> GetIceboxStories(int projectId)
        {
            return this.GetAllStoriesMatchingFilter(projectId, "state:unscheduled");
        }

        public List<Story> GetBacklogStories(int projectId)
        {
            return this.GetStoriesByIterationType(projectId, "backlog");
        }

        public void RemoveStory(int projectId, int storyId)
        {
            var request = BuildDeleteRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, storyId);

            RestClient.ExecuteRequestWithChecks(request);
        }

        public Story AddNewStory(int projectId, Story toBeSaved)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);
            request.AddParameter("application/json", toBeSaved.ToJson(), ParameterType.RequestBody);

            return RestClient.ExecuteRequestWithChecks<Story>(request);
        }

        public Story UpdateStory(int projectId, Story story)
        {
            var request = BuildPutRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, story.Id);
            request.AddParameter("application/json", story.ToJson(), ParameterType.RequestBody);

            return RestClient.ExecuteRequestWithChecks<Story>(request);
        }

        public Task SaveTask(Task task)
        {
            var request = BuildPutRequest();
            request.Resource = string.Format(SingleTaskEndpoint, task.ProjectId, task.StoryId, task.Id);
            request.AddParameter("application/json", task.ToJson(), ParameterType.RequestBody);
            var savedTask = RestClient.ExecuteRequestWithChecks<Task>(request);
            savedTask.ProjectId = task.ProjectId;
            return savedTask;
        }

        public void ReorderTasks(int projectId, int storyId, List<Task> tasks)
        {
            Parallel.ForEach(tasks, t =>
            {
                var request = BuildPutRequest();
                request.Resource = string.Format(TaskEndpoint + "/{2}?task[position]={3}", t.ProjectId, t.StoryId, t.Id, t.Position);
                RestClient.ExecuteRequestWithChecks(request);
            });
        }

        public Task AddNewTask(Task task)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(TaskEndpoint, task.ProjectId, task.StoryId);
            request.AddParameter("application/json", task.ToJson(), ParameterType.RequestBody);
            var savedTask = RestClient.ExecuteRequestWithChecks<Task>(request);
            savedTask.ProjectId = task.ProjectId;
            return savedTask;
        }

        public bool RemoveTask(int projectId, int storyId, int taskId)
        {
            var request = BuildDeleteRequest();
            request.Resource = string.Format(SingleTaskEndpoint, projectId, storyId, taskId);
            var response = RestClient.ExecuteRequestWithChecks<Task>(request);
            return response == null;
        }

        public Task GetTask(int projectId, int storyId, int taskId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleTaskEndpoint, projectId, storyId, taskId);

            var output = RestClient.ExecuteRequestWithChecks<Task>(request);
            output.StoryId = storyId;
            output.ProjectId = projectId;
            return output;
        }

        public List<Task> GetTasksForStory(int projectId, Story story)
        {
            var request = this.BuildGetRequest();
            request.Resource = string.Format(TaskEndpoint, projectId, story.Id);
            return RestClient.ExecuteRequestWithChecks<List<Task>>(request);
        }

        public void AddComment(int projectId, int storyId, string comment)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(SaveNewCommentEndpoint, projectId, storyId, comment);
            RestClient.ExecuteRequestWithChecks(request);
        }

        private Story FindStory(int projectId, int storyId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, storyId);

            return RestClient.ExecuteRequestWithChecks<Story>(request);
        }

        private List<Iteration> GetIterationsByType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);
            return this.GetIteration(request);
        }

        private List<Story> GetStoriesByIterationType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);
            var el = RestClient.ExecuteRequestWithChecks(request);

            var stories = new Stories();
            stories.AddRange(el[0]["stories"].Select(storey => storey.ToObject<Story>()));
            return stories;
        }

        private List<Iteration> GetIteration(RestRequest request)
        {
            var el = RestClient.ExecuteRequestWithChecks(request);
            var iterations = new List<Iteration>();
            iterations.AddRange(el.Select(iteration => iteration.ToObject<Iteration>()));
            return iterations;
        }

        private List<Story> GetStories(RestRequest request)
        {
            var el = RestClient.ExecuteRequestWithChecks(request);

            var stories = new Stories();
            stories.AddRange(el.Select(storey => storey.ToObject<Story>()));
            return stories;
        }
    }
}