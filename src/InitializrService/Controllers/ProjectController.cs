// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.InitializrService.Config;
using Steeltoe.InitializrService.Models;
using Steeltoe.InitializrService.Services;
using System;
using System.Threading.Tasks;

namespace Steeltoe.InitializrService.Controllers
{
    /// <summary>
    /// Project generation endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : InitializrServiceControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                             *
         * ----------------------------------------------------------------- */

        private readonly IUiConfigService _uiConfigService;

        private readonly IProjectGenerator _projectGenerator;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="uiConfigService">Injected Initializr configuration service.</param>
        /// <param name="projectGenerator">Injected project generator.</param>
        /// <param name="logger">Injected logger.</param>
        public ProjectController(
            IUiConfigService uiConfigService,
            IProjectGenerator projectGenerator,
            ILogger<ProjectController> logger)
            : base(logger)
        {
            _uiConfigService = uiConfigService;
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
            var defaults = _uiConfigService.UiConfig;
            var normalizedSpec = new ProjectSpec()
            {
                Name = spec.Name ?? defaults?.Name?.Default,
                Namespace = spec.Namespace ?? defaults?.Namespace?.Default,
                Description = spec.Description ?? defaults?.Description?.Default,
                SteeltoeVersion = spec.SteeltoeVersion ?? defaults?.SteeltoeVersion?.Default,
                DotNetFramework = spec.DotNetFramework ?? defaults?.DotNetFramework?.Default,
                Language = spec.Language ?? defaults?.Language?.Default,
                Packaging = spec.Packaging ?? defaults?.Packaging?.Default,
                Dependencies = spec.Dependencies ?? defaults?.Dependencies?.Default,
            };

            if (normalizedSpec.Dependencies != null)
            {
                var deps = normalizedSpec.Dependencies.Split(',');
                for (var i = 0; i < deps.Length; ++i)
                {
                    var found = false;
                    foreach (var group in defaults.Dependencies.Values)
                    {
                        foreach (var dep in group.Values)
                        {
                            if (!deps[i].Equals(dep.Id, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            // This is effectively unreachable. InitializrWeb filters out incompatible dependencies.
                            /*
                            var steeltoeRange = new ReleaseRange(dep.SteeltoeVersionRange);
                            if (!steeltoeRange.Accepts(normalizedSpec.SteeltoeVersion))
                            {
                                return NotFound(
                                    $"No dependency '{deps[i]}' found for Steeltoe version {normalizedSpec.SteeltoeVersion}.");
                            }
                            */

                            // This is effectively unreachable. InitializrWeb filters out incompatible dependencies.
                            /*
                            var frameworkRange = new ReleaseRange(dep.DotNetFrameworkRange);
                            if (!frameworkRange.Accepts(normalizedSpec.DotNetFramework))
                            {
                                return NotFound(
                                    $"No dependency '{deps[i]}' found for .NET framework {normalizedSpec.DotNetFramework}.");
                            }
                            */

                            deps[i] = dep.Id;
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

            if (normalizedSpec.Packaging is null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Default packaging not configured.");
            }

            Logger.LogInformation("Project specification: {ProjectSpec}", normalizedSpec);
            try
            {
                // Translate Spring-style wildcard 'x' to NuGet-style wildcard '*'.
                // See https://docs.spring.io/initializr/docs/current-SNAPSHOT/reference/html/#dependencies-compatibility-range.
                normalizedSpec.SteeltoeVersion = normalizedSpec.SteeltoeVersion.Replace('x', '*');

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
