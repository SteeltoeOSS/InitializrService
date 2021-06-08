// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// Server configuration endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UiConfigController : InitializrApiControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly IUiConfigService _configService;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="UiConfigController"/> class.
        /// </summary>
        /// <param name="configService">Injected configuration repository.</param>
        /// <param name="logger">Injected logger.</param>
        public UiConfigController(
            IUiConfigService configService,
            ILogger<UiConfigController> logger)
            : base(logger)
        {
            _configService = configService;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Implements <c>GET</c> index.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains the Initializr configuration.</returns>
        [HttpGet]
        public IActionResult GetUiConfig()
        {
            return Ok(_configService.GetUiConfig());
        }

        /// <summary>
        /// Implements <c>GET steeltoeVersions</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr Steeltoe versions.</returns>
        [HttpGet]
        [Route("steeltoeVersions")]
        public IActionResult GetSteeltoeVersions()
        {
            return Ok(_configService.GetUiConfig().SteeltoeVersion.Values);
        }

        /// <summary>
        /// Implements <c>GET dotNetFrameworks</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr .NET frameworks.</returns>
        [HttpGet]
        [Route("dotNetFrameworks")]
        public IActionResult GetDotNetFrameworks()
        {
            return Ok(_configService.GetUiConfig().DotNetFramework.Values);
        }

        /// <summary>
        /// Implements <c>GET dotNetTemplates</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr .NET templates.</returns>
        [HttpGet]
        [Route("dotNetTemplates")]
        public IActionResult GetDotNetTemplates()
        {
            return Ok(_configService.GetUiConfig().DotNetTemplate.Values);
        }

        /// <summary>
        /// Implements <c>GET languages</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr languages.</returns>
        [HttpGet]
        [Route("languages")]
        public IActionResult GetLanguages()
        {
            return Ok(_configService.GetUiConfig().Language.Values);
        }

        /// <summary>
        /// Implements <c>GET archive types</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr archive types.</returns>
        [HttpGet]
        [Route("archiveTypes")]
        public IActionResult GetArchiveTypes()
        {
            return Ok(_configService.GetUiConfig().Packaging.Values);
        }

        /// <summary>
        /// Implements <c>GET dependencies</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr dependencies types.</returns>
        [HttpGet]
        [Route("dependencies")]
        public IActionResult GetDependencies()
        {
            return Ok(_configService.GetUiConfig().Dependencies.Values);
        }
    }
}
