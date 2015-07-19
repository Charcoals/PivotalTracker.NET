using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public interface IAccountService
    {
        List<Account> GetAccounts();

        Account GetAccount(int accountId);

        List<AccountSummary> GetAccountSummaries();

        List<AccountMembership> GetAccountMemberships(int accountId);
    }
}