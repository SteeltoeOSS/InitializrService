using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Steeltoe.Initializr.WebApi.Server
{
	/// <summary>
	/// Project generation endpoint.
	/// </summary>
	[ApiController]
	[Route("api/starter.zip")]
	public class ProjectGenerationController : ControllerBase
	{
		private readonly ILogger<ProjectGenerationController> _logger;

		/// <summary>
		/// Create a new ConfigurationController.
		/// </summary>
		/// <param name="loggerFactory">logger factory</param>
		/// <param name="configurationRepository">configuration repository</param>
		public ProjectGenerationController(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<ProjectGenerationController>();
		}

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
