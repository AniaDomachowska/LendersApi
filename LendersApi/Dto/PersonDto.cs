using System.Collections.Generic;

namespace LendersApi.Dto
{
	public class PersonDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public IEnumerable<LoanDto> Loans { get; set; }
	}
}