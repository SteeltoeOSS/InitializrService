// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.Initializr.WebApi.Controllers;
using Steeltoe.Initializr.WebApi.Models;
using Steeltoe.Initializr.WebApi.Services;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Controllers
{
    public class ConfigurationControllerTest
    {
        [Fact]
        public async Task EndpointReturnsAConfiguration()
        {
            // Arrange
            var mockRepo = new Mock<IConfigurationRepository>();
            mockRepo.Setup(repo => repo.GetConfiguration()).ReturnsAsync(new Configuration());
            var controller = new ConfigurationController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var indexResult = Assert.IsType<OkObjectResult>(result);
            indexResult.Value.Should().BeOfType<Configuration>();
        }
    }
}
