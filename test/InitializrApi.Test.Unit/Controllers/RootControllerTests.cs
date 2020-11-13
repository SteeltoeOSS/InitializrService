// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Controllers
{
    public class RootControllerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Get_Should_Return_Help()
        {
            // Arrange
            var initializrCfg = new Mock<IOptions<InitializrOptions>>();
            var initializrOpts = new InitializrOptions { Logo = "/no/such/logo" };
            initializrCfg.Setup(cfg => cfg.Value).Returns(initializrOpts);
            var config = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata
                {
                    Name = new ProjectMetadata.Text
                    {
                        Default = "MyProject"
                    },
                    ApplicationName = new ProjectMetadata.Text
                    {
                        Default = "MyApplication"
                    },
                    Namespace = new ProjectMetadata.Text
                    {
                        Default = "MyNamespace"
                    },
                    Description = new ProjectMetadata.Text
                    {
                        Default = "my description"
                    },
                    SteeltoeVersion = new ProjectMetadata.SingleSelectList
                    {
                        Default = "1.2.3"
                    },
                    DotNetFramework = new ProjectMetadata.SingleSelectList
                    {
                        Default = "1.2"
                    },
                    DotNetTemplate = new ProjectMetadata.SingleSelectList
                    {
                        Default = "mytemplate"
                    },
                    Language = new ProjectMetadata.SingleSelectList
                    {
                        Default = "mylang"
                    },
                    Packaging = new ProjectMetadata.SingleSelectList
                    {
                        Default = "mypkg"
                    },
                    Dependencies = new ProjectMetadata.GroupList
                    {
                        Values = new[]
                        {
                            new ProjectMetadata.Group()
                            {
                                Name = "AGroup",
                                Values = new[]
                                {
                                    new ProjectMetadata.GroupItem()
                                    {
                                        Id = "dep1",
                                        Description = "DependencyOne"
                                    },
                                    new ProjectMetadata.GroupItem()
                                    {
                                        Id = "dep2",
                                        Description = "DependencyTwo"
                                    }
                                }
                            },
                            new ProjectMetadata.Group()
                            {
                                Name = "AnotherGroup",
                                Values = new[]
                                {
                                    new ProjectMetadata.GroupItem
                                    {
                                        Id = "anotherdep",
                                        Description = "AnotherDependency",
                                        SteeltoeVersionRange = "[1.0,1.9)",
                                    }
                                }
                            }
                        }
                    }
                }
            };
            var configService = new Mock<IInitializrConfigService>();
            configService.Setup(repo => repo.GetInitializrConfig()).Returns(config);
            var controller = new RootController(initializrCfg.Object, configService.Object, new NullLogger<RootController>());

            // Act
            var result = controller.GetHelp();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeOfType<string>();
            var help = Assert.IsType<string>(indexResult.Value);
            help.Should().MatchRegex("!!! failed to load logo:");
            help.Should().MatchRegex(":: Steeltoe Initializr ::  https://start.steeltoe.io");
            help.Should().MatchRegex("Examples:");
            help.Should().MatchRegex(@"\|\s+Parameter\s+\|\s+Description\s+\|\s+Default value\s+\|");
            help.Should().MatchRegex(@"\|\s+name\s+\|\s+project name\s+\|\s+MyProject\s+\|");
            help.Should().MatchRegex(@"\|\s+applicationName\s+\|\s+application name\s+\|\s+MyApplication\s+\|");
            help.Should().MatchRegex(@"\|\s+namespace\s+\|\s+namespace\s+\|\s+MyNamespace\s+\|");
            help.Should().MatchRegex(@"\|\s+description\s+\|\s+project description\s+\|\s+my description\s+\|");
            help.Should().MatchRegex(@"\|\s+steeltoeVersion\s+\|\s+Steeltoe version\s+\|\s+1.2.3\s+\|");
            help.Should().MatchRegex(@"\|\s+dotNetFramework\s+\|\s+target .NET framework\s+\|\s+1.2\s+\|");
            help.Should().MatchRegex(@"\|\s+dotNetTemplate\s+\|\s+.NET template\s+\|\s+mytemplate\s+\|");
            help.Should().MatchRegex(@"\|\s+language\s+\|\s+programming language\s+\|\s+mylang\s+\|");
            help.Should().MatchRegex(@"\|\s+packaging\s+\|\s+project packaging\s+\|\s+mypkg\s+\|");
            help.Should().MatchRegex(@"\|\s+Id\s+\|\s+Description\s+\|\s+Required Steeltoe version\s+\|");
            help.Should().MatchRegex(@"\|\s+dep1\s+\|\s+DependencyOne\s+\|\s+\|");
            help.Should().MatchRegex(@"\|\s+dep2\s+\|\s+DependencyTwo\s+\|\s+\|");
            help.Should().MatchRegex(@"\|\s+anotherdep\s+\|\s+AnotherDependency\s+\|\s+>=1.0 and <1.9\s+\|");
            help.Should().MatchRegex(@"\|\s+anotherdep\s+\|\s+AnotherDependency\s+\|\s+>=1.0 and <1.9\s+\|");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }

}
