// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Config;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for Initializr configuration service implementations.
    /// </summary>
    public interface IUiConfigService
    {
        /// <summary>
        /// Gets the Initializr configuration.
        /// </summary>
        public UiConfig UiConfig { get; }
    }
}
