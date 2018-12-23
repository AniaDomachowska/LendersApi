using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LendersApi.Controllers;
using LendersApi.Repository;
using LendersApi.Repository.Model;
using LendersApi.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace LendersApi.Tests
{
	[TestFixture]
	public class PersonControllerTests
	{
		[SetUp]
		public void Startup()
		{
			AutoMapperConfig.Initialize();
		}

		[Test]
		public void GetAll_ReturnsAllPeople()
		{
			// Arrange
			var repository = Substitute.For<IPeopleRepository>();
			repository.GetAll().Returns(new List<Person> {new Person {FirstName = "John"}}.AsQueryable());

			var peopleController = new PeopleController(repository);

			// Act
			var people = peopleController.GetPeople();

			// Assert
			people.Should().Contain(element => element.FirstName == "John");
		}
	}
}