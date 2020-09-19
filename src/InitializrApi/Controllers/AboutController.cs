// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// About endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : InitializrApiControllerBase
    {
        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutController"/> class.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        public AboutController(ILogger<AboutController> logger)
            : base(logger)
        {
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Implements <c>GET</c>.
        /// </summary>
        /// <returns>A <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains the server "About" details.</returns>
        [HttpGet]
        public IActionResult GetAbout()
        {
            return Ok(Program.About);
        }
    }
}
