using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using LendersApi.Controllers;
using LendersApi.Dto;
using LendersApi.Repository;
using LendersApi.Repository.Model;
using LendersApi.Tests.Helpers;
using Microsoft.AspNet.OData;
using NLog;
using NSubstitute;
using NUnit.Framework;

namespace LendersApi.Tests
{
	[TestFixture]
	public class PersonControllerTests
	{
		[OneTimeSetUp]
		public void Startup()
		{
			Mapper.Reset();
			AutoMapperConfig.Initialize();
		}

		[Test]
		public void GetAll_ReturnsAllPeople()
		{
			// Arrange
			var peopleController = PrepareSut(out _);

			// Act
			var people = peopleController.Get();

			// Assert
			people.Should().Contain(element => element.FirstName == "John");
		}

		[Test]
		public async Task AddPerson_AddsPerson()
		{
			// Arrange
			var peopleController = PrepareSut(out var unitOfWork);

			var personCreateDto = new PersonCreateDto()
			{
				FirstName = "John",
				LastName = "Doe"
			};

			var parameters = new ODataActionParameters {["model"] = personCreateDto};

			// Act

			await peopleController.AddPerson(parameters);

			// Assert

			unitOfWork.PeopleRepository
				.Received()
				.Add(Arg.Is<Person>(element => element.FirstName == "John"));
		}

		private static PeopleController PrepareSut(out IUnitOfWork unitOfWork)
		{
			var repository = Substitute.For<IPeopleRepository>();
			repository.GetAll().Returns(
				new List<Person>
				{
					new Person {FirstName = "John"}
				}.AsQueryable());

			unitOfWork = Substitute.For<IUnitOfWork>();
			unitOfWork.PeopleRepository.Returns(repository);

			var logger = Substitute.For<Microsoft.Extensions.Logging.ILogger<PeopleController>>();

			var peopleController = new PeopleController(unitOfWork, logger);
			return peopleController;
		}

		[Test]
		public async Task GetAll_ReturnsAllLoans()
		{
			// Arrange
			var personController = PrepareSut(out var unitOfWork);

			unitOfWork.LoanRepository.GetAllForPerson(Arg.Is(1)).Returns(new List<Loan>()
					{
						new Loan {Amount = (decimal) 12.2, Id = 1, LenderId = 2}
					}.AsQueryable());

			// Act
			var loans = await personController.GetLoans(1);

			// Assert
			loans.Should().Contain(element => element.Amount == (decimal)12.2);
		}
	}
}