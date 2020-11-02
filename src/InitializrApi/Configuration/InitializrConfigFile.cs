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
    /// An <see cref="IInitializrConfigService"/> using a local configuration file backend.
    /// </summary>
    public class InitializrConfigFile : InitializrApiServiceBase, IInitializrConfigService
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly InitializrOptions _options;

        private InitializrConfig _config;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializrConfigFile"/> class.
        /// </summary>
        /// <param name="options">Injected options from appsettings.</param>
        /// <param name="logger">Injected logger.</param>
        public InitializrConfigFile(IOptions<InitializrOptions> options, ILogger<InitializrConfigFile> logger)
            : base(logger)
        {
            _options = options.Value;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc />
        public void Initialize()
        {
            Logger.LogInformation("loading configuration: {Path}", _options.ConfigurationPath);
            try
            {
                var configJson = File.ReadAllText(_options.Configuration["Path"]);
                _config = Serializer.DeserializeJson<InitializrConfig>(configJson);
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException($"Configuration file path does not exist: {_options.ConfigurationPath}");
            }
            catch (UnauthorizedAccessException)
            {
                throw new ArgumentException(
                    $"Configuration file path is not a file or cannot be read: {_options.ConfigurationPath}");
            }
        }

        /// <inheritdoc />
        public InitializrConfig GetInitializrConfig()
        {
            return _config;
        }
    }
}
