namespace LendersApi.Dto
{
	public class LoanCreateDto
	{
		public int Lender { get; set; }
		public int Borrower { get; set; }
		public decimal Amount { get; set; }
	}
}