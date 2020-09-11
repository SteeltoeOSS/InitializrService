// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Base class for InitializrApi services.
    /// </summary>
    public abstract class InitializrApiServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializrApiServiceBase"/> class.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        protected InitializrApiServiceBase(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }
    }
}
