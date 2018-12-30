using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendersApi.Repository.Model;
using Microsoft.EntityFrameworkCore;

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

		public async Task<Loan> GetOne(int id)
		{
			return await context
				.Loans
				.FirstOrDefaultAsync(element => element.Id == id);
		}

		public async Task<IEnumerable<Loan>> GetAllForPerson(int borrowerId)
		{
			return await context
				.Loans
				.Where(element => element.LenderId == borrowerId)
				.ToListAsync();
		}
	}
}