using System.Linq;
using System.Threading.Tasks;
using LendersApi.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace LendersApi.Repository
{
	public class PeopleRepository : IPeopleRepository
	{
		private readonly EfDbContext context;

		public PeopleRepository(EfDbContext context)
		{
			this.context = context;
		}

		public void Add(Person person)
		{
			context.People.Add(person);
		}

		public async Task<Person> GetOne(int id)
		{
			return await context
				.People
				.FirstOrDefaultAsync(element => element.Id == id);
		}

		public IQueryable<Person> GetAll()
		{
			return context.People.AsQueryable();
		}
	}
}