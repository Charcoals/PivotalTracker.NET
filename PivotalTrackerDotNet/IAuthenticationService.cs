using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IAuthenticationService
    {
        Account GetAccount();
    }
}