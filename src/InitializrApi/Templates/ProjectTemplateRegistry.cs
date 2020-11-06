// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Steeltoe.InitializrApi.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using YamlDotNet.Core;

namespace Steeltoe.InitializrApi.Templates
{
    /// <summary>
    /// A simple <see cref="IProjectTemplateRegistry"/> implementation.
    /// </summary>
    public class ProjectTemplateRegistry : InitializrApiServiceBase, IProjectTemplateRegistry
    {
        /* ----------------------------------------------------------------- *
         * constants                                                         *
         * ----------------------------------------------------------------- */

        private const string Metadata = ".IZR/metadata.yaml";

        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly IInitializrConfigService _configService;

        private readonly List<(ProjectSpecConstraints Constraints, ProjectTemplate Template)> _templates =
            new List<(ProjectSpecConstraints Constraints, ProjectTemplate Template)>();

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplateRegistry"/> class.
        /// </summary>
        /// <param name="configService">Injected Initializr configuration service.</param>
        /// <param name="logger">Injected logger.</param>
        public ProjectTemplateRegistry(
            IInitializrConfigService configService,
            ILogger<ProjectTemplateRegistry> logger)
            : base(logger)
        {
            _configService = configService;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc />
        public void Initialize()
        {
            Logger.LogInformation("Initializing project template registry.");
            _templates.Clear();
            var config = _configService.GetInitializrConfig();
            if (config.ProjectTemplates is null)
            {
                Logger.LogError("Templates not configured.");
                return;
            }

            foreach (var templateConfig in config.ProjectTemplates)
            {
                Register(templateConfig.Uri);
            }
        }

        /// <inheritdoc/>
        public void Register(ProjectTemplate projectTemplate)
        {
            Logger.LogInformation(
                $"Registering project template: {projectTemplate.Constraints} ({projectTemplate.Description})");
            _templates.Add((projectTemplate.Constraints, projectTemplate));
        }

        /// <inheritdoc/>
        public ProjectTemplate Lookup(ProjectSpec spec)
        {
            try
            {
                foreach (var template in _templates)
                {
                    var constraints = template.Constraints;
                    if ((constraints.SteeltoeVersionRange is null
                         || constraints.SteeltoeVersionRange.Accepts(spec.SteeltoeVersion))
                        && (constraints.DotNetFrameworkRange is null
                            || constraints.DotNetFrameworkRange.Accepts(spec.DotNetFramework))
                        && (constraints.DotNetTemplate is null
                            || constraints.DotNetTemplate.Equals(spec.DotNetTemplate))
                        && (constraints.Language is null
                            || constraints.Language.Equals(spec.Language)))
                    {
                        return template.Template;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Logger.LogError("Error looking up project template: {Error}", e.Message);
            }

            return null;
        }

        private void Register(Uri uri)
        {
            Logger.LogInformation("Fetching project template: {Uri}", uri);
            try
            {
                var request = WebRequest.Create(uri);
                using var response = request.GetResponse();
                using var stream = response.GetResponseStream();
                if (stream is null)
                {
                    Logger.LogError("URI returned null stream: {TemplateUri}", uri);
                    return;
                }

                using var archive = new ZipArchive(stream);
                ProjectTemplate projectTemplate;
                try
                {
                    projectTemplate = ParseZipEntry<ProjectTemplate>(archive.GetEntry(Metadata));
                    if (projectTemplate is null)
                    {
                        Logger.LogError($"Project template metadata missing ('{Metadata}'): {{TemplateUri}}", uri);
                        return;
                    }

                    if (projectTemplate.Constraints is null)
                    {
                        Logger.LogError("Project template constraints missing: {TemplateUri}", uri);
                        return;
                    }

                    if (projectTemplate.Manifest is null)
                    {
                        Logger.LogError("Project template manifest missing: {TemplateUri}", uri);
                        return;
                    }
                }
                catch (YamlException e)
                {
                    Logger.LogError(e, "Project template metadata malformed ('{Path}'): {TemplateUri}", Metadata, uri);
                    return;
                }

                foreach (var fileEntry in projectTemplate.Manifest)
                {
                    var path = fileEntry.Path;
                    var archiveEntry = archive.GetEntry(path);

                    if (archiveEntry is null)
                    {
                        Logger.LogError("Project template missing file: '{File}' {TemplateUri}", path, uri);
                        return;
                    }

                    if (path.EndsWith('/'))
                    {
                        continue;
                    }

                    using var reader = new StreamReader(archiveEntry.Open());
                    fileEntry.Text = reader.ReadToEnd();
                }

                Register(projectTemplate);
            }
            catch (InvalidDataException)
            {
                Logger.LogError("URI not a Zip archive: {TemplateUri}", uri);
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                if (e is DirectoryNotFoundException || e is FileNotFoundException)
                {
                    Logger.LogError("URI not found: {TemplateUri}", uri);
                }
                else
                {
                    Logger.LogError(
                        e,
                        "Unexpected error [{Exception}[{Error}]]: {TemplateUri}",
                        e.GetType(),
                        e.Message,
                        uri);
                }
            }
        }

        private T ParseZipEntry<T>(ZipArchiveEntry entry)
        {
            if (entry is null)
            {
                return default(T);
            }

            using var reader = new StreamReader(entry.Open());
            var text = reader.ReadToEnd();
            Logger.LogDebug("{Path}:\n{Text}", entry.FullName, text);
            return Serializer.DeserializeYaml<T>(text);
        }
    }
}
