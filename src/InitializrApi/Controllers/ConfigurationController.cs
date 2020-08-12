// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Steeltoe.InitializrApi.Services;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// Server configuration endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRepository _configurationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationController"/> class.
        /// </summary>
        /// <param name="configurationRepository">Injected configuration repository.</param>
        public ConfigurationController(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        /// <summary>
        /// Implements <c>GET</c> index.
        /// </summary>
        /// <returns>A task containing the <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains the server configuration.</returns>
        [HttpGet]
        public async Task<IActionResult> GetConfiguration()
        {
            var config = await _configurationRepository.GetConfiguration();
            return Ok(config);
        }

        /// <summary>
        /// Implements <c>GET metadata</c>.
        /// </summary>
        /// <returns>A task containing the <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains project metadata configuration.</returns>
        [HttpGet]
        [Route("metadata")]
        public async Task<IActionResult> GetMetadata()
        {
            var config = await _configurationRepository.GetConfiguration();
            return Ok(config.Metadata);
        }

        /// <summary>
        /// Implements <c>GET templates</c>.
        /// </summary>
        /// <returns>A task containing the <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains templates configuration.</returns>
        [HttpGet]
        [Route("templates")]
        public async Task<IActionResult> GetTemplates()
        {
            var config = await _configurationRepository.GetConfiguration();
            return Ok(config.Templates);
        }
    }
}
