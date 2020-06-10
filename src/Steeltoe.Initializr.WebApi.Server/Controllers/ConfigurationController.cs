using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Initializr.WebApi.Server.Services;

namespace Steeltoe.Initializr.WebApi.Server.Controllers
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
		public async Task<IActionResult> Get()
		{
			var config = await _configurationRepository.GetConfiguration();
			return Ok(config);
		}
	}
}
