using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IAuthenticationService
    {
        Me GetAccount();
    }
}