using System.Linq;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public class LoanRepository : ILoanRepository
	{
		private readonly EfDbContext context;

		public LoanRepository(EfDbContext context)
		{
			this.context = context;
		}

		public void Add(Loan loan)
		{
			context.Loans.Add(loan);
		}

		public Loan GetOne(int id)
		{
			return context
				.Loans
				.FirstOrDefault(element => element.Id == id);
		}

		public IQueryable<Loan> GetAllForPerson(int borrowerId)
		{
			return context
				.Loans
				.Where(element => element.Lender == borrowerId);
		}

		public IQueryable<Loan> GetAll()
		{
			return context.Loans.AsQueryable();
		}
	}
}