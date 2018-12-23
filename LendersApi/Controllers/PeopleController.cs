using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LendersApi.Dto;
using LendersApi.Repository;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace LendersApi.Controllers
{
	public class PeopleController : ODataController
	{
		private readonly IPeopleRepository peopleRepository;

		public PeopleController(IPeopleRepository peopleRepository)
		{
			this.peopleRepository = peopleRepository;
		}

		[EnableQuery]
		[HttpGet]
		public IEnumerable<PersonDto> GetPeople()
		{
			return peopleRepository.GetAll().AsEnumerable().Select(Mapper.Map<PersonDto>);
		}
	}
}