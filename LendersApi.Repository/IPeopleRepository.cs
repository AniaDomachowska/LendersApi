using System.Linq;
using System.Threading.Tasks;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public interface IPeopleRepository
	{
		void Add(Person person);
		Task<Person> GetOne(int id);
		IQueryable<Person> GetAll();
	}
}