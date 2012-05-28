using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests {
    class ProjectServiceTest {
        private ProjectService service = null;

        [TestFixtureSetUp]
        public void TestFixtureSetUp() {
            service = new ProjectService(AuthenticationService.Authenticate(TestCredentials.Username, TestCredentials.Password));
        }

        [Test]
        public void GetProjects() {
            var result = service.GetProjects();
            Assert.AreEqual(2, result.Count);
        }
    }
}
