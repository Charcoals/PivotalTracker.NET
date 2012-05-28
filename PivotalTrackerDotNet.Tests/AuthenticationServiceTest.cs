using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PivotalTrackerDotNet.Domain;

namespace PivotalTrackerDotNet.Tests
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        [Test]
        public void CanAuthenticateWithValidCredentials()
        {
            var token = AuthenticationService.Authenticate(TestCredentials.Username, TestCredentials.Password);
            Assert.IsNotNull(token);
            Assert.IsNotNull(token.Guid);
            Assert.AreNotEqual(Guid.Empty, token.Guid);
        }
    }
}
