// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Steeltoe.InitializrApi
{
    /// <summary>
    /// Initializr configuration options.
    /// </summary>
    public sealed class InitializrApiOptions
    {
        /// <summary>
        /// The value of the appsettings section name.
        /// </summary>
        public const string InitializrApi = "InitializrApi";

        /// <summary>
        /// Gets or sets the Net Core Tool Service config dictionary.
        /// </summary>
        public Dictionary<string, string> NetCoreToolService { get; set; }

        /// <summary>
        /// Gets the URI for the Net Core Tool Service.
        /// </summary>
        public string NetCoreToolServiceUri => NetCoreToolService?["Uri"];

        /// <summary>
        /// Gets or sets the UI config dictionary.
        /// </summary>
        public Dictionary<string, string> UiConfig { get; set; }

        /// <summary>
        /// Gets the path to the UI config.
        /// </summary>
        public string UiConfigPath => UiConfig?["Path"];

        /// <summary>
        /// Gets or sets the path to the logo used for doc header.
        /// </summary>
        public string Logo { get; set; }
    }
}
