// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Expressions;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Stubble.Core;
using Stubble.Core.Builders;
using System.Collections.Generic;

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
                    parameters[templateParameter.Name] =
                        new Expression<bool>(templateParameter.Expression).Evaluate(parameters);
                }
            }

            var project = new Project();
            foreach (var fileEntry in template.Manifest)
            {
                if (fileEntry.Dependencies != null)
                {
                    var dependencySatisfied = false;
                    foreach (var fileEntryDependency in fileEntry.Dependencies.Split(','))
                    {
                        if (parameters.ContainsKey(fileEntryDependency))
                        {
                            dependencySatisfied = true;
                            break;
                        }
                    }

                    if (!dependencySatisfied)
                    {
                        continue;
                    }
                }

                var path = fileEntry.Rename is null ? fileEntry.Path : _renderer.Render(fileEntry.Rename, parameters);
                var text = fileEntry.Text is null ? null : _renderer.Render(fileEntry.Text, parameters);
                project.FileEntries.Add(new FileEntry { Path = path, Text = text });
            }

            return project;
        }
    }
}
