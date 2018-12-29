using System.Threading.Tasks;

namespace LendersApi.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly EfDbContext context;

		public UnitOfWork(EfDbContext context, IPeopleRepository peopleRepository, ILoanRepository loanRepository)
		{
			this.context = context;
			PeopleRepository = peopleRepository;
			LoanRepository = loanRepository;
		}

		public IPeopleRepository PeopleRepository { get; }

		public ILoanRepository LoanRepository { get; }

		public async Task Commit()
		{
			await context.SaveChangesAsync();
		}
	}
}