// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Steeltoe.Initializr.WebApi.Models;

namespace Steeltoe.Initializr.WebApi.Services
{
    /// <summary>
    /// Contract for configuration repository implementations.
    /// </summary>
    public interface IMetadataRepository
    {
        /// <summary>
        /// Gets the project generation configuration.
        /// </summary>
        /// <returns>project generation configuration</returns>
        public Task<ConfigurationMetadata> GetConfiguration();
    }
}
