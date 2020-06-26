using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Initializr.WebApi.Models.Metadata;

namespace Steeltoe.Initializr.WebApi.Services
{
    public class ConfigServerMetadataRepository : IMetadataRepository
    {
        private readonly Configuration _configuration;

        /// <summary>
        /// Create a new ConfigServerMetadataRepository.
        /// </summary>
        /// <param name="configuration">configuration metadata</param>
        /// <param name="settings">Config Server settings</param>
        /// <param name="logger">application logger</param>
        public ConfigServerMetadataRepository(
            IOptions<Configuration> configuration,
            IOptions<ConfigServerClientSettingsOptions> settings,
            ILogger<IMetadataRepository> logger)
        {
            _configuration = configuration.Value;
            logger.LogInformation($"Using Config Server listening on {settings.Value.Uri}");
        }

        /// <summary>
        /// Return the configuration metadata.
        /// </summary>
        /// <returns>configuration metadata</returns>
        public Task<Configuration> GetConfiguration()
        {
            var result = new TaskCompletionSource<Configuration>();
            result.SetResult(_configuration);
            return result.Task;
        }
    }
}
