// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Controllers
{
    public class ConfigControllerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void GetInitializrApiConfiguration_Should_Return_InitializrApiConfiguration()
        {
            // Arrange
            var config = new InitializrConfig();
            var configService = new Mock<IInitializrConfigService>();
            configService.Setup(repo => repo.GetInitializrConfig()).Returns(config);
            var controller = new ConfigController(configService.Object, new NullLogger<ConfigController>());

            // Act
            var result = controller.GetInitializrConfiguration();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeSameAs(config);
        }

        [Fact]
        public void GetProjectMetadata_Should_Return_ProjectMetadata()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata(),
            };
            var configService = new Mock<IInitializrConfigService>();
            configService.Setup(repo => repo.GetInitializrConfig()).Returns(config);
            var controller = new ConfigController(configService.Object, new NullLogger<ConfigController>());

            // Act
            var result = controller.GetProjectMetadata();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeSameAs(config.ProjectMetadata);
        }

        [Fact]
        public void GetProjectTemplateConfigurations_Should_Return_ProjectTemplateConfigurations()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new ProjectTemplateConfiguration[0],
            };
            var configService = new Mock<IInitializrConfigService>();
            configService.Setup(repo => repo.GetInitializrConfig()).Returns(config);
            var controller = new ConfigController(configService.Object, new NullLogger<ConfigController>());

            // Act
            var result = controller.GetProjectTemplatesConfig();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeSameAs(config.ProjectTemplates);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
