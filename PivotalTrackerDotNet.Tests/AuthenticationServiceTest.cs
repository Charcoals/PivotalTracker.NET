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
            var account = client.Authentication.GetAccount();
            VerifyAccount(account);
        }

        [Test]
        public void CanGetAccountFromClient_UserNamePassword()
        {
            var client = new PivotalTrackerClient(Constants.Username, Constants.Password);
            var account = client.Authentication.GetAccount();
            VerifyAccount(account);
        }

        private static void VerifyAccount(Me me)
        {
            Assert.IsNotNull(me);
            Assert.AreNotEqual(0, me.Id);
            Assert.IsNotNullOrEmpty(me.Email);
            Assert.IsNotNullOrEmpty(me.Initials);
            Assert.IsNotNullOrEmpty(me.Kind);
            Assert.IsNotNullOrEmpty(me.Name);
            Assert.IsNotNullOrEmpty(me.Username);
            Assert.IsNotNullOrEmpty(me.ApiToken);
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