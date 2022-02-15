// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.InitializrService.Services;

namespace Steeltoe.InitializrService.Controllers
{
    /// <summary>
    /// Server configuration endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UiConfigController : InitializrServiceControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                             *
         * ----------------------------------------------------------------- */

        private readonly IUiConfigService _uiConfigService;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="UiConfigController"/> class.
        /// </summary>
        /// <param name="uiConfigService">Injected configuration repository.</param>
        /// <param name="logger">Injected logger.</param>
        public UiConfigController(
            IUiConfigService uiConfigService,
            ILogger<UiConfigController> logger)
            : base(logger)
        {
            _uiConfigService = uiConfigService;
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
            return Ok(_uiConfigService.UiConfig);
        }

        /// <summary>
        /// Implements <c>GET steeltoeVersions</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr Steeltoe versions.</returns>
        [HttpGet]
        [Route("steeltoeVersions")]
        public IActionResult GetSteeltoeVersions()
        {
            return Ok(_uiConfigService.UiConfig.SteeltoeVersion.Values);
        }

        /// <summary>
        /// Implements <c>GET dotNetFrameworks</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr .NET frameworks.</returns>
        [HttpGet]
        [Route("dotNetFrameworks")]
        public IActionResult GetDotNetFrameworks()
        {
            return Ok(_uiConfigService.UiConfig.DotNetFramework.Values);
        }

        /// <summary>
        /// Implements <c>GET languages</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr languages.</returns>
        [HttpGet]
        [Route("languages")]
        public IActionResult GetLanguages()
        {
            return Ok(_uiConfigService.UiConfig.Language.Values);
        }

        /// <summary>
        /// Implements <c>GET archive types</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr archive types.</returns>
        [HttpGet]
        [Route("archiveTypes")]
        public IActionResult GetArchiveTypes()
        {
            return Ok(_uiConfigService.UiConfig.Packaging.Values);
        }

        /// <summary>
        /// Implements <c>GET dependencies</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr dependencies types.</returns>
        [HttpGet]
        [Route("dependencies")]
        public IActionResult GetDependencies()
        {
            return Ok(_uiConfigService.UiConfig.Dependencies.Values);
        }
    }
}
