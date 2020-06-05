using Microsoft.AspNetCore.Mvc;

namespace Steeltoe.Initializr.WebApi.Server
{
	/// <summary>
	/// Project generation configuration metadata endpoint.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class ConfigurationController : ControllerBase
	{
		/// <summary>
		/// Implements <code>GET</code>..
		/// </summary>
		/// <returns>Project generation configuration metadata</returns>
		[HttpGet]
		public ActionResult Get()
		{
			return Ok("");
		}
	}
}
