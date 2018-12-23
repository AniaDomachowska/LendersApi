using System.Linq;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public interface IPeopleRepository
	{
		void Add(Person person);
		Person GetOne(int id);
		IQueryable<Person> GetAll();
	}
}