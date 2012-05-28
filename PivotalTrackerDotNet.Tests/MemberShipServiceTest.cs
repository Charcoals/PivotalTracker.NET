using NUnit.Framework;

namespace PivotalTrackerDotNet.Tests {
	[TestFixture]
	public class MembershipServiceTest {
		private MembershipService membershipService = null;
		const int projectId = 456301;

		[TestFixtureSetUp]
		public void TestFixtureSetUp() {
			membershipService = new MembershipService(AuthenticationService.Authenticate(TestCredentials.Username, TestCredentials.Password));
		}

		[Test]
		public void CanRetrieveAllPersonsAllowedInAProject() {
			var persons = membershipService.GetMembers(projectId);
			Assert.NotNull(persons);
			Assert.AreEqual(1, persons.Count);
		}
	}
}
