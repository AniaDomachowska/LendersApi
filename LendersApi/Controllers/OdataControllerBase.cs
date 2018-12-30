using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LendersApi.Controllers
{
	public class OdataControllerBase : ODataController
	{
		public OdataControllerBase(ILogger logger)
		{
			Logger = logger;
		}

		protected ILogger Logger { get; }

		protected ActionResult ReturnBadRequestWithLog(string message)
		{
			Logger.LogError(message);
			return BadRequest(message);
		}
	}
}