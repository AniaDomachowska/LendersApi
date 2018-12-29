using System.Linq;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public interface ILoanRepository
	{
		void Add(Loan loan);
		Loan GetOne(int id);
		IQueryable<Loan> GetAllForPerson(int borrowerId);
	}
}