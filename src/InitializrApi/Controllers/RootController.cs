// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.InitializrApi.Config;
using Steeltoe.InitializrApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private readonly InitializrApiOptions _apiOptions;

        private readonly IUiConfigService _uiConfigService;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="RootController"/> class.
        /// </summary>
        /// <param name="options">Injected Initializr options.</param>
        /// <param name="uiConfigService">Injected Initializr configuration service.</param>
        /// <param name="logger">Injected logger.</param>
        public RootController(
            IOptions<InitializrApiOptions> options,
            IUiConfigService uiConfigService,
            ILogger<RootController> logger)
            : base(logger)
        {
            _apiOptions = options.Value;
            _uiConfigService = uiConfigService;
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
            if (_apiOptions?.Logo is not null)
            {
                try
                {
                    help.Add(string.Empty);
                    var logoPath = _apiOptions.Logo;
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
            var uiConfig = _uiConfigService.UiConfig;
            help.Add("This service generates quickstart projects that can be easily customized.");
            help.Add("Possible customizations include a project's dependencies and .NET target framework.");
            help.Add(string.Empty);
            help.Add("The URI templates take a set of parameters to customize the result of a request.");
            var table = new List<List<string>>
            {
                new () { "Parameter", "Description", "Default value" },
                new () { "name", "project name", uiConfig.Name.Default },
                new () { "namespace", "namespace", uiConfig.Namespace.Default },
                new () { "description", "project description", uiConfig.Description.Default },
                new () { "steeltoeVersion", "Steeltoe version", uiConfig.SteeltoeVersion.Default },
                new () { "dotNetFramework", "target .NET framework", uiConfig.DotNetFramework.Default },
                new () { "language", "programming language", uiConfig.Language.Default },
                new () { "packaging", "project packaging", uiConfig.Packaging.Default },
            };
            help.AddRange(ToTable(table));
            help.Add(string.Empty);
            help.Add("The following section has a list of supported identifiers for the comma-separated");
            help.Add("list of \"dependencies\".");
            table = new List<List<string>>
            {
                new () { "Id", "Description", "Required Steeltoe version" },
            };
            table.AddRange(
                from @group in uiConfig.Dependencies.Values
                from dependency in @group.Values
                select new List<string>
                {
                    dependency.Id,
                    dependency.Description,
                    new ReleaseRange(dependency.SteeltoeVersionRange).ToPrettyString(),
                });

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

            const char newline = '\n';
            return Ok(string.Join(newline, help));
        }

        private static IEnumerable<string> ToTable(IReadOnlyList<List<string>> rows)
        {
            var columnMaxWidth = new int[rows[0].Count];
            for (var column = 0; column < columnMaxWidth.Length; ++column)
            {
                columnMaxWidth[column] = 0;
                foreach (var row in rows)
                {
                    columnMaxWidth[column] = Math.Max(columnMaxWidth[column], row[column].Length);
                }
            }

            const char borderHorizontal = '-';
            const char borderVertical = '|';
            const char borderJunction = '+';

            var borderBuffer = new StringBuilder();
            borderBuffer.Append(borderJunction);
            var lineFormatBuffer = new StringBuilder();
            lineFormatBuffer.Append(borderVertical);
            for (var column = 0; column < columnMaxWidth.Length; ++column)
            {
                borderBuffer.Append(new string(borderHorizontal, columnMaxWidth[column] + 2)).Append(borderJunction);
                lineFormatBuffer.Append(" {").Append(column).Append(",-").Append(columnMaxWidth[column]).Append("} ")
                    .Append(borderVertical);
            }

            var borderRule = borderBuffer.ToString();
            var lineFormat = lineFormatBuffer.ToString();
            var table = new List<string>
            {
                borderRule, string.Format(lineFormat, rows[0].ToArray<object>()), borderRule,
            };
            for (var row = 1; row < rows.Count; ++row)
            {
                table.Add(string.Format(lineFormat, rows[row].ToArray<object>()));
            }

            table.Add(borderRule);
            return table;
        }
    }
}
