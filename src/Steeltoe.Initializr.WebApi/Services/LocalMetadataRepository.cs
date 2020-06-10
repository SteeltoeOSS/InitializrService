using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Steeltoe.Initializr.WebApi.Models.Metadata;

namespace Steeltoe.Initializr.WebApi.Services
{
	/// <summary>
	/// A configuration repository that loads configuration from a local JSON file.
	/// </summary>
	public class LocalMetadataRepository : IMetadataRepository
	{
		private readonly ILogger _logger;

		public Configuration Configuration { get; private set; }

		/// <summary>
		/// Create a new LocalConfigurationRepository.
		/// Gets the project configuration defined by a local JSON file specified in appsettings.json.
		/// </summary>
		public LocalMetadataRepository(ILoggerFactory loggerFactory, IConfiguration configuration)
		{
			_logger = loggerFactory.CreateLogger<LocalMetadataRepository>();
			var options = new InitializrOptions();
			configuration.GetSection(InitializrOptions.Initializr).Bind(options);
			_logger.LogInformation($"loading metadata configuration from file: {options.MetadataFile}");
			Configuration =
				JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(options.MetadataFile));
		}

		/// <returns>project generation configuration</returns>
		public Task<Configuration> GetConfiguration()
		{
			var result = new TaskCompletionSource<Configuration>();
			result.SetResult(Configuration);
			return result.Task;
		}
	}
}
