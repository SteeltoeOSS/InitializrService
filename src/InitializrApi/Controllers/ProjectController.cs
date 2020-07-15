// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System.IO;
using System.Net;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="projectGenerator">Injected project generator.</param>
        public ProjectController(IProjectGenerator projectGenerator)
        {
            _projectGenerator = projectGenerator;
        }

        /// <summary>
        /// Implements <c>GET</c>.
        /// Generated project is bundled in a ZIP archive.
        /// </summary>
        /// <returns>A task containing the <c>GET</c> result which, if <see cref="HttpStatusCode.OK"/>, contains a project bundle stream.</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var spec = new ProjectSpecification();
            var stream = await _projectGenerator.GenerateProject(spec);
            var buf = new MemoryStream();
            await stream.CopyToAsync(buf);
            var bytes = buf.ToArray();
            stream.Close();
            buf.Close();
            return Ok(bytes);
        }
    }
}
