using System.ComponentModel.DataAnnotations;

namespace LendersApi.Dto
{
	public class PersonCreateDto
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }
	}
}