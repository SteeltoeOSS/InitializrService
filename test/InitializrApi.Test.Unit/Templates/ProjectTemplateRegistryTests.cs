// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Steeltoe.InitializrApi.Templates;
using Steeltoe.InitializrApi.Test.Utils;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Templates
{
    public class ProjectTemplateRegistryTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Files_In_Project_Template_Should_Match_Manifest()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr"),
                    },
                },
            };
            var registry = new TemplateRegistryBuilder().WithConfig(config).Build();

            // Act
            var template = registry.Lookup(new ProjectSpec());

            // Assert
            Assert.NotNull(template);
            var fileEntries = template.Manifest.ToList();
            fileEntries.Count().Should().Be(3);
            fileEntries[0].Path.Should().Be("f1");
            fileEntries[0].Text.Should().Be("my file f1");
            fileEntries[0].Rename.Should().BeNull();
            fileEntries[1].Path.Should().Be("d1/");
            fileEntries[1].Text.Should().BeNull();
            fileEntries[1].Rename.Should().BeNull();
            fileEntries[2].Path.Should().Be("r1");
            fileEntries[2].Text.Should().Be("my file r1->n1");
            fileEntries[2].Rename.Should().Be("n1");
        }

        [Fact]
        public void Lookup_Should_Find_Match()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri(
                            "pt:///izr?description=pt1&steeltoeVersionRange=st2.0&dotNetFrameworkRange=df1.0&dotNetTemplate=dt1&language=l1"),
                    },
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri(
                            "pt:///izr?description=pt2&steeltoeVersionRange=st1.0&dotNetFrameworkRange=df1.0&dotNetTemplate=dt1&language=l1"),
                    },
                },
            };
            var registry = new TemplateRegistryBuilder().WithConfig(config).Build();

            // Act
            var template = registry.Lookup(new ProjectSpec
                { SteeltoeVersion = "st2.0", DotNetFramework = "df1.0", DotNetTemplate = "dt1", Language = "l1" });

            // Assert
            Assert.NotNull(template);
            template.Description.Should().Be("pt1");

            // Act
            template = registry.Lookup(new ProjectSpec
                { SteeltoeVersion = "st1.0", DotNetFramework = "df1.0", DotNetTemplate = "dt1", Language = "l1" });

            // Assert
            Assert.NotNull(template);
            template.Description.Should().Be("pt2");
        }

        [Fact]
        public void Initialize_Should_Reset_State()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr"),
                    },
                },
            };
            var registry = new TemplateRegistryBuilder().WithConfig(config).Build();

            // Act
            var template = registry.Lookup(new ProjectSpec());

            // Assert
            template.Should().NotBeNull();

            // Arrange
            config.ProjectTemplates = new ProjectTemplateConfiguration[0];

            // Act
            registry.Initialize();
            template = registry.Lookup(new ProjectSpec());

            // Assert
            template.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Project_Templates_Not_Configured_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig();
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Templates not configured.");
        }

        [Fact]
        public void Uri_File_Not_Found_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///no/such/file"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "URI not found: pt:///no/such/file");
        }

        [Fact]
        public void Uri_Directory_Not_Found_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///no/such/dir/"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "URI not found: pt:///no/such/dir/");
        }

        [Fact]
        public void Uri_Null_Stream_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///stream/null"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "URI returned null stream: pt:///stream/null");
        }

        [Fact]
        public void Uri_Non_Zip_Stream_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///stream/empty"),
                    }
                },
            };
            var configService = new Mock<IInitializrConfigService>();
            configService.Setup(svc => svc.GetInitializrConfig()).Returns(config);
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "URI not a Zip archive: pt:///stream/empty");
        }

        [Fact]
        public void Uri_Unexpected_Error_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///error"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Unexpected error [System.Exception[some error message]]: pt:///error");
        }

        [Fact]
        public void Missing_Metadata_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr?metadata=no"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error,
                "Project template metadata missing ('.IZR/metadata.yaml'): pt:///izr?metadata=no");
        }

        [Fact]
        public void Malformed_Metadata_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr?metadata=malformed"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error,
                "Project template metadata malformed ('.IZR/metadata.yaml'): pt:///izr?metadata=malformed");
        }

        [Fact]
        public void Missing_Constraints_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr?constraints=no"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project template constraints missing: pt:///izr?constraints=no");
        }

        [Fact]
        public void Missing_Manifest_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr?manifest=no"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project template manifest missing: pt:///izr?manifest=no");
        }

        [Fact]
        public void Missing_File_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr?missingfile=yes"),
                    }
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();

            // Act
            new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project template missing file: 'f1' pt:///izr?missingfile=yes");
        }

        [Fact]
        public void Invalid_Version_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("pt:///izr?steeltoeVersionRange=1.0"),
                    },
                },
            };
            var logger = new Mock<ILogger<ProjectTemplateRegistry>>();
            var registry = new TemplateRegistryBuilder().WithConfig(config).WithLogger(logger.Object).Build();
            var spec = new ProjectSpec { SteeltoeVersion = "1..1" };

            // Act
            var template = registry.Lookup(spec);

            // Assert
            template.Should().BeNull();
            logger.VerifyLog(LogLevel.Error,
                "Error looking up project template: Version not in correct format: '1..1'");
        }

        /* ----------------------------------------------------------------- *
         * test helpers                                                      *
         * ----------------------------------------------------------------- */

        static ProjectTemplateRegistryTests()
        {
            WebRequest.RegisterPrefix("pt", new MockProjectTemplateWebScheme());
        }

        class TemplateRegistryBuilder
        {
            private InitializrConfig _config = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata(),
                ProjectTemplates = new ProjectTemplateConfiguration[0],
            };

            private ILogger<ProjectTemplateRegistry> _logger = new NullLogger<ProjectTemplateRegistry>();

            internal ProjectTemplateRegistry Build()
            {
                var configService = new Mock<IInitializrConfigService>();
                configService.Setup(svc => svc.GetInitializrConfig()).Returns(_config);
                var registry = new ProjectTemplateRegistry(configService.Object, _logger);
                registry.Initialize();
                return registry;
            }

            internal TemplateRegistryBuilder WithConfig(InitializrConfig config)
            {
                _config = config;
                return this;
            }

            internal TemplateRegistryBuilder WithLogger(ILogger<ProjectTemplateRegistry> logger)
            {
                _logger = logger;
                return this;
            }
        }
    }
}
