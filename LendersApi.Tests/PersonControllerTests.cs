using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LendersApi.Controllers;
using LendersApi.Dto;
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
			var peopleController = PrepareSut();

			// Act
			var people = peopleController.GetPeople();

			// Assert
			people.Should().Contain(element => element.FirstName == "John");
		}

		[Test]
		public void AddPerson_AddsPerson()
		{
			// Arrange
			var peopleController = PrepareSut();

			// Act
			var personCreateDto = new PersonCreateDto()
			{
				FirstName = "John",
				LastName = "Doe"
			};
			
			var result = peopleController.AddPerson(personCreateDto);

			// Assert
			
		}

		private static PeopleController PrepareSut()
		{
			var repository = Substitute.For<IPeopleRepository>();
			repository.GetAll().Returns(new List<Person> {new Person {FirstName = "John"}}.AsQueryable());

			IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
			unitOfWork.PeopleRepository.Returns(repository);

			var peopleController = new PeopleController(unitOfWork);
			return peopleController;
		}
	}
}