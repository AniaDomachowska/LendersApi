using System;

namespace LendersApi.Repository.Model
{
	public class Loan
	{
		public int Id { get; set; }
		public int LenderId { get; set; }
		public Person Lender { get; set; }
		public int BorrowerId { get; set; }
		public Person Borrower { get; set; }
		public decimal Amount { get; set; }
		public decimal PaidAmount { get; set; }
		public DateTime? PaidDateTime { get; set; }
	}
}