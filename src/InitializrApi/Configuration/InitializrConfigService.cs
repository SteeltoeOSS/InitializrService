// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.Configuration
{
    /// <summary>
    /// An <see cref="IInitializrConfigService"/> using a <a href="https://cloud.spring.io/spring-cloud-config/reference/html/#_spring_cloud_config_server">Spring Cloud Config Server</a> backend.
    /// </summary>
    public class InitializrConfigService : InitializrApiServiceBase, IInitializrConfigService
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly InitializrConfig _config;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializrConfigService"/> class.
        /// </summary>
        /// <param name="configuration">Injected configuration from Config Server.</param>
        /// <param name="settings">Injected settings from Config Server.</param>
        /// <param name="logger">Injected logger.</param>
        public InitializrConfigService(
            IOptions<InitializrConfig> configuration,
            IOptions<ConfigServerClientSettingsOptions> settings,
            ILogger<InitializrConfigService> logger)
            : base(logger)
        {
            _config = configuration.Value;
            Logger.LogInformation($"Config Server: uri={settings.Value.Uri},env={settings.Value.Env},label={settings.Value.Label}");
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc />
        public void Initialize()
        {
            Logger.LogInformation("Initializing Initializr configuration.");
            if (_config.ProjectMetadata is null)
            {
                Logger.LogError("Project metadata missing.");
            }

            if (_config.ProjectTemplates is null)
            {
                Logger.LogError("Project templates configuration missing.");
            }
        }

        /// <inheritdoc/>
        public InitializrConfig GetInitializrConfig()
        {
            return _config;
        }
    }
}
