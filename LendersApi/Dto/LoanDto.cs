using System;

namespace LendersApi.Dto
{
	public class LoanDto
	{
		public int Id { get; set; }
		public int Lender { get; set; }
		public int Borrower { get; set; }
		public decimal Amount { get; set; }
		public decimal PaidAmount { get; set; }
		public DateTime PaidDateTime { get; set; }
	}
}