// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// Project generation endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectGenerator _projectGenerator;

        private readonly ILogger<ProjectController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="projectGenerator">Injected project generator.</param>
        /// <param name="logger">Injected logger.</param>
        public ProjectController(IProjectGenerator projectGenerator, ILogger<ProjectController> logger)
        {
            _projectGenerator = projectGenerator;
            _logger = logger;
        }

        /// <summary>
        /// Implements <c>GET</c>.
        /// Generated project is bundled in a ZIP archive.
        /// </summary>
        /// <returns>A task containing the <c>GET</c> result which, if is <see cref="FileContentResult"/>, contains a project bundle archive stream.</returns>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] ProjectSpecification spec)
        {
            _logger.LogDebug($"Project specification: {spec}");
            var stream = await _projectGenerator.GenerateProject(spec);
            byte[] bytes;
            await using (var buf = new MemoryStream())
            {
                await stream.CopyToAsync(buf);
                bytes = buf.ToArray();
            }

            return File(bytes, MediaTypeNames.Application.Zip, $"{spec.Name}.zip");
        }
    }
}
