// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
            var about = new Program.About();
            _configuration.About = new Configuration.Product
            {
                Name = about.Name,
                Vendor = about.Vendor,
                Url = about.ProductUrl,
                Version = about.Version,
                Commit = about.Commit,
            };
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
