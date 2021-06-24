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
    public class UiConfigControllerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void GetUiConfig_Should_Return_UiConfig()
        {
            // Arrange
            var uiConfig = new UiConfig();
            var configService = new Mock<IUiConfigService>();
            configService.Setup(svc => svc.UiConfig).Returns(uiConfig);
            var controller = new UiConfigController(configService.Object, new NullLogger<UiConfigController>());

            // Act
            var result = controller.GetUiConfig();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeSameAs(uiConfig);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
