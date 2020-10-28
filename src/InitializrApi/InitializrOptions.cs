// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
        /// Gets or sets the path to a configuration file.
        /// </summary>
        public string Path { get; set; }
    }
}
