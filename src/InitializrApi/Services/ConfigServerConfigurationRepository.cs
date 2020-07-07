// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Models;

namespace Steeltoe.InitializrApi.Services
{
    public class ConfigServerConfigurationRepository : IConfigurationRepository
    {
        private readonly Configuration _configuration;

        /// <summary>
        /// An IConfigurationRepository implementation using a Spring Cloud Config Server.
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="settings">Config Server settings</param>
        /// <param name="logger">application logger</param>
        public ConfigServerConfigurationRepository(
            IOptions<Configuration> configuration,
            IOptions<ConfigServerClientSettingsOptions> settings,
            ILogger<IConfigurationRepository> logger)
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
            logger.LogInformation($"Using Config Server listening at {settings.Value.Uri ?? "default URI"}");
        }

        /// <summary>
        /// Return the configuration
        /// </summary>
        /// <returns>configuration</returns>
        public Task<Configuration> GetConfiguration()
        {
            var result = new TaskCompletionSource<Configuration>();
            result.SetResult(_configuration);
            return result.Task;
        }
    }
}
