// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Steeltoe.InitializrApi.Test.TestUtils;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Controllers
{
    public class ConfigurationControllerTest
    {
        private const string Endpoint = "/api/configuration";

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

        [Fact]
        public async Task PostNotSupported()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();
            var content = new Mock<HttpContent>();

            // Act
            var response = await client.PostAsync(Endpoint, content.Object);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task PutNotSupported()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();
            var content = new Mock<HttpContent>();

            // Act
            var response = await client.PutAsync(Endpoint, content.Object);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task PatchNotSupported()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();
            var content = new Mock<HttpContent>();

            // Act
            var response = await client.PatchAsync(Endpoint, content.Object);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task DeleteNotSupported()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();

            // Act
            var response = await client.DeleteAsync(Endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }
    }
}
