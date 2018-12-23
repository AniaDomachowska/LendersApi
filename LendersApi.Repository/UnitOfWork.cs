using System.Threading.Tasks;

namespace LendersApi.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly EfDbContext context;

		public UnitOfWork(EfDbContext context, IPeopleRepository peopleRepository)
		{
			this.context = context;
			PeopleRepository = peopleRepository;
		}

		public IPeopleRepository PeopleRepository { get; }

		public async Task Commit()
		{
			await context.SaveChangesAsync();
		}
	}
}