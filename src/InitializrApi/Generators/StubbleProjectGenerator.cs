// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Parsers;
using Steeltoe.InitializrApi.Services;
using Stubble.Core;
using Stubble.Core.Builders;
using System.Collections.Generic;
using System.IO;

namespace Steeltoe.InitializrApi.Generators
{
    /// <summary>
    /// An <see cref="IProjectGenerator"/> implementation using <a gref="https://github.com/StubbleOrg/Stubble">Stubble</a>, a Mustache template engine implemented in C#.
    /// </summary>
    public class StubbleProjectGenerator : InitializrApiServiceBase, IProjectGenerator
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly IProjectTemplateRegistry _projectTemplateRegistry;

        private readonly StubbleVisitorRenderer _renderer;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="StubbleProjectGenerator"/> class.
        /// </summary>
        /// <param name="projectTemplateRegistry">Injected template registry.</param>
        /// <param name="logger">Injected logger.</param>
        public StubbleProjectGenerator(
            IProjectTemplateRegistry projectTemplateRegistry,
            ILogger<StubbleProjectGenerator> logger)
            : base(logger)
        {
            _projectTemplateRegistry = projectTemplateRegistry;
            _renderer = new StubbleBuilder()
                .Configure(settings => { settings.SetIgnoreCaseOnKeyLookup(true); })
                .Build();
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc/>
        public Project GenerateProject(ProjectSpec spec)
        {
            var template = _projectTemplateRegistry.Lookup(spec);
            if (template is null)
            {
                Logger.LogDebug("No project template found for spec: {ProjectSpec}", spec);
                return null;
            }

            var parameters = new Dictionary<string, object>
            {
                ["Name"] = spec.Name,
                ["Description"] = spec.Description,
                ["Namespace"] = spec.Namespace,
                ["SteeltoeVersion"] = spec.SteeltoeVersion,
                ["DotNetFramework"] = spec.DotNetFramework,
                ["DotNetTemplate"] = spec.DotNetTemplate,
                ["Language"] = spec.Language,
            };
            if (spec.Dependencies != null)
            {
                foreach (var dependency in spec.Dependencies?.Split(','))
                {
                    parameters[dependency] = true;
                }
            }

            if (template.Parameters != null)
            {
                foreach (var templateParameter in template.Parameters)
                {
                    if (templateParameter.Value != null)
                    {
                        parameters[templateParameter.Name] = templateParameter.Value;
                    }
                    else if (templateParameter.Expression != null)
                    {
                        parameters[templateParameter.Name] =
                            new ExpressionParser(templateParameter.Expression).Evaluate(parameters);
                    }
                }
            }

            var project = new Project();
            project.FileEntries.Add(new FileEntry { Path = $"{spec.Name}/" });
            foreach (var fileEntry in template.Manifest)
            {
                if (fileEntry.Dependencies != null)
                {
                    var dependencySatisfied = false;
                    foreach (var fileEntryDependency in fileEntry.Dependencies.Split(','))
                    {
                        parameters.TryGetValue(fileEntryDependency, out var dependency);
                        if (dependency != null)
                        {
                            if (dependency is bool)
                            {
                                if ((bool)dependency)
                                {
                                    dependencySatisfied = true;
                                }
                            }
                            else
                            {
                                dependencySatisfied = true;
                            }

                            if (dependencySatisfied)
                            {
                                break;
                            }
                        }
                    }

                    if (!dependencySatisfied)
                    {
                        continue;
                    }
                }

                var path = fileEntry.Rename is null ? fileEntry.Path : _renderer.Render(fileEntry.Rename, parameters);
                var text = fileEntry.Text is null ? null : _renderer.Render(fileEntry.Text, parameters);
                project.FileEntries.Add(new FileEntry { Path = Path.Join(spec.Name, path), Text = text });
            }

            return project;
        }
    }
}
