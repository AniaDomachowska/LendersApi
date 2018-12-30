using System.Collections.Generic;

namespace LendersApi.Repository.Model
{
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public IEnumerable<Loan> BorrowerLoans { get; set; }
		public IEnumerable<Loan> LenderLoans { get; set; }
	}
}