// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Config;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.Configuration
{
    /// <summary>
    /// An <see cref="IUiConfigService"/> using a <a href="https://cloud.spring.io/spring-cloud-config/reference/html/#_spring_cloud_config_server">Spring Cloud Config Server</a> backend.
    /// </summary>
    public class UiConfigService : InitializrApiServiceBase, IUiConfigService
    {
        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="UiConfigService"/> class.
        /// </summary>
        /// <param name="configuration">Injected configuration from Config Server.</param>
        /// <param name="settings">Injected settings from Config Server.</param>
        /// <param name="logger">Injected logger.</param>
        public UiConfigService(
            IOptionsSnapshot<UiConfig> configuration,
            IOptions<ConfigServerClientSettingsOptions> settings,
            ILogger<UiConfigService> logger)
            : base(logger)
        {
            UiConfig = configuration.Value;
            Logger.LogInformation(
                "Config Server: uri={ConfigServer},env={Environment},label={Label}",
                settings.Value.Uri,
                settings.Value.Env,
                settings.Value.Label);
        }

        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <inheritdoc cref="IUiConfigService"/>
        public UiConfig UiConfig { get; }
    }
}
