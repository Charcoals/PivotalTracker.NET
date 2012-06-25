using System;
using System.Collections.Generic;
using PivotalTrackerDotNet.Domain;
using RestSharp;
using RestSharp.Contrib;
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
        List<Story> GetAllStories(int projectId);
        List<Story> GetAllStories(int projectId, int limit, int offset);
        List<Story> GetAllStoriesMatchingFilter(int projectId, string filter);
        List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter);

        Story AddNewStory(int projectId, Story toBeSaved);
        Story FinishStory(int projectId, int storyId);
        Story StartStory(int projectId, int storyId);
        Story GetStory(int projectId, int storyId);
        Story RemoveStory(int projectId, int storyId);
        Story UpdateStory(int projectId, Story story);

        Task AddNewTask(Task task);
        Task GetTask(int projectId, int storyId, int taskId);
        
        bool RemoveTask(int projectId, int storyId, int taskId);
        void SaveTask(Task task);
        void ReorderTasks(int projectId, int storyId, List<Task> tasks);
        void AddComment(int projectId, int storyId, string comment);
    }

    public class StoryService : AAuthenticatedService, IStoryService
    {
        const string SpecifiedIterationEndpoint = "projects/{0}/iterations/{1}";
        const string SingleStoryEndpoint = "projects/{0}/stories/{1}";
        const string StoriesEndpoint = "projects/{0}/stories";
        const string TaskEndpoint = "projects/{0}/stories/{1}/tasks";
        const string SaveNewTaskEndpoint = "projects/{0}/stories/{1}/tasks?task[description]={2}";
        const string SaveNewCommentEndpoint = "projects/{0}/stories/{1}/notes?note[text]={2}";
        const string SingleTaskEndpoint = "projects/{0}/stories/{1}/tasks/{2}";//projects/$PROJECT_ID/stories/$STORY_ID/tasks/$TASK_ID
        const string StoryStateEndpoint = "projects/{0}/stories/{1}?story[current_state]={2}";
        const string StoryFilterEndpoint = StoriesEndpoint + "?filter={1}";
        const string StoryPaginationEndpoint = StoriesEndpoint + "?limit={1}&offset={2}";
        const string IterationEndPoint = "projects/{0}/iterations";
        const string IterationPaginationEndPoint = IterationEndPoint+"?offset={1}&limit={2}";
        const string IterationRecentEndPoint = IterationEndPoint + "/done?offset=-{1}";

        public StoryService(AuthenticationToken token)
            : base(token)
        {
        }

        public List<Story> GetAllStories(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);

            return GetStories(projectId, request);
        }

        public List<Story> GetAllStories(int projectId, int limit, int offset)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryPaginationEndpoint, projectId, limit, offset);

            return GetStories(projectId, request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterEndpoint, projectId, filter);

            return GetStories(projectId, request);
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
            var story = FindStory(projectId, storyId);

            return GetStoryWithTasks(projectId, story);
        }

        public List<Iteration> GetAllIterations(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationEndPoint, projectId);

            var response = RestClient.Execute<List<Iteration>>(request);
            return response.Data ?? new List<Iteration>();
        }

        public List<Iteration> GetAllIterations(int projectId, int limit, int offset)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationPaginationEndPoint, projectId,offset,limit);

            var response = RestClient.Execute<List<Iteration>>(request);
            return response.Data ?? new List<Iteration>();
        }

        public List<Iteration> GetLastIterations(long projectId, int number)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationRecentEndPoint, projectId, number);

            var response = RestClient.Execute<List<Iteration>>(request);
            return response.Data ?? new List<Iteration>();
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

        public Story RemoveStory(int projectId, int storyId)
        {
            var request = BuildDeleteRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, storyId);

            var response = RestClient.Execute<Story>(request);
            var story = response.Data;

            return story;
        }

        public Story AddNewStory(int projectId, Story toBeSaved)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);
            request.AddParameter("application/xml", toBeSaved.ToXml(), ParameterType.RequestBody);

            var response = RestClient.Execute<Story>(request);
            return response.Data;
        }

        public Story UpdateStory(int projectId, Story story)
        {
            var toBeUpdated = FindStory(projectId, story.Id);

            var request = BuildPutRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, story.Id);
            request.AddParameter("application/xml", toBeUpdated.GenerateXmlDiff(story), ParameterType.RequestBody);

            var response = RestClient.Execute<Story>(request);
            return response.Data;
        }

        public void SaveTask(Task task)
        {
            var request = BuildPutRequest();
            request.Resource = string.Format(TaskEndpoint + "/{2}?task[description]={3}&task[complete]={4}&task[position]={5}", task.ProjectId, task.ParentStoryId, task.Id, HttpUtility.UrlEncode(task.Description), task.Complete.ToString().ToLower(), task.Position);
            RestClient.Execute(request);
        }

        public void ReorderTasks(int projectId, int storyId, List<Task> tasks)
        {
            Parallel.ForEach(tasks, t =>
            {
                var request = BuildPutRequest();
                request.Resource = string.Format(TaskEndpoint + "/{2}?task[position]={3}", t.ProjectId,
                                                 t.ParentStoryId, t.Id, t.Position);
                RestClient.Execute(request);
            });
        }

        public Task AddNewTask(Task task)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(SaveNewTaskEndpoint, task.ProjectId, task.ParentStoryId, task.Description);
            //request.AddParameter("application/xml", toBeSaved.ToXml(), ParameterType.RequestBody);
            var response = RestClient.Execute<Task>(request);
            var savedTask = response.Data;
            savedTask.ParentStoryId = task.ParentStoryId;
            savedTask.ProjectId = task.ProjectId;
            return savedTask;
        }

        public bool RemoveTask(int projectId, int storyId, int taskId)
        {
            var request = BuildDeleteRequest();
            request.Resource = string.Format(SingleTaskEndpoint, projectId, storyId, taskId);

            var response = RestClient.Execute<Task>(request);
            return response.Data == null;
        }

        public Task GetTask(int projectId, int storyId, int taskId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleTaskEndpoint, projectId, storyId, taskId);

            var response = RestClient.Execute<Task>(request);
            var output = response.Data;
            output.ParentStoryId = storyId;
            output.ProjectId = projectId;
            return output;
        }

        public void AddComment(int projectId, int storyId, string comment)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(SaveNewCommentEndpoint, projectId, storyId, comment);
            RestClient.Execute(request);
        }

        Story FindStory(int projectId, int storyId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SingleStoryEndpoint, projectId, storyId);

            var response = RestClient.Execute<Story>(request);
            var story = response.Data;
            return story;
        }

        List<Iteration> GetIterationsByType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);
            var response = RestClient.Execute<List<Iteration>>(request);
            return response.Data ?? new List<Iteration>();
        }

        List<Story> GetStoriesByIterationType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);

            return GetStories(projectId, request);
        }

        List<Story> GetStories(int projectId, RestRequest request)
        {
            var response = RestClient.Execute<List<Story>>(request);
            var stories = response.Data ?? new List<Story>();
            foreach (var story in stories)
            {
                GetStoryWithTasks(projectId, story);
            }
            return stories;
        }

        Story GetStoryWithTasks(int projectId, Story story)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(TaskEndpoint, projectId, story.Id);
            var taskResponse = RestClient.Execute<List<Task>>(request);
            story.Tasks = taskResponse.Data;
            if (story.Tasks != null)
            {
                story.Tasks.ForEach(e =>
                {
                    e.ParentStoryId = story.Id;
                    e.ProjectId = projectId;
                });
            }
            return story;
        }
    }
}
