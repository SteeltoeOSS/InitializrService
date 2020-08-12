// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// About endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : ControllerBase
    {
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
