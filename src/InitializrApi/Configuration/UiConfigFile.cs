// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Steeltoe.InitializrApi.Utilities;
using System;
using System.IO;

namespace Steeltoe.InitializrApi.Configuration
{
    /// <summary>
    /// An <see cref="IUiConfigService"/> using a local configuration file backend.
    /// </summary>
    public class UiConfigFile : InitializrApiServiceBase, IUiConfigService
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly InitializrApiOptions _apiOptions;

        private UiConfig _uiConfig;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="UiConfigFile"/> class.
        /// </summary>
        /// <param name="options">Injected options from appsettings.</param>
        /// <param name="logger">Injected logger.</param>
        public UiConfigFile(IOptions<InitializrApiOptions> options, ILogger<UiConfigFile> logger)
            : base(logger)
        {
            _apiOptions = options.Value;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc />
        public void Initialize()
        {
            Logger.LogInformation("loading configuration: {Path}", _apiOptions.UiConfigPath);
            try
            {
                var configJson = File.ReadAllText(_apiOptions.UiConfig["Path"]);
                _uiConfig = Serializer.DeserializeJson<UiConfig>(configJson);
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException($"UI configuration file path does not exist: {_apiOptions.UiConfigPath}");
            }
            catch (UnauthorizedAccessException)
            {
                throw new ArgumentException(
                    $"UI configuration file path is not a file or cannot be read: {_apiOptions.UiConfigPath}");
            }
        }

        /// <inheritdoc />
        public UiConfig GetUiConfig()
        {
            return _uiConfig;
        }
    }
}
