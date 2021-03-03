// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Steeltoe.InitializrApi.Archivers;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Controllers
{
    public class ProjectControllerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Zip_Archive_Format_Should_Return_Zip_Archive()
        {
            // Arrange
            var archiverRegistry = new Mock<IArchiverRegistry>();
            archiverRegistry.Setup(reg => reg.Lookup(It.Is<string>(s => s.Equals("zip"))))
                .Returns(new ZipArchiver());
            var controller = new ProjectControllerBuilder().WithArchiverRegistry(archiverRegistry.Object).Build();
            var spec = new ProjectSpec { Packaging = "zip" };

            // Act
            var unknown = controller.GetProjectArchive(spec);

            // Assert
            var result = Assert.IsType<FileContentResult>(unknown);
            result.ContentType.Should().Be("application/zip");
            result.FileDownloadName.Should().EndWith(".zip");
            var stream = new MemoryStream(result.FileContents);
            new ZipArchive(stream).Should().BeOfType<ZipArchive>();
        }

        [Fact]
        public void Configuration_Should_Specify_Defaults()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata
                {
                    Name = new ProjectMetadata.Text { Default = "my project name" },
                    Description = new ProjectMetadata.Text { Default = "my description" },
                    Namespace = new ProjectMetadata.Text { Default = "my namespace" },
                    SteeltoeVersion = new ProjectMetadata.SingleSelectList { Default = "my steeltoe version" },
                    DotNetFramework = new ProjectMetadata.SingleSelectList { Default = "my dotnet framework" },
                    DotNetTemplate = new ProjectMetadata.SingleSelectList { Default = "my dotnet template" },
                    Language = new ProjectMetadata.SingleSelectList { Default = "my language" },
                    Packaging = new ProjectMetadata.SingleSelectList { Default = "myarchive" },
                },
            };
            var controller = new ProjectControllerBuilder()
                .WithInitializrConfiguration(config)
                .Build();

            // Act
            var unknown = controller.GetProjectArchive(new ProjectSpec());

            // Assert
            var result = Assert.IsType<FileContentResult>(unknown);
            using var reader = new StreamReader(new MemoryStream(result.FileContents));
            reader.ReadLine().Should().Be("project name=my project name");
            reader.ReadLine().Should().Be("description=my description");
            reader.ReadLine().Should().Be("namespace=my namespace");
            reader.ReadLine().Should().Be("steeltoe version=my steeltoe version");
            reader.ReadLine().Should().Be("dotnet framework=my dotnet framework");
            reader.ReadLine().Should().Be("dotnet template=my dotnet template");
            reader.ReadLine().Should().Be("language=my language");
            reader.ReadLine().Should().Be("packaging=myarchive");
            reader.ReadLine().Should().Be("dependencies=<na>");
            reader.ReadLine().Should().BeNull();
        }

        [Fact]
        public void Dependencies_Should_Be_Case_Corrected()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata
                {
                    Dependencies = new ProjectMetadata.GroupList
                    {
                        Values = new[]
                        {
                            new ProjectMetadata.Group
                            {
                                Values = new[]
                                {
                                    new ProjectMetadata.GroupItem
                                    {
                                        Id = "CamelCaseDep",
                                    },
                                },
                            },
                        },
                    },
                },
            };
            var spec = new ProjectSpec
            {
                Packaging = "myarchive",
                Dependencies = "camelcasedep",
            };
            var controller = new ProjectControllerBuilder()
                .WithInitializrConfiguration(config)
                .Build();

            // Act
            var unknown = controller.GetProjectArchive(spec);

            // Assert
            var result = Assert.IsType<FileContentResult>(unknown);
            using var reader = new StreamReader(new MemoryStream(result.FileContents));
            var body = reader.ReadToEnd();
            body.Should().Contain("dependencies=CamelCaseDep");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void No_Template_Found_Should_Return_404_Page_Not_Found()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();
            var spec = new ProjectSpec { Name = "nosuchtemplate" };

            // Act
            var unknown = controller.GetProjectArchive(spec);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(unknown);
            result.Value.ToString().Should()
                .Be("No project template for spec: [name=nosuchtemplate,packaging=myarchive]");
        }

        [Fact]
        public void Unknown_Archive_Format_Should_Return_404_Page_Not_Found()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();
            var spec = new ProjectSpec { Packaging = "nosuchformat" };

            // Act
            var unknown = controller.GetProjectArchive(spec);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(unknown);
            result.Value.ToString().Should().Be("Packaging 'nosuchformat' not found.");
        }

        [Fact]
        public void Unknown_Dependency_Should_Return_404_Page_Not_found()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();
            var spec = new ProjectSpec { Dependencies = "nosuchdep" };

            // Act
            var unknown = controller.GetProjectArchive(spec);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(unknown);
            result.Value.ToString().Should().Be("Dependency 'nosuchdep' not found.");
        }

        [Fact]
        public void Null_Archive_Format_Should_Return_500_Internal_Server_Error()
        {
            // Arrange
            var spec = new ProjectSpec();
            var config = new InitializrConfig { ProjectTemplates = new ProjectTemplateConfiguration[0] };
            var controller = new ProjectControllerBuilder().WithInitializrConfiguration(config).Build();

            // Act
            var unknown = controller.GetProjectArchive(spec);

            // Assert
            var result = Assert.IsType<ObjectResult>(unknown);
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            result.Value.Should().Be("Default packaging not configured.");
        }

        /* ----------------------------------------------------------------- *
         * test helpers                                                      *
         * ----------------------------------------------------------------- */

        class ProjectControllerBuilder
        {
            private InitializrConfig _config;

            private IProjectGenerator _generator;

            private IArchiverRegistry _registry;

            internal ProjectController Build()
            {
                if (_config is null)
                {
                    _config = new InitializrConfig
                    {
                        ProjectMetadata = new ProjectMetadata
                        {
                            Packaging = new ProjectMetadata.SingleSelectList
                            {
                                Default = "myarchive",
                            },
                        },
                        ProjectTemplates = new ProjectTemplateConfiguration[0],
                    };
                }

                if (_generator is null)
                {
                    _generator = new TestProjectGenerator();
                }

                if (_registry is null)
                {
                    var mock = new Mock<IArchiverRegistry>();
                    mock.Setup(reg => reg.Lookup(It.Is<string>(s => s.Equals("myarchive"))))
                        .Returns(new TestArchiver());
                    _registry = mock.Object;
                }

                var configurationService = new Mock<IInitializrConfigService>();
                configurationService.Setup(svc => svc.GetInitializrConfig()).Returns(_config);
                var logger = new NullLogger<ProjectController>();
                var projectController =
                    new ProjectController(configurationService.Object, _generator, _registry, logger)
                    {
                        ControllerContext =
                        {
                            HttpContext = new DefaultHttpContext()
                        }
                    };
                return projectController;
            }

            internal ProjectControllerBuilder WithInitializrConfiguration(InitializrConfig config)
            {
                _config = config;
                return this;
            }

            internal ProjectControllerBuilder WithArchiverRegistry(IArchiverRegistry registry)
            {
                _registry = registry;
                return this;
            }
        }

        private class TestProjectGenerator : IProjectGenerator
        {
            public Project GenerateProject(ProjectSpec spec)
            {
                if (spec.Name != null && spec.Name.Equals("nosuchtemplate"))
                {
                    return null;
                }

                var project = new Project();
                project.FileEntries.Add(new FileEntry { Path = "project name", Text = spec.Name ?? "<na>" });
                project.FileEntries.Add(new FileEntry { Path = "description", Text = spec.Description ?? "<na>" });
                project.FileEntries.Add(new FileEntry { Path = "namespace", Text = spec.Namespace ?? "<na>" });
                project.FileEntries.Add(new FileEntry
                    { Path = "steeltoe version", Text = spec.SteeltoeVersion ?? "<na>" });
                project.FileEntries.Add(new FileEntry
                    { Path = "dotnet framework", Text = spec.DotNetFramework ?? "<na>" });
                project.FileEntries.Add(
                    new FileEntry { Path = "dotnet template", Text = spec.DotNetTemplate ?? "<na>" });
                project.FileEntries.Add(new FileEntry { Path = "language", Text = spec.Language ?? "<na>" });
                project.FileEntries.Add(new FileEntry
                    { Path = "packaging", Text = spec.Packaging ?? "<na>" });
                project.FileEntries.Add(new FileEntry { Path = "dependencies", Text = spec.Dependencies ?? "<na>" });
                return project;
            }
        }

        private class TestArchiver : IArchiver
        {
            public byte[] ToBytes(IEnumerable<FileEntry> fileEntries)
            {
                var buf = new StringBuilder();
                foreach (var fileEntry in fileEntries)
                {
                    buf.Append(fileEntry.Path)
                        .Append('=')
                        .Append(fileEntry.Text)
                        .Append(Environment.NewLine);
                }

                return Encoding.UTF8.GetBytes(buf.ToString());
            }

            public string GetPackaging()
            {
                return "application/myarchive";
            }

            public string GetFileExtension()
            {
                return ".myext";
            }
        }
    }
}
