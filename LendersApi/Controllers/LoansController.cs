using System;
using System.Threading.Tasks;
using AutoMapper;
using LendersApi.Dto;
using LendersApi.Repository;
using LendersApi.Repository.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace LendersApi.Controllers
{
	public class LoansController : ODataController
	{
		private readonly IUnitOfWork unitOfWork;

		public LoansController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpPost]
		public async Task<ActionResult> AddLoan(ODataActionParameters parameters)
		{
			LoanCreateDto loanCreateDto = (LoanCreateDto) parameters["model"];
			var loan = Mapper.Map<Loan>(loanCreateDto);

			unitOfWork.LoanRepository.Add(loan);

			await unitOfWork.Commit();
			return Ok(loanCreateDto);
		}

		[HttpPost]
		public async Task<ActionResult> PayLoan(
			[FromODataUri]int key,
			ODataActionParameters parameters)
		{
			var amountParam = parameters["Amount"];

			if (amountParam == null)
			{
				return BadRequest("Amount not provided.");
			}

			var amount = (decimal)amountParam;

			if (amount <= 0)
			{
				return BadRequest("Amount is less than 0");
			}

			var loan = await unitOfWork.LoanRepository.GetOne(key);

			if (loan == null)
			{
				return BadRequest("Loan does not exist.");
			}

			loan.PaidAmount += amount;

			if (loan.Amount <= loan.PaidAmount)
			{
				loan.PaidDateTime = DateTime.Now;
			}

			await unitOfWork.Commit();

			return Ok(loan);
		}
	}
}