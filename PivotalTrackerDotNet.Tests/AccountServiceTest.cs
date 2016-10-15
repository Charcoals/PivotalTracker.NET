using System;

using NUnit.Framework;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class AccountServiceTest
    {
        [Test]
        public void CanGetAccounts()
        {
            var service = new AccountService(Constants.ApiToken);
            var accounts = service.GetAccounts();
            Assert.Greater(accounts.Count, 0);
        }

        [Test]
        public void CanGetAccountFromClient()
        {
            var client = new PivotalTrackerClient(Constants.ApiToken);
            var accounts = client.Account.GetAccounts();
            Assert.Greater(accounts.Count, 0);
        }

        [Test]
        public void CanGetAccount()
        {
            var service = new AccountService(Constants.ApiToken);
            var account = service.GetAccount(Constants.AccountId);
            Assert.IsNotNull(account);
        }

        [Test]
        public void CanGetAccountSummaries()
        {
            var service = new AccountService(Constants.ApiToken);
            var accountSummaries = service.GetAccountSummaries();
            Assert.Greater(accountSummaries.Count, 0);
        }

        [Test]
        public void CanGetAccountMemberships()
        {
            var service = new AccountService(Constants.ApiToken);
            var memberships = service.GetAccountMemberships(Constants.AccountId);
            Assert.Greater(memberships.Count, 0);
        }

        private static void VerifyAccount(Me me)
        {
            Assert.IsNotNull(me);
            Assert.AreNotEqual(0, me.Id);
            Assert.IsFalse(string.IsNullOrEmpty(me.Email));
            Assert.IsFalse(string.IsNullOrEmpty(me.Initials));
            Assert.IsFalse(string.IsNullOrEmpty(me.Kind));
            Assert.IsFalse(string.IsNullOrEmpty(me.Name));
            Assert.IsFalse(string.IsNullOrEmpty(me.Username));
            Assert.IsFalse(string.IsNullOrEmpty(me.ApiToken));
            Assert.Greater(me.CreatedAt, DateTimeOffset.MinValue);
            Assert.Greater(me.UpdatedAt, DateTimeOffset.MinValue);
            Assert.IsNotNull(me.TimeZone);
            Assert.IsNotNull(me.TimeZone.Kind);
            Assert.IsNotNull(me.TimeZone.Offset);
            Assert.IsNotNull(me.TimeZone.OlsonName);
            Assert.AreNotEqual(Guid.Empty.ToString(), me.ApiToken);
            Assert.IsNotNull(me.Projects);
            Assert.AreEqual(2, me.Projects.Count);

            foreach (var project in me.Projects)
            {
                Assert.AreNotEqual(0, project.Id);
                Assert.AreNotEqual(0, project.ProjectId);
                Assert.IsFalse(string.IsNullOrEmpty(project.Kind));
                Assert.IsFalse(string.IsNullOrEmpty(project.ProjectName));
                Assert.IsNotNull(project.ProjectColor);
                Assert.IsNotNull(project.Role);
                Assert.Greater(project.LastViewedAt, DateTimeOffset.MinValue);
            }
        }
    }
    // ReSharper restore InconsistentNaming
}