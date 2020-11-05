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
    public class ConfigController : InitializrApiControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly IInitializrConfigService _configService;

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
        public IActionResult GetInitializrConfiguration()
        {
            return Ok(_configService.GetInitializrConfig());
        }

        /// <summary>
        /// Implements <c>GET projectMetadata</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr project metadata.</returns>
        [HttpGet]
        [Route("projectMetadata")]
        public IActionResult GetProjectMetadata()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata);
        }

        /// <summary>
        /// Implements <c>GET steeltoeVersions</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr Steeltoe versions.</returns>
        [HttpGet]
        [Route("steeltoeVersions")]
        public IActionResult GetSteeltoeVersions()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata.SteeltoeVersion.Values);
        }

        /// <summary>
        /// Implements <c>GET dotNetFrameworks</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr .NET frameworks.</returns>
        [HttpGet]
        [Route("dotNetFrameworks")]
        public IActionResult GetDotNetFrameworks()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata.DotNetFramework.Values);
        }

        /// <summary>
        /// Implements <c>GET dotNetTemplates</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr .NET templates.</returns>
        [HttpGet]
        [Route("dotNetTemplates")]
        public IActionResult GetDotNetTemplates()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata.DotNetTemplate.Values);
        }

        /// <summary>
        /// Implements <c>GET languages</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr languages.</returns>
        [HttpGet]
        [Route("languages")]
        public IActionResult GetLanguages()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata.Language.Values);
        }

        /// <summary>
        /// Implements <c>GET archive types</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr archive types.</returns>
        [HttpGet]
        [Route("archiveTypes")]
        public IActionResult GetArchiveTypes()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata.Packaging.Values);
        }

        /// <summary>
        /// Implements <c>GET dependencies</c>.
        /// </summary>
        /// <returns>Returns a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains Initializr dependencies types.</returns>
        [HttpGet]
        [Route("dependencies")]
        public IActionResult GetDependencies()
        {
            return Ok(_configService.GetInitializrConfig().ProjectMetadata.Dependencies.Values);
        }

        /// <summary>
        /// Implements <c>GET projectTemplates</c>.
        /// </summary>
        /// <returns>Return a <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains InitializrApi project templates configuration.</returns>
        [HttpGet]
        [Route("projectTemplates")]
        public IActionResult GetProjectTemplatesConfig()
        {
            return Ok(_configService.GetInitializrConfig().ProjectTemplates);
        }
    }
}
