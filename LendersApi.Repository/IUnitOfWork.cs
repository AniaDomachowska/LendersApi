using System.Threading.Tasks;

namespace LendersApi.Repository
{
	public interface IUnitOfWork
	{
		IPeopleRepository PeopleRepository { get; }
		ILoanRepository LoanRepository { get; }
		Task Commit();
	}
}