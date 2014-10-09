using System.Collections.Generic;
using System.Linq;
using PivotalTrackerDotNet.Domain;
using RestSharp;
using Parallel = System.Threading.Tasks.Parallel;

namespace PivotalTrackerDotNet
{
    public interface IStoryService
    {
        List<Iteration> GetAllIterations(int projectId);
        List<Iteration> GetAllIterations(int projectId, int limit, int offset);
        List<Iteration> GetLastIterations(long projectId, int number);
        List<Iteration> GetCurrentIterations(int projectId);
        List<Iteration> GetDoneIterations(int projectId);
        List<Iteration> GetBacklogIterations(int projectId);

        List<Story> GetCurrentStories(int projectId);
        List<Story> GetDoneStories(int projectId);
        List<Story> GetIceboxStories(int projectId);
        List<Story> GetBacklogStories(int projectId);
        List<Story> GetAllStories(int projectId, bool addTask = true);
        List<Story> GetAllStories(int projectId, int limit, int offset, bool addTask = true);
        List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, bool addTask = true);
        List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter);

        Story AddNewStory(int projectId, Story toBeSaved);
        Story FinishStory(int projectId, int storyId);
        Story StartStory(int projectId, int storyId);
        Story GetStory(int projectId, int storyId);
        void RemoveStory(int projectId, int storyId);
        Story UpdateStory(int projectId, Story story);

        Task AddNewTask(Task task);
        Task GetTask(int projectId, int storyId, int taskId);
        
        bool RemoveTask(int projectId, int storyId, int taskId);
        Task SaveTask(Task task);
        void ReorderTasks(int projectId, int storyId, List<Task> tasks);
        void AddComment(int projectId, int storyId, string comment);
    }

    public class StoryService : AAuthenticatedService, IStoryService
    {
        const string SpecifiedIterationEndpoint = "projects/{0}/iterations?scope={1}";
        const string SingleStoryEndpoint = "projects/{0}/stories/{1}";
        const string StoriesEndpoint = "projects/{0}/stories";
        const string TaskEndpoint = "projects/{0}/stories/{1}/tasks";
        const string SaveNewCommentEndpoint = "projects/{0}/stories/{1}/notes?note[text]={2}";
        const string SingleTaskEndpoint = "projects/{0}/stories/{1}/tasks/{2}";//projects/$PROJECT_ID/stories/$STORY_ID/tasks/$TASK_ID
        const string StoryStateEndpoint = "projects/{0}/stories/{1}?story[current_state]={2}";
        const string StoryFilterEndpoint = StoriesEndpoint + "?filter={1}";
        const string StoryPaginationEndpoint = StoriesEndpoint + "?limit={1}&offset={2}";
        const string StoryFilterPaginationEndpoint = StoryFilterEndpoint + "&limit={2}&offset={3}";
        const string IterationEndPoint = "projects/{0}/iterations";
        const string IterationPaginationEndPoint = IterationEndPoint+"?offset={1}&limit={2}";
        const string IterationRecentEndPoint = IterationEndPoint + "/done?offset=-{1}";

        public StoryService(string token)
            : base(token)
        {
        }

        public List<Story> GetAllStories(int projectId, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);

            return GetStories(request);
        }

        public List<Story> GetAllStories(int projectId, int limit, int offset, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryPaginationEndpoint, projectId, limit, offset);

            return GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterEndpoint, projectId, filter);

            return GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, int limit, int offset, bool addTask = true)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterPaginationEndpoint, projectId, filter, limit, offset);

            return GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, int limit, int offset)
        {
            return GetAllStoriesMatchingFilter(projectId, filter.ToString(), limit, offset);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter)
        {
            return GetAllStoriesMatchingFilter(projectId, filter.ToString());
        }

        public Story FinishStory(int projectId, int storyId)
        {
            var originalStory = GetStory(projectId, storyId);
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
            return FindStory(projectId, storyId);
        }

        public List<Iteration> GetAllIterations(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationEndPoint, projectId);

            return GetIteration(request);
        }

        private List<Iteration> GetIteration(RestRequest request)
        {
            var el = RestClient.ExecuteRequestWithChecks(request);
            var iterations = new List<Iteration>();
            iterations.AddRange(el.Select(iteration => iteration.ToObject<Iteration>()));
            return iterations;
        }

        public List<Iteration> GetAllIterations(int projectId, int limit, int offset)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationPaginationEndPoint, projectId,offset,limit);

            return GetIteration(request);
        }

        public List<Iteration> GetLastIterations(long projectId, int number)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationRecentEndPoint, projectId, number);
            return GetIteration(request);
        }

        public List<Iteration> GetCurrentIterations(int projectId)
        {
            return GetIterationsByType(projectId, "current");
        }

        public List<Iteration> GetDoneIterations(int projectId)
        {
            return GetIterationsByType(projectId, "done");
        }

        public List<Iteration> GetBacklogIterations(int projectId)
        {
            return GetIterationsByType(projectId, "backlog");
        }

        public List<Story> GetCurrentStories(int projectId)
        {
            return GetStoriesByIterationType(projectId, "current");
        }

        public List<Story> GetDoneStories(int projectId)
        {
            return GetStoriesByIterationType(projectId, "done");
        }

        public List<Story> GetIceboxStories(int projectId)
        {
            return GetAllStoriesMatchingFilter(projectId, "state:unscheduled");
        }

        public List<Story> GetBacklogStories(int projectId)
        {
            return GetStoriesByIterationType(projectId, "backlog");
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
                request.Resource = string.Format(TaskEndpoint + "/{2}?task[position]={3}", t.ProjectId,
                                                 t.StoryId, t.Id, t.Position);
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

        public void AddComment(int projectId, int storyId, string comment)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(SaveNewCommentEndpoint, projectId, storyId, comment);
            RestClient.ExecuteRequestWithChecks(request);
        }

        Story FindStory(int projectId, int storyId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, storyId);

            return RestClient.ExecuteRequestWithChecks<Story>(request);
        }

        List<Iteration> GetIterationsByType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);
            return GetIteration(request);
        }

        List<Story> GetStoriesByIterationType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);
            var el = RestClient.ExecuteRequestWithChecks(request);
            
            var stories = new Stories();
            stories.AddRange(el[0]["stories"].Select(storey => storey.ToObject<Story>()));
            return stories;
        }

        List<Story> GetStories(RestRequest request)
        {
            var el = RestClient.ExecuteRequestWithChecks(request);
            
            var stories = new Stories();
            stories.AddRange(el.Select(storey => storey.ToObject<Story>()));
            return stories;
        }

        public List<Task> GetTasksForStory(int projectId, Story story) {
            var request = this.BuildGetRequest();
            request.Resource = string.Format(TaskEndpoint, projectId, story.Id);
            return RestClient.ExecuteRequestWithChecks<List<Task>>(request);
        }
    }
}
