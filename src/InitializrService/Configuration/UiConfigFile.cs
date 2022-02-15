// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.InitializrService.Config;
using Steeltoe.InitializrService.Services;
using Steeltoe.InitializrService.Utilities;
using System;
using System.IO;

namespace Steeltoe.InitializrService.Configuration
{
    /// <summary>
    /// An <see cref="IUiConfigService"/> using a local configuration file backend.
    /// </summary>
    public class UiConfigFile : InitializrServiceBase, IUiConfigService
    {
        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="UiConfigFile"/> class.
        /// </summary>
        /// <param name="options">Injected options from appsettings.</param>
        /// <param name="logger">Injected logger.</param>
        public UiConfigFile(IOptions<InitializrServiceOptions> options, ILogger<UiConfigFile> logger)
            : base(logger)
        {
            var apiOptions = options.Value;
            Logger.LogInformation("loading configuration: {Path}", apiOptions.UiConfigPath);
            try
            {
                var configJson = File.ReadAllText(apiOptions.UiConfig["Path"]);
                UiConfig = Serializer.DeserializeJson<UiConfig>(configJson);
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException($"UI configuration file path does not exist: {apiOptions.UiConfigPath}");
            }
            catch (UnauthorizedAccessException)
            {
                throw new ArgumentException(
                    $"UI configuration file path is not a file or cannot be read: {apiOptions.UiConfigPath}");
            }
        }

        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <inheritdoc cref="IUiConfigService"/>
        public UiConfig UiConfig { get; }
    }
}
