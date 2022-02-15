// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;

namespace Steeltoe.InitializrService.Services
{
    /// <summary>
    /// Base class for InitializrService services.
    /// </summary>
    public abstract class InitializrServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializrServiceBase"/> class.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        protected InitializrServiceBase(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }
    }
}
