PivotalTracker.NET
==================

PivotalTracker.NET is a wrapper around the [Rest API](https://www.pivotaltracker.com/help/api) provided by Pivotal Labs, the makers of [Pivotal Track](https://www.pivotaltracker.com).

With it, developers can retrieve projects, create stories, delete stories, edit stories, add tasks to stories, remove tasks to stories, edit tasks, and add comment to stories.


Sample Usage
-------------

### Authenticating

```csharp
AuthenticationToken authenticationToken = AuthenticationService.Authenticate("user", "pass");
```

### Retrieving All Stories

```csharp
long projectId = 1123;
var  authenticationToken = AuthenticationService.Authenticate("myUsername", "myPassword");
var stories = new StoryService(authenticationToken).GetAllStories(projectId);
```

### Creating a New Story

```csharp
long projectId = 1123;
var story = new Story {
                      Name = "Nouvelle histoire",
                      RequestedBy = "pivotaltrackerdotnet",
                      StoryType = StoryType.Feature,
                      Description = "bla bla bla and more bla",
                      ProjectId = Constants.ProjectId,
                      Estimate = 2
                    };
                    
var authenticationToken = AuthenticationService.Authenticate("myUsername", "myPassword");
var storyService = new StoryService(authenticationToken);

var savedStory = storyService.AddNewStory(projectId,story);

```

The documentation will <i><b>'soon'</b></i> be available on the project's wiki.