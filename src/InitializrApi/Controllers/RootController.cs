// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Steeltoe.InitializrApi.Controllers
{
    /// <summary>
    /// About endpoint.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class RootController : InitializrApiControllerBase
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private const string NewLine = "\n";

        private readonly InitializrOptions _options;

        private readonly IInitializrConfigService _configService;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="RootController"/> class.
        /// </summary>
        /// <param name="options">Injected Initializr options.</param>
        /// <param name="configService">Injected Initializr configuration service.</param>
        /// <param name="logger">Injected logger.</param>
        public RootController(
            IOptions<InitializrOptions> options,
            IInitializrConfigService configService,
            ILogger<RootController> logger)
            : base(logger)
        {
            _options = options.Value;
            _configService = configService;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Implements <c>GET</c>.
        /// </summary>
        /// <returns>A <c>GET</c> result which, if is <see cref="OkObjectResult"/>, contains the server "About" details.</returns>
        [HttpGet]
        public IActionResult GetHelp()
        {
            var help = new List<string>();
            if (!(_options?.Logo is null))
            {
                try
                {
                    help.Add(string.Empty);
                    var logoPath = _options.Logo;
                    System.IO.File.ReadAllLines(logoPath).ToList().ForEach(l => help.Add(l));
                    help.Add(string.Empty);
                }
                catch (Exception e)
                {
                    help.Add($"!!! failed to load logo: {e.Message}");
                    help.Add(string.Empty);
                }
            }

            help.Add(" :: Steeltoe Initializr ::  https://start.steeltoe.io");
            help.Add(string.Empty);
            var metadata = _configService.GetInitializrConfig().ProjectMetadata;
            help.Add("This service generates quickstart projects that can be easily customized.");
            help.Add("Possible customizations include a project's dependencies and .NET target framework.");
            help.Add(string.Empty);
            help.Add("The URI templates take a set of parameters to customize the result of a request.");
            var table = new List<List<string>>
            {
                new List<string> { "Parameter", "Description", "Default value" },
                new List<string> { "name", "project name", metadata.Name.Default },
                new List<string> { "applicationName", "application name", metadata.ApplicationName.Default },
                new List<string> { "namespace", "namespace", metadata.Namespace.Default },
                new List<string> { "description", "project description", metadata.Description.Default },
                new List<string> { "steeltoeVersion", "Steeltoe version", metadata.SteeltoeVersion.Default },
                new List<string> { "dotNetFramework", "target .NET framework", metadata.DotNetFramework.Default },
                new List<string> { "dotNetTemplate", ".NET template", metadata.DotNetTemplate.Default },
                new List<string> { "language", "programming language", metadata.Language.Default },
                new List<string> { "packaging", "project packaging", metadata.Packaging.Default },
            };
            help.AddRange(ToTable(table));
            help.Add(string.Empty);
            help.Add("The following section has a list of supported identifiers for the comma-separated");
            help.Add("list of \"dependencies\".");
            table = new List<List<string>>
            {
                new List<string> { "Id", "Description", "Required Steeltoe version" },
            };
            foreach (var group in metadata.Dependencies.Values)
            {
                foreach (var dependency in group.Values)
                {
                    table.Add(new List<string>
                    {
                        dependency.Id,
                        dependency.Description,
                        new ReleaseRange(dependency.SteeltoeVersionRange).ToPrettyString(),
                    });
                }
            }

            help.AddRange(ToTable(table));
            help.Add(string.Empty);
            help.Add("Examples:");
            help.Add(string.Empty);
            help.Add("To create a default project:");
            help.Add("\t$ http https://start.steeltoe.io/api/project -d");
            help.Add(string.Empty);
            help.Add("To create a project targeting Steeltoe 2.5.1 and netcoreapp2.1:");
            help.Add(
                "\t$ http https://start.steeltoe.io/api/project steeltoeVersion==2.5.1 dotNetFramework==netcoreapp2.1 -d");
            help.Add(string.Empty);
            help.Add("To create a project with a actuator endpoints and a Redis backend:");
            help.Add("\t$ http https://start.steeltoe.io/api/project dependencies==actuators,redis -d");

            return Ok(string.Join(NewLine, help));
        }

        private IEnumerable<string> ToTable(List<List<string>> rows)
        {
            var max = new int[rows[0].Count];
            for (var c = 0; c < max.Length; ++c)
            {
                max[c] = 0;
                for (int r = 0; r < rows.Count; ++r)
                {
                    max[c] = Math.Max(max[c], rows[r][c].Length);
                }
            }

            var border = "+";
            var format = "|";
            for (int c = 0; c < max.Length; ++c)
            {
                border += new string('-', max[c] + 2) + "+";
                format += $" {{{c},-{max[c]}}} |";
            }

            var table = new List<string>();
            table.Add(border);
            table.Add(string.Format(format, rows[0].ToArray<object>()));
            table.Add(border);
            for (int r = 1; r < rows.Count; ++r)
            {
                table.Add(string.Format(format, rows[r].ToArray<object>()));
            }

            table.Add(border);
            return table;
        }
    }
}
