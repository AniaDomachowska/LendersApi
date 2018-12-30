using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LendersApi.Dto;
using LendersApi.Repository;
using LendersApi.Repository.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace LendersApi.Controllers
{
	public class PeopleController : ODataController
	{
		private readonly IUnitOfWork unitOfWork;

		public PeopleController(IUnitOfWork unitOfWork)
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

			if (person == null)
			{
				return BadRequest("Person does not exist.");
			}

			return Mapper.Map<PersonDto>(person);
		}

		[EnableQuery]
		public async Task<IQueryable<LoanDto>> GetLoans([FromODataUri]int key)
		{
			var loans = await unitOfWork
				.LoanRepository
				.GetAllForPerson(key);

			return loans
				.Select(Mapper.Map<LoanDto>)
				.AsQueryable();
		}

		[HttpPost]
		public async Task<ActionResult> AddPerson(PersonCreateDto personCreateDto)
		{
			if (personCreateDto == null)
			{
				return BadRequest("Request data model was not provided.");
			}

			unitOfWork
				.PeopleRepository
				.Add(Mapper.Map<Person>(personCreateDto));

			await unitOfWork.Commit();

			return Ok(personCreateDto);
		}
	}
}