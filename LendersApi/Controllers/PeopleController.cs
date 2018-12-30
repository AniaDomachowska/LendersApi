using System.Collections.Generic;
using System.Linq;
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
	public class PeopleController : OdataControllerBase
	{
		private readonly IUnitOfWork unitOfWork;

		public PeopleController(IUnitOfWork unitOfWork, ILogger<PeopleController> logger) : base(logger)
		{
			this.unitOfWork = unitOfWork;
		}

		[EnableQuery]
		[System.Web.Http.HttpGet]
		public IEnumerable<PersonDto> Get()
		{
			return unitOfWork
				.PeopleRepository
				.GetAll()
				.AsEnumerable()
				.Select(Mapper.Map<PersonDto>);
		}

		[EnableQuery]
		[System.Web.Http.HttpGet]
		public async Task<ActionResult<PersonDto>> Get([FromODataUri] int key)
		{
			var person = await unitOfWork
				.PeopleRepository
				.GetOne(key);

			if (person == null) return ReturnBadRequestWithLog("Person does not exist.");

			return Mapper.Map<PersonDto>(person);
		}

		[EnableQuery]
		public async Task<IQueryable<LoanDto>> GetLoans([FromODataUri] int key)
		{
			var loans = await unitOfWork
				.LoanRepository
				.GetAllForPerson(key);

			return loans
				.Select(Mapper.Map<LoanDto>)
				.AsQueryable();
		}

		[HttpPost]
		public async Task<ActionResult> AddPerson(ODataActionParameters parameters)
		{
			Logger.LogInformation("Adding person started.");

			var personCreateDto = parameters["model"] as PersonCreateDto;

			if (personCreateDto == null) return ReturnBadRequestWithLog("Request data model was not provided.");

			var personToAdd = Mapper.Map<Person>(personCreateDto);

			unitOfWork
				.PeopleRepository
				.Add(personToAdd);

			await unitOfWork.Commit();

			var personDto = Mapper.Map<PersonDto>(personToAdd);

			Logger.LogInformation("Adding person succeeded.");
			return Ok(personDto);
		}
	}
}