using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IStoryService
    {
        List<Iteration> GetAllIterations(int projectId);
        PagedResult<Iteration> GetAllIterations(int projectId, int limit, int offset);
        List<Iteration> GetLastIterations(long projectId, int number);
        List<Iteration> GetCurrentIterations(int projectId);
        List<Iteration> GetDoneIterations(int projectId);
        List<Iteration> GetBacklogIterations(int projectId);

        List<Story> GetCurrentStories(int projectId);
        List<Story> GetDoneStories(int projectId);
        List<Story> GetIceboxStories(int projectId);
        List<Story> GetBacklogStories(int projectId);
        List<Story> GetAllStories(int projectId);
        PagedResult<Story> GetAllStories(int projectId, int limit, int offset);
        List<Story> GetAllStoriesMatchingFilter(int projectId, string filter);
        List<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter);
        PagedResult<Story> GetAllStoriesMatchingFilter(int projectId, string filter, int limit, int offset);
        PagedResult<Story> GetAllStoriesMatchingFilter(int projectId, FilteringCriteria filter, int limit, int offset);

        Story AddNewStory(int projectId, Story toBeSaved);
        Story FinishStory(int projectId, int storyId);
        Story StartStory(int projectId, int storyId);
        Story GetStory(int projectId, int storyId);
        void RemoveStory(int projectId, int storyId);
        Story UpdateStory(int projectId, Story story);

        Task AddNewTask(Task task);
        Task GetTask(int projectId, int storyId, int taskId);
        List<Task> GetTasksForStory(int projectId, int storyId);
        List<Task> GetTasksForStory(int projectId, Story story);

        bool RemoveTask(int projectId, int storyId, int taskId);
        Task SaveTask(Task task);
        void ReorderTasks(int projectId, int storyId, List<Task> tasks);

        void AddComment(int projectId, int storyId, string comment);

        List<Activity> GetStoryActivity(int projectId, int storyId);
        PagedResult<Activity> GetStoryActivity(int projectId, int storyId, int offset, int limit);
    }
}