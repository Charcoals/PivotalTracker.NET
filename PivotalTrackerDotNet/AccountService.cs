using System.Collections.Generic;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet
{
    public class AccountService : AAuthenticatedService, IAccountService
    {
        private const string AccountsEndpoint               = "accounts";
        private const string SingleAccountEndpoint          = "accounts/{0}";
        private const string AccountMembershipsEndpoint     = SingleAccountEndpoint + "/memberships";
        private const string AccountSummariesEndpoint       = "account_summaries";

        public AccountService(string token)
            : base(token)
        {
        }

        public AccountService(AuthenticationToken token)
            : base(token)
        {
        }

        public List<Account> GetAccounts()
        {
            var request = BuildGetRequest(AccountsEndpoint);
            request.AddQueryParameter("fields", ":default,project_ids");

            return RestClient.ExecuteRequestWithChecks<List<Account>>(request);
        }

        public Account GetAccount(int accountId)
        {
            var request = BuildGetRequest(string.Format(SingleAccountEndpoint, accountId));
            request.AddQueryParameter("fields", ":default,project_ids");

            return RestClient.ExecuteRequestWithChecks<Account>(request);
        }

        public List<AccountSummary> GetAccountSummaries()
        {
            var request = BuildGetRequest(AccountSummariesEndpoint);

            return RestClient.ExecuteRequestWithChecks<List<AccountSummary>>(request);
        }

        public List<AccountMembership> GetAccountMemberships(int accountId)
        {
            var request = BuildGetRequest(string.Format(AccountMembershipsEndpoint, accountId));

            return RestClient.ExecuteRequestWithChecks<List<AccountMembership>>(request);
        }
    }
}
