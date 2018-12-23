using System.Threading.Tasks;

namespace LendersApi.Repository
{
	public interface IUnitOfWork
	{
		IPeopleRepository PeopleRepository { get; }
		Task Commit();
	}
}