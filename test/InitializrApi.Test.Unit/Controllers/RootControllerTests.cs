// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.InitializrApi.Config;
using Steeltoe.InitializrApi.Controllers;
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
            var initializrCfg = new Mock<IOptions<InitializrApiOptions>>();
            var initializrOpts = new InitializrApiOptions { Logo = "/no/such/logo" };
            initializrCfg.Setup(cfg => cfg.Value).Returns(initializrOpts);
            var config = new UiConfig
            {
                Name = new UiConfig.Text
                {
                    Default = "MyProject"
                },
                Namespace = new UiConfig.Text
                {
                    Default = "MyNamespace"
                },
                Description = new UiConfig.Text
                {
                    Default = "my description"
                },
                SteeltoeVersion = new UiConfig.SingleSelectList
                {
                    Default = "1.2.3"
                },
                DotNetFramework = new UiConfig.SingleSelectList
                {
                    Default = "1.2"
                },
                Language = new UiConfig.SingleSelectList
                {
                    Default = "mylang"
                },
                Packaging = new UiConfig.SingleSelectList
                {
                    Default = "mypkg"
                },
                Dependencies = new UiConfig.GroupList
                {
                    Values = new[]
                    {
                        new UiConfig.Group()
                        {
                            Name = "AGroup",
                            Values = new[]
                            {
                                new UiConfig.GroupItem()
                                {
                                    Id = "dep1",
                                    Description = "DependencyOne"
                                },
                                new UiConfig.GroupItem()
                                {
                                    Id = "dep2",
                                    Description = "DependencyTwo"
                                }
                            }
                        },
                        new UiConfig.Group()
                        {
                            Name = "AnotherGroup",
                            Values = new[]
                            {
                                new UiConfig.GroupItem
                                {
                                    Id = "anotherdep",
                                    Description = "AnotherDependency",
                                    SteeltoeVersionRange = "[1.0,1.9)",
                                    DotNetFrameworkRange = "2.0",
                                }
                            }
                        }
                    }
                }
            };
            var configService = new Mock<IUiConfigService>();
            configService.Setup(svc => svc.UiConfig).Returns(config);
            var controller = new RootController(initializrCfg.Object, configService.Object,
                new NullLogger<RootController>());

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
            help.Should().MatchRegex(@"\|\s+namespace\s+\|\s+namespace\s+\|\s+MyNamespace\s+\|");
            help.Should().MatchRegex(@"\|\s+description\s+\|\s+project description\s+\|\s+my description\s+\|");
            help.Should().MatchRegex(@"\|\s+steeltoeVersion\s+\|\s+Steeltoe version\s+\|\s+1.2.3\s+\|");
            help.Should().MatchRegex(@"\|\s+dotNetFramework\s+\|\s+target .NET framework\s+\|\s+1.2\s+\|");
            help.Should().MatchRegex(@"\|\s+language\s+\|\s+programming language\s+\|\s+mylang\s+\|");
            help.Should().MatchRegex(@"\|\s+packaging\s+\|\s+project packaging\s+\|\s+mypkg\s+\|");
            help.Should().MatchRegex(@"\|\s+Id\s+\|\s+Description\s+\|\s+Steeltoe Version\s+\|\s+.NET Framework\s+\|");
            help.Should().MatchRegex(@"\|\s+dep1\s+\|\s+DependencyOne\s+\|\s+\|");
            help.Should().MatchRegex(@"\|\s+dep2\s+\|\s+DependencyTwo\s+\|\s+\|");
            help.Should().MatchRegex(@"\|\s+anotherdep\s+\|\s+AnotherDependency\s+\|\s+>=1.0 and <1.9\s+\|\s+>=2.0\s+\|");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
