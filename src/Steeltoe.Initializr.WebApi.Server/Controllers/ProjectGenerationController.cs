using Microsoft.AspNetCore.Mvc;

namespace Steeltoe.Initializr.WebApi.Server
{
	/// <summary>
	/// Project generation endpoint.
	/// </summary>
	[ApiController]
	[Route("api/starter.zip")]
	public class ProjectGenerationController : ControllerBase
	{
		/// <summary>
		/// Implements <code>GET</code>..
		/// </summary>
		/// <returns>Generated project</returns>
		[HttpGet]
		public ActionResult Get()
		{
			return Ok("");
		}
	}
}
