using System;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class PivotalTrackerClient : IPivotalTrackerClient
    {
        private readonly string token;
        private readonly Lazy<IAuthenticationService> authenticationService;
        private readonly Lazy<IAccountService> accountService;
        private readonly Lazy<IStoryService> storyService;
        private readonly Lazy<IMembershipService> membershipsService;
        private readonly Lazy<IProjectService> projectService;

        public PivotalTrackerClient(string token) : this()
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("token cannot be null or empty.", "token");

            this.token = token;
        }

        public PivotalTrackerClient(AuthenticationToken token) : this((string)token)
        {
        }

        public PivotalTrackerClient(string username, string password) : this()
        {
            this.token = AuthenticationService.Authenticate(username, password);
        }

        private PivotalTrackerClient()
        {
            this.authenticationService  = new Lazy<IAuthenticationService>(() => new AuthenticationService(this.token));
            this.accountService         = new Lazy<IAccountService>(() => new AccountService(this.token));
            this.storyService           = new Lazy<IStoryService>(() => new StoryService(this.token));
            this.membershipsService     = new Lazy<IMembershipService>(() => new MembershipService(this.token));
            this.projectService         = new Lazy<IProjectService>(() => new ProjectService(this.token));
        }

        public IAuthenticationService Authentication
        {
            get { return this.authenticationService.Value; }
        }

        public IAccountService Account
        {
            get { return this.accountService.Value; }
        }

        public IStoryService Stories
        {
            get { return this.storyService.Value; }
        }

        public IMembershipService Memberships
        {
            get { return this.membershipsService.Value; }
        }

        public IProjectService Projects
        {
            get { return this.projectService.Value; }
        }
    }
}
