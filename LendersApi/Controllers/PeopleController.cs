using System.Collections.Generic;
using LendersApi.Dto;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace LendersApi.Controllers
{
	public class PeopleController : ODataController
	{
		[EnableQuery]
		[HttpGet]
		public IEnumerable<PersonDto> GetPeople()
		{
			return new List<PersonDto> {new PersonDto {FirstName = "John"}};
		}
	}
}