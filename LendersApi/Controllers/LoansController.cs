using LendersApi.Repository;
using Microsoft.AspNet.OData;

namespace LendersApi.Controllers
{
	public class LoansController : ODataController
	{
		private readonly IUnitOfWork unitOfWork;

		public LoansController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
	}
}