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

		[System.Web.Http.HttpPost]
		public async Task<ActionResult> AddLoan(ODataActionParameters parameters)
		{
			LoanCreateDto loanCreateDto = (LoanCreateDto) parameters["model"];
			var loan = Mapper.Map<Loan>(loanCreateDto);

			unitOfWork.LoanRepository.Add(loan);

			await unitOfWork.Commit();
			return Ok(loanCreateDto);
		}
	}
}