// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.Generators
{
    /// <summary>
    /// a A project generator that uses a NetCoreToolService.
    /// </summary>
    public class NetCoreToolProjectGenerator : InitializrApiServiceBase, IProjectGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetCoreToolProjectGenerator"/> class.
        /// </summary>
        /// <param name="options">Injected options.</param>
        /// <param name="logger">Injected logger.</param>
        public NetCoreToolProjectGenerator(
            IOptions<InitializrApiOptions> options,
            ILogger<NetCoreToolProjectGenerator> logger)
            : base(logger)
        {
            Logger.LogInformation("Net Core Tool Service URI: {Uri}", options.Value.NetCoreToolServiceUri);
        }

        /// <inheritdoc/>
        public byte[] GenerateProjectArchive(ProjectSpec spec)
        {
            return null;
        }
    }
}
