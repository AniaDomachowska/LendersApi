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
		public ActionResult<PersonDto> Get([FromODataUri] int key)
		{
			var person = unitOfWork
				.PeopleRepository
				.GetOne(key);

			return Mapper.Map<PersonDto>(person);
		}

		[EnableQuery]
		public IQueryable<LoanDto> GetLoans([FromODataUri]int key)
		{
			return unitOfWork
				.LoanRepository
				.GetAllForPerson(key)
				.AsEnumerable()
				.Select(Mapper.Map<LoanDto>)
				.AsQueryable();
		}

		[HttpPost]
		public async Task<ActionResult> AddPerson(PersonCreateDto personCreateDto)
		{
			unitOfWork
				.PeopleRepository
				.Add(Mapper.Map<Person>(personCreateDto));

			await unitOfWork.Commit();

			return Ok(personCreateDto);
		}
	}
}