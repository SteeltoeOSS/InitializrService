using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Steeltoe.Initializr.WebApi.Server.Models.Metadata;

namespace Steeltoe.Initializr.WebApi.Server.Data
{
	/// <summary>
	/// A configuration repository that loads configuration from a local JSON file.
	/// </summary>
	public class LocalConfigurationRepository : IConfigurationRepository
	{
		private readonly ILogger _logger;

		/// <summary>
		/// Create a new LocalConfigurationRepository.
		/// </summary>
		public LocalConfigurationRepository(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory?.CreateLogger<LocalConfigurationRepository>();
		}

		/// <summary>
		/// Gets the project configuration defined by the local JSON file.
		/// </summary>
		/// <returns>project generation configuration</returns>
		public Task<Configuration> GetConfiguration()
		{
			_logger?.LogInformation("getting configuration");
			var config = new Configuration();
			var result = new TaskCompletionSource<Configuration>();
			result.SetResult(config);
			return result.Task;
		}
	}
}
