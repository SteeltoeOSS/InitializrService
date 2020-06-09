using Microsoft.AspNetCore.Mvc;
using Steeltoe.Initializr.WebApi.Server.Data;

namespace Steeltoe.Initializr.WebApi.Server
{
	/// <summary>
	/// Project generation configuration metadata endpoint.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class ConfigurationController : ControllerBase
	{
		private readonly IConfigurationRepository _configurationRepository;

		/// <summary>
		/// Create a new ConfigurationController.
		/// </summary>
		/// <param name="configurationRepository">configuration repository</param>
		public ConfigurationController(IConfigurationRepository configurationRepository)
		{
			_configurationRepository = configurationRepository;
		}

		/// <summary>
		/// Implements <code>GET</code>..
		/// </summary>
		/// <returns>Project generation configuration metadata</returns>
		[HttpGet]
		public ActionResult Get()
		{
			return Ok(_configurationRepository.GetConfiguration());
		}
	}
}
