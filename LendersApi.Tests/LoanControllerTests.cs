using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
	public class LoanControllerTests
	{
		[SetUp]
		public void Startup()
		{
			AutoMapperConfig.Initialize();
		}

//		[Test]
//		public void GetAll_ReturnsAllLoans()
//		{
//			// Arrange
//			var loanController = PrepareSut(out var unitOfWork);

//			unitOfWork.LoanRepository.GetAllForPerson(Arg.Is(1)).Returns(new List<Loan>()
//			{
//				new Loan() {Amount = (decimal) 12.2, Id = 1, Lender = 2}
//			}.AsQueryable());

//			// Act
//			var loans = loanController.GetLoans(1);

//			// Assert
//			loans.Should().Contain(element => element.Amount == (decimal) 12.2);
//		}

//		[Test]
//		public async Task AddPerson_AddsPerson()
//		{
//			// Arrange
//			var peopleController = PrepareSut(out var unitOfWork);

//			// Act
//			var personCreateDto = new PersonCreateDto()
//			{
//				FirstName = "John",
//				LastName = "Doe"
//			};
			
////			await peopleController.a(personCreateDto);

//			// Assert

//			unitOfWork.PeopleRepository
//				.Received()
//				.Add(Arg.Is<Person>(element => element.FirstName == "John"));
//		}

//		private static LoansController PrepareSut(out IUnitOfWork unitOfWork)
//		{
//			var repository = Substitute.For<ILoanRepository>();
//			repository.GetAllForPerson(Arg.Is(1)).Returns(new List<Loan> {new Loan
//			{
//				Amount = (decimal)12.33,
//				Borrower = 1,
//				Lender = 2
//			}}.AsQueryable());

//			unitOfWork = Substitute.For<IUnitOfWork>();
//			unitOfWork.LoanRepository.Returns(repository);

//			var loanController = new LoansController(unitOfWork);
//			return loanController;
//		}
	}
}