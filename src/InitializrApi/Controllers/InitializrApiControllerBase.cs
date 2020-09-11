// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// Base class for InitializrApi controllers.
    /// </summary>
    public abstract class InitializrApiControllerBase : ControllerBase
    {
        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializrApiControllerBase"/> class.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        protected InitializrApiControllerBase(ILogger logger)
        {
            Logger = logger;
        }

        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }
    }
}
