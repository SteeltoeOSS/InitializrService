// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Steeltoe.InitializrService.Config;
using Steeltoe.InitializrService.Controllers;
using Steeltoe.InitializrService.Models;
using Steeltoe.InitializrService.Services;
using Xunit;

namespace Steeltoe.InitializrService.Test.Unit.Controllers
{
    public class ProjectControllerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public async Task Configuration_Should_Specify_Defaults()
        {
            // Arrange
            var config = new UiConfig
            {
                Name = new UiConfig.Text { Default = "my project name" },
                Description = new UiConfig.Text { Default = "my description" },
                Namespace = new UiConfig.Text { Default = "my namespace" },
                SteeltoeVersion = new UiConfig.SingleSelectList { Default = "3.0.0" },
                DotNetFramework = new UiConfig.SingleSelectList { Default = "netcoreapp3.1" },
                Language = new UiConfig.SingleSelectList { Default = "my language" },
                Packaging = new UiConfig.SingleSelectList { Default = "myarchive" },
            };
            var controller = new ProjectControllerBuilder()
                .WithInitializrConfiguration(config)
                .Build();

            // Act
            var unknown = await controller.GetProjectArchive(new ProjectSpec());
            var result = Assert.IsType<FileContentResult>(unknown);
            var projectPackage = Encoding.ASCII.GetString(result.FileContents);

            // Assert
            projectPackage.Should().Contain("project name=my project name");
            projectPackage.Should().Contain("namespace=my namespace");
            projectPackage.Should().Contain("description=my description");
            projectPackage.Should().Contain("steeltoe version=3.0.0");
            projectPackage.Should().Contain("dotnet framework=netcoreapp3.1");
            projectPackage.Should().Contain("language=my language");
            projectPackage.Should().Contain("packaging=myarchive");
            projectPackage.Should().Contain("dependencies=<na>");
        }

        [Fact]
        public async Task Dependencies_Should_Be_Case_Corrected()
        {
            // Arrange
            var config = new UiConfig
            {
                Dependencies = new UiConfig.GroupList
                {
                    Values = new[]
                    {
                        new UiConfig.Group
                        {
                            Values = new[]
                            {
                                new UiConfig.GroupItem
                                {
                                    Id = "CamelCaseDep",
                                },
                            },
                        },
                    },
                },
            };
            var spec = new ProjectSpec
            {
                SteeltoeVersion = "0.0",
                DotNetFramework = "0.0",
                Packaging = "myarchive",
                Dependencies = "camelcasedep",
            };
            var controller = new ProjectControllerBuilder()
                .WithInitializrConfiguration(config)
                .Build();

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<FileContentResult>(unknown);
            var projectPackage = Encoding.ASCII.GetString(result.FileContents);

            // Assert
            projectPackage.Should().Contain("dependencies=CamelCaseDep");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public async Task No_Template_Found_Should_Return_404_Page_Not_Found()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();
            var spec = new ProjectSpec
            {
                SteeltoeVersion = "0.0",
                DotNetFramework = "0.0",
                Name = "nosuchproject",
            };

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<NotFoundObjectResult>(unknown);

            // Assert
            result.Value.ToString().Should().Be("No project for spec.");
        }

        [Fact]
        public async Task Unknown_Packaging_Should_Return_400_BadRequest()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();
            var spec = new ProjectSpec
            {
                SteeltoeVersion = "0.0",
                DotNetFramework = "0.0",
                Packaging = "nosuchpackaging",
            };

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<BadRequestObjectResult>(unknown);

            // Assert
            result.Value.ToString().Should().Be("Packaging 'nosuchpackaging' not found.");
        }

        [Fact]
        public async Task Unknown_Dependency_Should_Return_404_Page_Not_found()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();
            var spec = new ProjectSpec
            {
                SteeltoeVersion = "0.0",
                DotNetFramework = "0.0",
                Dependencies = "nosuchdep",
            };

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<NotFoundObjectResult>(unknown);

            // Assert
            result.Value.ToString().Should().Be("Dependency 'nosuchdep' not found.");
        }

        [Fact]
        public async Task Null_Archive_Format_Should_Return_500_Internal_Server_Error()
        {
            // Arrange
            var spec = new ProjectSpec
            {
                SteeltoeVersion = "0.0",
                DotNetFramework = "0.0",
            };
            var config = new UiConfig();
            var controller = new ProjectControllerBuilder().WithInitializrConfiguration(config).Build();

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<ObjectResult>(unknown);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            result.Value.Should().Be("Default packaging not configured.");
        }

        [Fact]
        public async Task Unsupported_Steeltoe_Version_Should_Return_404_NotFound()
        {
            // Arrange
            var config = new UiConfig
            {
                Name = new UiConfig.Text { Default = "my project name" },
                Description = new UiConfig.Text { Default = "my description" },
                Namespace = new UiConfig.Text { Default = "my namespace" },
                SteeltoeVersion = new UiConfig.SingleSelectList { Default = "0.0" },
                DotNetFramework = new UiConfig.SingleSelectList { Default = "0.0" },
                Dependencies = new UiConfig.GroupList
                {
                    Values = new[]
                    {
                        new UiConfig.Group
                        {
                            Values = new[]
                            {
                                new UiConfig.GroupItem
                                {
                                    Id = "MyDep",
                                    SteeltoeVersionRange = "1.0"
                                },
                            },
                        },
                    },
                },
            };
            var controller = new ProjectControllerBuilder()
                .WithInitializrConfiguration(config)
                .Build();
            var spec = new ProjectSpec
            {
                Dependencies = "mydep",
            };

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<NotFoundObjectResult>(unknown);

            // Assert
            result.Value.ToString().Should().Be("No dependency 'mydep' found for Steeltoe version 0.0.");
        }

        [Fact]
        public async Task Unsupported_DotNet_Framework_Should_Return_404_NotFound()
        {
            // Arrange
            var config = new UiConfig
            {
                Name = new UiConfig.Text { Default = "my project name" },
                Description = new UiConfig.Text { Default = "my description" },
                Namespace = new UiConfig.Text { Default = "my namespace" },
                SteeltoeVersion = new UiConfig.SingleSelectList { Default = "0.0" },
                DotNetFramework = new UiConfig.SingleSelectList { Default = "0.0" },
                Dependencies = new UiConfig.GroupList
                {
                    Values = new[]
                    {
                        new UiConfig.Group
                        {
                            Values = new[]
                            {
                                new UiConfig.GroupItem
                                {
                                    Id = "MyDep",
                                    DotNetFrameworkRange = "1.0"
                                },
                            },
                        },
                    },
                },
            };
            var controller = new ProjectControllerBuilder()
                .WithInitializrConfiguration(config)
                .Build();
            var spec = new ProjectSpec
            {
                Dependencies = "mydep",
            };

            // Act
            var unknown = await controller.GetProjectArchive(spec);
            var result = Assert.IsType<NotFoundObjectResult>(unknown);

            // Assert
            result.Value.ToString().Should().Be("No dependency 'mydep' found for .NET framework 0.0.");
        }


        /* ----------------------------------------------------------------- *
         * test helpers                                                      *
         * ----------------------------------------------------------------- */

        class ProjectControllerBuilder
        {
            private UiConfig _uiConfig;

            private IProjectGenerator _generator;

            internal ProjectController Build()
            {
                _uiConfig ??= new UiConfig
                {
                    Packaging = new UiConfig.SingleSelectList
                    {
                        Default = "myarchive",
                    },
                };

                _generator ??= new TestProjectGenerator();

                var configurationService = new Mock<IUiConfigService>();
                configurationService.Setup(svc => svc.UiConfig).Returns(_uiConfig);
                var logger = new NullLogger<ProjectController>();
                var projectController = new ProjectController(configurationService.Object, _generator, logger)
                {
                    ControllerContext =
                    {
                        HttpContext = new DefaultHttpContext()
                    }
                };
                return projectController;
            }

            internal ProjectControllerBuilder WithInitializrConfiguration(UiConfig uiConfig)
            {
                _uiConfig = uiConfig;
                return this;
            }
        }

        private class TestProjectGenerator : IProjectGenerator
        {
            public Task<byte[]> GenerateProjectArchive(ProjectSpec spec)
            {
                if (spec.Name is "nosuchproject")
                {
                    throw new NoProjectForSpecException($"No project for spec.");
                }

                if (spec.Packaging is "nosuchpackaging")
                {
                    throw new InvalidSpecException($"Packaging '{spec.Packaging}' not found.");
                }

                const char newline = '\n';
                var buffer = new StringBuilder();
                buffer.Append("project name=").Append(spec.Name ?? "<na>");
                buffer.Append(newline);
                buffer.Append("description=").Append(spec.Description ?? "<na>");
                buffer.Append(newline);
                buffer.Append("namespace=").Append(spec.Namespace ?? "<na>");
                buffer.Append(newline);
                buffer.Append("steeltoe version=").Append(spec.SteeltoeVersion ?? "<na>");
                buffer.Append(newline);
                buffer.Append("dotnet framework=").Append(spec.DotNetFramework ?? "<na>");
                buffer.Append(newline);
                buffer.Append("language=").Append(spec.Language ?? "<na>");
                buffer.Append(newline);
                buffer.Append("packaging=").Append(spec.Packaging ?? "<na>");
                buffer.Append(newline);
                buffer.Append("dependencies=").Append(spec.Dependencies ?? "<na>");
                buffer.Append(newline);
                return Task.FromResult(Encoding.ASCII.GetBytes(buffer.ToString()));
            }
        }
    }
}
