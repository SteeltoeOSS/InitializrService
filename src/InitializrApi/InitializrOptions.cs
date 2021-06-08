// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Steeltoe.InitializrApi
{
    /// <summary>
    /// Initializr configuration options.
    /// </summary>
    public sealed class InitializrOptions
    {
        /// <summary>
        /// The value of the appsettings section name.
        /// </summary>
        public const string Initializr = "Initializr";

        /// <summary>
        /// Gets or sets the path to the logo used for doc header.
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the configuration dictionary.
        /// </summary>
        public Dictionary<string, string> UiConfig { get; set; }

        /// <summary>
        /// Gets or sets the CORS dictionary.
        /// </summary>
        public Dictionary<string, string> Cors { get; set; }

        /// <summary>
        /// Gets the path to the UI configuration file.
        /// </summary>
        public string UiConfigPath
        {
            get => UiConfig?["Path"];
        }

        /// <summary>
        /// Gets the CORS origin.
        /// </summary>
        public string CorsOrigin
        {
            get => Cors?["Origin"];
        }
    }
}
