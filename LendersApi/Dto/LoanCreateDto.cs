namespace LendersApi.Dto
{
	public class LoanCreateDto
	{
		public int LenderId { get; set; }
		public int BorrowerId { get; set; }
		public decimal Amount { get; set; }
	}
}