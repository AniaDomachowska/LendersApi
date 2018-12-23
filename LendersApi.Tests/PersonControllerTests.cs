using LendersApi.Controllers;
using NUnit.Framework;

namespace LendersApi.Tests
{
	[TestFixture]
	public class PersonControllerTests
	{
		[Test]
		public void GetAll_ReturnsAllPeople()
		{
			var peopleController = new PeopleController();
		}
	}
}