using LendersApi.Controllers;
using LendersApi.Repository;
using NUnit.Framework;

namespace LendersApi.Tests
{
	[TestFixture]
	public class PersonControllerTests
	{
		[Test]
		public void GetAll_ReturnsAllPeople()
		{
			var repository = NSubstitute.Substitute.For<IPeopleRepository>();
			var peopleController = new PeopleController(repository);
		}
	}
}