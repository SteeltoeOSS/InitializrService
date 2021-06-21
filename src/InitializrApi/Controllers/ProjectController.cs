// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// Project generation endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : InitializrApiControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly IUiConfigService _configService;

        private readonly IProjectGenerator _projectGenerator;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="configService">Injected Initializr configuration service.</param>
        /// <param name="projectGenerator">Injected project generator.</param>
        /// <param name="logger">Injected logger.</param>
        public ProjectController(
            IUiConfigService configService,
            IProjectGenerator projectGenerator,
            ILogger<ProjectController> logger)
            : base(logger)
        {
            _configService = configService;
            _projectGenerator = projectGenerator;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Implements <c>GET</c>.
        /// Generated project is bundled in a ZIP archive.
        /// </summary>
        /// <returns>A task containing the <c>GET</c> result which, if is <see cref="FileContentResult"/>, contains a project bundle archive stream.</returns>
        [AcceptVerbs("GET")]
        public async Task<ActionResult> GetProjectArchive([FromQuery] ProjectSpec spec)
        {
            var defaults = _configService.GetUiConfig();
            var normalizedSpec = new ProjectSpec()
            {
                Name = spec.Name ?? defaults?.Name?.Default,
                Description = spec.Description ?? defaults?.Description?.Default,
                Namespace = spec.Namespace ?? defaults?.Namespace?.Default,
                SteeltoeVersion = spec.SteeltoeVersion ?? defaults?.SteeltoeVersion?.Default,
                DotNetFramework = spec.DotNetFramework ?? defaults?.DotNetFramework?.Default,
                DotNetTemplate = spec.DotNetTemplate ?? defaults?.DotNetTemplate?.Default,
                Language = spec.Language ?? defaults?.Language?.Default,
                Packaging = spec.Packaging ?? defaults?.Packaging?.Default,
                Dependencies = spec.Dependencies ?? defaults?.Dependencies?.Default,
            };
            if (normalizedSpec.Packaging is null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Default packaging not configured.");
            }

            if (normalizedSpec.Dependencies != null)
            {
                var caseSensitiveDeps = new List<string>();
                if (defaults.Dependencies?.Values != null)
                {
                    foreach (var group in defaults.Dependencies.Values)
                    {
                        foreach (var dep in group.Values)
                        {
                            caseSensitiveDeps.Add(dep.Id);
                        }
                    }
                }

                var deps = normalizedSpec.Dependencies.Split(',');
                for (int i = 0; i < deps.Length; ++i)
                {
                    var found = false;
                    foreach (var caseSensitiveDep in caseSensitiveDeps)
                    {
                        if (caseSensitiveDep.Equals(deps[i], StringComparison.OrdinalIgnoreCase))
                        {
                            deps[i] = caseSensitiveDep;
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        return NotFound($"Dependency '{deps[i]}' not found.");
                    }
                }

                normalizedSpec.Dependencies = string.Join(',', deps);
            }

            Logger.LogDebug("Project specification: {ProjectSpec}", normalizedSpec);
            try
            {
                var projectPackage = await _projectGenerator.GenerateProjectArchive(normalizedSpec);
                return File(
                    projectPackage,
                    $"application/zip",
                    $"{normalizedSpec.Name}.zip");
            }
            catch (NoProjectForSpecException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidSpecException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
