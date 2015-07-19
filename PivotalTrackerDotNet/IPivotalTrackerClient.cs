namespace PivotalTrackerDotNet
{
    public interface IPivotalTrackerClient
    {
        IAuthenticationService Authentication { get; }

        IAccountService Account { get; }

        IStoryService Stories { get; }

        IMembershipService Memberships { get; }

        IProjectService Projects { get; }
    }
}