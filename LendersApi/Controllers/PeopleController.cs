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
		public IEnumerable<PersonDto> GetPeople()
		{
			return unitOfWork
				.PeopleRepository
				.GetAll()
				.AsEnumerable()
				.Select(Mapper.Map<PersonDto>);
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