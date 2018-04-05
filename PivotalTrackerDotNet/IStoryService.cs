using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IStoryService
    {
        void AddComment(int projectId, int storyId, string comment);
        Story AddNewStory(int projectId, Story toBeSaved);
        Task AddNewTask(Task task);
        Story FinishStory(int projectId, int storyId);
        List<Iteration> GetAllIterations(int projectId);
        PagedResult<Iteration> GetAllIterations(int projectId, int limit, int offset);
        List<Iteration> GetAllIterations(int projectId, StoryIncludeFields fields);
        List<Story> GetAllStories(int projectId);
        PagedResult<Story> GetAllStories(int projectId, int limit, int offset);
        List<Story> GetAllStories(int projectId, StoryIncludeFields fields);
        List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter);
        PagedResult<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, int limit, int offset);
        List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, StoryIncludeFields fields);
        List<Story> GetAllStoriesMatchingFilter(int projectId, string filter);
        PagedResult<Story> GetAllStoriesMatchingFilter(int projectId, string filter, int limit, int offset);
        List<Story> GetAllStoriesMatchingFilter(int projectId, string filter, StoryIncludeFields fields);
        List<Iteration> GetBacklogIterations(int projectId);
        List<Story> GetBacklogStories(int projectId);
        List<Iteration> GetCurrentIterations(int projectId);
        List<Story> GetCurrentStories(int projectId);
        List<Iteration> GetDoneIterations(int projectId);
        List<Story> GetDoneStories(int projectId);
        List<Story> GetIceboxStories(int projectId);
        List<Iteration> GetLastIterations(long projectId, int number);
        Story GetStory(int projectId, int storyId);
        Story GetStory(int projectId, int storyId, StoryIncludeFields fields);
        List<Activity> GetStoryActivity(int projectId, int storyId);
        PagedResult<Activity> GetStoryActivity(int projectId, int storyId, int offset, int limit);
        List<Comment> GetStoryComments(int projectId, int storyId);
        Task GetTask(int projectId, int storyId, int taskId);
        List<Task> GetTasksForStory(int projectId, int storyId);
        List<Task> GetTasksForStory(int projectId, Story story);
        void RemoveStory(int projectId, int storyId);
        bool RemoveTask(int projectId, int storyId, int taskId);
        void ReorderTasks(int projectId, int storyId, List<Task> tasks);
        Task SaveTask(Task task);
        Story StartStory(int projectId, int storyId);
        Story UpdateStory(int projectId, Story story);
    }
}