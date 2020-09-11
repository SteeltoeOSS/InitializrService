// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// Server configuration endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : InitializrApiControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly InitializrConfig _config;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigController"/> class.
        /// </summary>
        /// <param name="configService">Injected configuration repository.</param>
        /// <param name="logger">Injected logger.</param>
        public ConfigController(
            IInitializrConfigService configService,
            ILogger<ConfigController> logger)
            : base(logger)
        {
            _config = configService.GetInitializrConfig();
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Implements <c>GET</c> index.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains the Initializr configuration.</returns>
        [HttpGet]
        public IActionResult GetInitializrConfiguration()
        {
            return Ok(_config);
        }

        /// <summary>
        /// Implements <c>GET projectMetadata</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr project metadata.</returns>
        [HttpGet]
        [Route("projectMetadata")]
        public IActionResult GetProjectMetadata()
        {
            return Ok(_config.ProjectMetadata);
        }

        /// <summary>
        /// Implements <c>GET projectTemplates</c>.
        /// </summary>
        /// <returns>Return a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains InitializrApi project templates configuration.</returns>
        [HttpGet]
        [Route("projectTemplates")]
        public IActionResult GetProjectTemplatesConfig()
        {
            return Ok(_config.ProjectTemplates);
        }
    }
}
