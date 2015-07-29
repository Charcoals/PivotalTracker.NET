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
        private const string StoryActivityEndpoint         = "projects/{0}/stories/{1}/activity";
        private const string StoryCommentsEndpoint         = "projects/{0}/stories/{1}/comments";
        private const string SaveNewCommentEndpoint        = "projects/{0}/stories/{1}/comments?text={2}";
        private const string SingleTaskEndpoint            = "projects/{0}/stories/{1}/tasks/{2}";
        private const string StoryStateEndpoint            = "projects/{0}/stories/{1}?story[current_state]={2}";
        private const string StoryFilterEndpoint           = StoriesEndpoint + "?filter={1}";
        private const string IterationEndPoint             = "projects/{0}/iterations";
        private const string IterationRecentEndPoint       = IterationEndPoint + "/done?offset=-{1}";

        public StoryService(string token) : base(token)
        {
        }

        public StoryService(AuthenticationToken token) : base(token)
        {
        }

        public List<Story> GetAllStories(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);

            return this.GetStories(request);
        }

        public List<Story> GetAllStories(int projectId, StoryIncludeFields fields)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoriesEndpoint, projectId);

            string fieldsQuery = ":default";

            var fieldsToInclude = this.GetFieldsNames(fields);
            
            if (fieldsToInclude.Any())
                fieldsQuery += "," + string.Join(",", fieldsToInclude);
            
            request.AddQueryParameter("fields", fieldsQuery);

            return this.GetStories(request);
        }

        public PagedResult<Story> GetAllStories(int projectId, int limit, int offset)
        {
            var request = this.BuildGetRequest(string.Format(StoriesEndpoint, projectId))
                              .SetPagination(offset, limit);

            return this.RestClient.ExecuteRequestWithChecks<PagedResult<Story>>(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterEndpoint, projectId, filter);

            return this.GetStories(request);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, StoryIncludeFields fields)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(StoryFilterEndpoint, projectId, filter);

            string fieldsQuery = ":default";

            var fieldsToInclude = this.GetFieldsNames(fields);
            
            if (fieldsToInclude.Any())
                fieldsQuery += "," + string.Join(",", fieldsToInclude);
            
            request.AddQueryParameter("fields", fieldsQuery);

            return this.GetStories(request);
        }

        public PagedResult<Story> GetAllStoriesMatchingFilter(int projectId, string filter, int limit, int offset)
        {
            var request = this.BuildGetRequest(string.Format(StoryFilterEndpoint, projectId, filter))
                              .SetPagination(offset, limit);

            return this.RestClient.ExecuteRequestWithChecks<PagedResult<Story>>(request);
        }

        public PagedResult<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, int limit, int offset)
        {
            return this.GetAllStoriesMatchingFilter(projectId, filter.ToString(), limit, offset);
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter)
        {
            return this.GetAllStoriesMatchingFilter(projectId, filter.ToString());
        }

        public List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, StoryIncludeFields fields)
        {
            return this.GetAllStoriesMatchingFilter(projectId, filter.ToString(), fields);
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
            var request = BuildGetRequest(string.Format(SingleStoryEndpoint, projectId, storyId));

            return RestClient.ExecuteRequestWithChecks<Story>(request);
        }

        public Story GetStory(int projectId, int storyId, StoryIncludeFields fields)
        {
            var request = BuildGetRequest(string.Format(SingleStoryEndpoint, projectId, storyId));
            
            string fieldsQuery = ":default";

            var fieldsToInclude = this.GetFieldsNames(fields);
            
            if (fieldsToInclude.Any())
                fieldsQuery += "," + string.Join(",", fieldsToInclude);
            
            request.AddQueryParameter("fields", fieldsQuery);

            return RestClient.ExecuteRequestWithChecks<Story>(request);
        }

        public List<Iteration> GetAllIterations(int projectId)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationEndPoint, projectId);
            
            return this.GetIterations(request);
        }

        public List<Iteration> GetAllIterations(int projectId, StoryIncludeFields fields)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationEndPoint, projectId);

            string fieldsQuery = ":default";

            var fieldsToInclude = this.GetFieldsNames(fields);
            
            if (fieldsToInclude.Any())
                fieldsQuery += "," + string.Join(",", fieldsToInclude);
            
            request.AddQueryParameter("fields", ":default,stories(" + fieldsQuery + ")");

            return this.GetIterations(request);
        }

        public PagedResult<Iteration> GetAllIterations(int projectId, int limit, int offset)
        {
            var request = this.BuildGetRequest(string.Format(IterationEndPoint, projectId))
                              .SetPagination(offset, limit);

            return this.RestClient.ExecuteRequestWithChecks<PagedResult<Iteration>>(request);
        }

        public List<Iteration> GetLastIterations(long projectId, int number)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(IterationRecentEndPoint, projectId, number);
            return this.GetIterations(request);
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
            return this.GetAllStoriesMatchingFilter(projectId, FilteringCriteria.FilterBy.State(StoryStatus.Unscheduled));
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

        public List<Task> GetTasksForStory(int projectId, int storyId)
        {
            var request = this.BuildGetRequest();
            request.Resource = string.Format(TaskEndpoint, projectId, storyId);
            return RestClient.ExecuteRequestWithChecks<List<Task>>(request);
        }

        public List<Task> GetTasksForStory(int projectId, Story story)
        {
            return this.GetTasksForStory(projectId, story.Id);
        }

        public void AddComment(int projectId, int storyId, string comment)
        {
            var request = BuildPostRequest();
            request.Resource = string.Format(SaveNewCommentEndpoint, projectId, storyId, comment);
            RestClient.ExecuteRequestWithChecks(request);
        }

        public List<Activity> GetStoryActivity(int projectId, int storyId)
        {
            var request = this.BuildGetRequest(string.Format(StoryActivityEndpoint, projectId, storyId));

            return RestClient.ExecuteRequestWithChecks<List<Activity>>(request);
        }

        public PagedResult<Activity> GetStoryActivity(int projectId, int storyId, int offset, int limit)
        {
            var request = this.BuildGetRequest(string.Format(StoryActivityEndpoint, projectId, storyId))
                              .SetPagination(offset, limit);

            return RestClient.ExecuteRequestWithChecks<PagedResult<Activity>>(request);
        }

        public List<Comment> GetStoryComments(int projectId, int storyId)
        {
            var request = this.BuildGetRequest(string.Format(StoryCommentsEndpoint, projectId, storyId));

            return RestClient.ExecuteRequestWithChecks<List<Comment>>(request);
        }

        private List<Iteration> GetIterationsByType(int projectId, string iterationType)
        {
            var request = BuildGetRequest();
            request.Resource = string.Format(SpecifiedIterationEndpoint, projectId, iterationType);
            return this.GetIterations(request);
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

        private List<Iteration> GetIterations(RestRequest request)
        {
            return RestClient.ExecuteRequestWithChecks<List<Iteration>>(request);
        }

        private List<Story> GetStories(RestRequest request)
        {
            return RestClient.ExecuteRequestWithChecks<List<Story>>(request);
            //var el = RestClient.ExecuteRequestWithChecks<List<Story>>(request);

            //var stories = new Stories();
            //stories.AddRange(el.Select(storey => storey.ToObject<Story>()));
            //return stories;
        }

        private IEnumerable<string> GetFieldsNames(StoryIncludeFields fields)
        {
            if (fields.HasFlag(StoryIncludeFields.AfterId))
                yield return "after_id";

            if (fields.HasFlag(StoryIncludeFields.BeforeId))
                yield return "before_id";

            if (fields.HasFlag(StoryIncludeFields.Comments))
            {
                yield return "comments(:default,person,file_attachments,google_attachments)";
            }
            else if (fields.HasFlag(StoryIncludeFields.CommentIds))
                yield return "comment_ids";

            if (fields.HasFlag(StoryIncludeFields.Followers))
                yield return "followers";
            else if (fields.HasFlag(StoryIncludeFields.FollowerIds))
                yield return "follower_ids";

            if (fields.HasFlag(StoryIncludeFields.Tasks))
                yield return "tasks";
            else if (fields.HasFlag(StoryIncludeFields.TaskIds))
                yield return "task_ids";

            if (fields.HasFlag(StoryIncludeFields.Owners))
                yield return "owners";
            else if (fields.HasFlag(StoryIncludeFields.OwnerIds))
                yield return "owner_ids";

            if (fields.HasFlag(StoryIncludeFields.RequestedBy))
                yield return "requested_by";
        }
    }
}