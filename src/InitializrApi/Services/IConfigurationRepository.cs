// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for project configuration repository implementations.
    /// </summary>
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Gets the project generation configuration.
        /// </summary>
        /// <returns>A task containing server configuration.</returns>
        public Task<InitializrApiConfiguration> GetConfiguration();
    }
}
