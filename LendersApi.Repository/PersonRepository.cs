using System.Linq;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public class PersonRepository : IPersonRepository
	{
		private readonly EfDbContext context;

		public PersonRepository(EfDbContext context)
		{
			this.context = context;
		}

		public void Add(Person person)
		{
			context.People.Add(person);
		}

		public Person GetOne(int id)
		{
			return context
				.People
				.FirstOrDefault(element => element.Id == id);
		}

		public IQueryable<Person> GetAll()
		{
			return context.People.AsQueryable();
		}
	}
}