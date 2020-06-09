using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Steeltoe.Initializr.WebApi.Server.Models.Metadata;

namespace Steeltoe.Initializr.WebApi.Server.Data
{
	/// <summary>
	/// A configuration repository that loads configuration from a local JSON file.
	/// </summary>
	public class LocalConfigurationRepository : IConfigurationRepository
	{
		private const string ConfigurationFile = "initializr-configuration.json";

		private static readonly string ConfigurationPath =
			Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
				ConfigurationFile);

		private static readonly object Padlock = new object();

		private static Configuration _configuration;

		private readonly ILogger _logger;


		/// <summary>
		/// Create a new LocalConfigurationRepository.
		/// </summary>
		public LocalConfigurationRepository(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<LocalConfigurationRepository>();
		}

		/// <summary>
		/// Gets the project configuration defined by the local JSON file.
		/// </summary>
		/// <returns>project generation configuration</returns>
		public Task<Configuration> GetConfiguration()
		{
			if (_configuration == null)
			{
				lock (Padlock)
				{
					if (_configuration == null)
					{
						_logger.LogInformation($"loading configuration: {ConfigurationPath}");
						_configuration =
							JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConfigurationPath));
					}
				}
			}

			var result = new TaskCompletionSource<Configuration>();
			result.SetResult(_configuration);
			return result.Task;
		}
	}
}
