using System.Collections.Generic;
using System.Threading.Tasks;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public interface ILoanRepository
	{
		void Add(Loan loan);
		Task<Loan> GetOne(int id);
		Task<IEnumerable<Loan>> GetAllForPerson(int borrowerId);
	}
}