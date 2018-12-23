namespace LendersApi.Repository.Model
{
	public class Ledger
	{
		public int Lender { get; set; }
		public int Borrower { get; set; }
		public decimal Amount { get; set; }
	}
}