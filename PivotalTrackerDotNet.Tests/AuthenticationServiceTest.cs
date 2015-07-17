using System;

using NUnit.Framework;

using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class AuthenticationServiceTest
    {
        [Test]
        public void CanAuthenticateWithValidCredentials()
        {
            var token = AuthenticationService.Authenticate(Constants.Username, Constants.Password);
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Guid);
            Assert.AreNotEqual(Guid.Empty, token.Guid);
        }

        [Test]
        public void CanGetAccount()
        {
            var account = AuthenticationService.GetAccount(Constants.Username, Constants.Password);
            VerifyAccount(account);
        }

        [Test]
        public void CanGetAccountFromService()
        {
            var service = new AuthenticationService(Constants.ApiToken);
            var account = service.GetAccount();
            VerifyAccount(account);
        }

        [Test]
        public void CanGetAccountFromClient_ApiToken()
        {
            var client = new PivotalTrackerClient(Constants.ApiToken);
            var account = client.Account.GetAccount();
            VerifyAccount(account);
        }

        [Test]
        public void CanGetAccountFromClient_UserNamePassword()
        {
            var client = new PivotalTrackerClient(Constants.Username, Constants.Password);
            var account = client.Account.GetAccount();
            VerifyAccount(account);
        }

        private static void VerifyAccount(Account account)
        {
            Assert.IsNotNull(account);
            Assert.AreNotEqual(0, account.Id);
            Assert.IsNotNullOrEmpty(account.Email);
            Assert.IsNotNullOrEmpty(account.Initials);
            Assert.IsNotNullOrEmpty(account.Kind);
            Assert.IsNotNullOrEmpty(account.Name);
            Assert.IsNotNullOrEmpty(account.Username);
            Assert.IsNotNullOrEmpty(account.ApiToken);
            Assert.Greater(account.CreatedAt, DateTimeOffset.MinValue);
            Assert.Greater(account.UpdatedAt, DateTimeOffset.MinValue);
            Assert.IsNotNull(account.TimeZone);
            Assert.IsNotNull(account.TimeZone.Kind);
            Assert.IsNotNull(account.TimeZone.Offset);
            Assert.IsNotNull(account.TimeZone.OlsonName);
            Assert.AreNotEqual(Guid.Empty.ToString(), account.ApiToken);
            Assert.IsNotNull(account.Projects);
            Assert.AreEqual(2, account.Projects.Count);

            foreach (var project in account.Projects)
            {
                Assert.AreNotEqual(0, project.Id);
                Assert.AreNotEqual(0, project.ProjectId);
                Assert.IsNotNullOrEmpty(project.Kind);
                Assert.IsNotNullOrEmpty(project.ProjectName);
                Assert.IsNotNull(project.ProjectColor);
                Assert.IsNotNull(project.Role);
                Assert.Greater(project.LastViewedAt, DateTimeOffset.MinValue);
            }
        }
    }
    // ReSharper restore InconsistentNaming
}