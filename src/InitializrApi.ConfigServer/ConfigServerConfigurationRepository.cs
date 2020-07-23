// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.ConfigServer
{
    /// <summary>
    /// An <see cref="IConfigurationRepository"/> using a <a href="https://cloud.spring.io/spring-cloud-config/reference/html/#_spring_cloud_config_server">Spring Cloud Config Server</a> backend.
    /// </summary>
    public class ConfigServerConfigurationRepository : IConfigurationRepository
    {
        private readonly Configuration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationRepository"/> class.
        /// </summary>
        /// <param name="configuration">Injected configuration from Config Server.</param>
        /// <param name="settings">Injected Config Server settings.</param>
        /// <param name="about">Injected about.</param>
        /// <param name="logger">Injected logger.</param>
        public ConfigServerConfigurationRepository(
            IOptions<Configuration> configuration,
            IOptions<ConfigServerClientSettingsOptions> settings,
            IAbout about,
            ILogger<IConfigurationRepository> logger)
        {
            _configuration = configuration.Value;
            _configuration.About = about.GetAbout();
            logger.LogInformation($"Using Config Server listening at {settings.Value.Uri ?? "default URI"}");
        }

        /// <inheritdoc/>
        public Task<Configuration> GetConfiguration()
        {
            var result = new TaskCompletionSource<Configuration>();
            result.SetResult(_configuration);
            return result.Task;
        }
    }
}
