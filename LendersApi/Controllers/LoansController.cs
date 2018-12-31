using System;
using System.Threading.Tasks;
using AutoMapper;
using LendersApi.Dto;
using LendersApi.Repository;
using LendersApi.Repository.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LendersApi.Controllers
{
	public class LoansController : OdataControllerBase
	{
		private readonly IUnitOfWork unitOfWork;

		public LoansController(IUnitOfWork unitOfWork, ILogger<LoansController> logger) : base(logger)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpPost]
		public async Task<ActionResult> AddLoan(ODataActionParameters parameters)
		{
			Logger.LogInformation("Adding loan started.");
			var loanCreateDto = parameters["model"] as LoanCreateDto;

			if (loanCreateDto == null)
			{
				return ReturnBadRequestWithLog("Request data model not provided.");
			}

			if (loanCreateDto.BorrowerId == loanCreateDto.LenderId)
			{
				return ReturnBadRequestWithLog("Person cannot lend money to oneself.");
			}

			var borrower = await unitOfWork.PeopleRepository.GetOne(loanCreateDto.BorrowerId);
			if (borrower == null)
			{
				return ReturnBadRequestWithLog("Borrower doesn't exist.");
			}

			var lender = await unitOfWork.PeopleRepository.GetOne(loanCreateDto.LenderId);
			if (lender == null)
			{
				return ReturnBadRequestWithLog("Lender doesn't exist.");
			}

			if (loanCreateDto.Amount <= 0)
			{
				return ReturnBadRequestWithLog("Amount is invalid.");
			}

			var loan = Mapper.Map<Loan>(loanCreateDto);

			unitOfWork.LoanRepository.Add(loan);

			await unitOfWork.Commit();

			var loanDto = Mapper.Map<LoanDto>(loan);

			Logger.LogInformation("Adding loan successful.");
			return Ok(loanDto);
		}

		[HttpPost]
		public async Task<ActionResult> PayLoan(
			[FromODataUri]int key,
			ODataActionParameters parameters)
		{
			Logger.LogInformation("Paying loan started.");

			var amountParam = parameters["Amount"];

			if (amountParam == null)
			{
				return ReturnBadRequestWithLog("Amount not provided.");
			}

			var amount = (decimal)amountParam;

			if (amount <= 0)
			{
				return ReturnBadRequestWithLog("Amount is less than 0");
			}

			var loan = await unitOfWork.LoanRepository.GetOne(key);

			if (loan == null)
			{
				return ReturnBadRequestWithLog("Loan does not exist.");
			}

			if (loan.PaidDateTime != null)
			{
				return ReturnBadRequestWithLog("Loan already paid!");
			}

			loan.PaidAmount += amount;

			if (loan.Amount <= loan.PaidAmount)
			{
				loan.PaidDateTime = DateTime.Now;
			}

			await unitOfWork.Commit();

			var loanDto = Mapper.Map<LoanDto>(loan);

			Logger.LogInformation("Paying loan successful.");
			return Ok(loanDto);
		}
	}
}