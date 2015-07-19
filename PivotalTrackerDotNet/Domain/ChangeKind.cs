namespace PivotalTrackerDotNet.Domain
{
    public enum ChangeKind
    {
        ProjectCreateActivity,
        ProjectDeleteActivity,

        TaskMoveActivity,

        CommentCreateActivity,
        CommentDeleteActivity,
        CommentUpdateActivity,
        EpicCreateActivity,
        EpicDeleteActivity,
        EpicMoveActivity,
        EpicUpdateActivity,
        FollowerCreateActivity,
        FollowerDeleteActivity,
        IterationUpdateActivity,
        LabelCreateActivity,
        LabelDeleteActivity,
        LabelUpdateActivity,
        ModelImportActivity,
        ProjectMembershipCreateActivity,
        ProjectMembershipDeleteActivity,
        ProjectMembershipUpdateActivity,
        ProjectUpdateActivity,
        StoryC2genericcommandActivity,
        StoryCreateActivity,
        StoryDeleteActivity,
        StoryMoveActivity,
        StoryMoveFromProjectActivity,
        StoryMoveIntoProjectActivity,
        StoryMoveIntoProjectAndPrioritizeActivity,
        StoryUpdateActivity,
        TaskCreateActivity,
        TaskDeleteActivity,
        TaskUpdateActivity
    }
}