using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Steeltoe.Initializr.WebApi.Services;

namespace Steeltoe.Initializr.WebApi.MetadataRepository
{
	/// <summary>
	/// A configuration repository that loads configuration from a local JSON file.
	/// </summary>
	public class LocalMetadataRepository : IMetadataRepository
	{
		private readonly ILogger<LocalMetadataRepository> _logger;

		private readonly MetadataRepositoryOptions _options;

		private readonly Configuration _configuration;

		/// <summary>
		/// Create a new LocalConfigurationRepository.
		/// Gets the project configuration defined by a local JSON file specified in appsettings.json.
		/// </summary>
		public LocalMetadataRepository(ILoggerFactory loggerFactory, IOptions<MetadataRepositoryOptions> options)
		{
			_logger = loggerFactory.CreateLogger<LocalMetadataRepository>();
			_options = options.Value;
			var file = new Regex("^file://").Replace(_options.Uri, string.Empty);
			_logger.LogInformation($"loading metadata configuration from file: {file}");
			_configuration =
				JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(file));
		}

		/// <returns>project generation configuration</returns>
		public Task<Configuration> GetConfiguration()
		{
			var result = new TaskCompletionSource<Configuration>();
			result.SetResult(_configuration);
			return result.Task;
		}
	}
}
