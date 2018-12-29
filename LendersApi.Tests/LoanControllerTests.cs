using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LendersApi.Controllers;
using LendersApi.Dto;
using LendersApi.Repository;
using LendersApi.Repository.Model;
using LendersApi.Tests.Helpers;
using Microsoft.AspNet.OData;
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


		[Test]
		public async Task AddLoan_AddsLoan()
		{
			// Arrange
			var loansController = PrepareSut(out var unitOfWork);

			// Act
			var loanCreateDto = new LoanCreateDto()
			{
				Amount = 100,
				Borrower = 1,
				Lender = 1
			};

			var oDataActionParameters = new ODataActionParameters {["model"] = loanCreateDto};
			await loansController.AddLoan(oDataActionParameters);

			// Assert

			unitOfWork.LoanRepository
				.Received()
				.Add(Arg.Is<Loan>(element => element.Amount == loanCreateDto.Amount
				                             && element.Borrower == loanCreateDto.Borrower
				                             && element.Lender == loanCreateDto.Lender));

			await unitOfWork
				.Received()
				.Commit();
		}

		[Test]
		public async Task PayLoan_PaysPartOfTheLoanProperly()
		{
			// Arrange
			var loansController = PrepareSut(out var unitOfWork);

			var loan = new Loan()
			{
				Amount = 100,
				Borrower = 1,
				Lender = 2
			};

			unitOfWork.LoanRepository.GetOne(Arg.Any<int>()).Returns(loan);

			// Act
			var oDataActionParameters = new ODataActionParameters { ["Amount"] = (decimal) 12.2 };
			await loansController.PayLoan(1, oDataActionParameters);

			// Assert

			loan.PaidAmount.Should().Be((decimal)12.2);
		}

		[Test]
		public async Task PayLoan_PaysWholeLoanProperly()
		{
			// Arrange
			var loansController = PrepareSut(out var unitOfWork);

			var loan = new Loan()
			{
				Amount = 100,
				Borrower = 1,
				Lender = 2
			};

			unitOfWork.LoanRepository.GetOne(Arg.Any<int>()).Returns(loan);

			// Act
			var oDataActionParameters = new ODataActionParameters { ["Amount"] = (decimal)44 };
			await loansController.PayLoan(1, oDataActionParameters);
			await loansController.PayLoan(1, oDataActionParameters);
			await loansController.PayLoan(1, oDataActionParameters);

			// Assert

			loan.PaidAmount.Should().Be(132);
			loan.PaidDateTime.Should().NotBeNull();
		}

		private static LoansController PrepareSut(out IUnitOfWork unitOfWork)
		{
			var repository = Substitute.For<ILoanRepository>();
			repository.GetAllForPerson(Arg.Is(1)).Returns(new List<Loan> {new Loan
					{
						Amount = (decimal)12.33,
						Borrower = 1,
						Lender = 2
					}}.AsQueryable());

			unitOfWork = Substitute.For<IUnitOfWork>();
			unitOfWork.LoanRepository.Returns(repository);

			var loanController = new LoansController(unitOfWork);
			return loanController;
		}
	}
}