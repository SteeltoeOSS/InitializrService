// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Controllers
{
    public class AboutControllerTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void GetAbout_Should_Return_About()
        {
            // Arrange
            var controller = new AboutController(new NullLogger<AboutController>());

            // Act
            var result = controller.GetAbout();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeOfType<About>();
            var about = Assert.IsType<About>(indexResult.Value);
            about.Name.Should().Be("Steeltoe.InitializrApi");
            about.Vendor.Should().Be("SteeltoeOSS/VMware");
            about.Url.Should().Be("https://github.com/SteeltoeOSS/InitializrApi/");
            about.Version.Should().StartWith("0.13.0");
            about.Commit.Should().NotBeNullOrWhiteSpace();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
