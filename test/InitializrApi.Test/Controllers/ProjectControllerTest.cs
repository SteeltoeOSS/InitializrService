// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Steeltoe.InitializrApi.Test.TestUtils;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Controllers
{
    public class ProjectControllerTest
    {
        private const string Endpoint = "/api/project";

        [Fact]
        public async Task GetReturnsProjectArchive()
        {
            // Arrange
            var controller = new ProjectControllerBuilder().Build();

            // Act
            var unknown = await controller.Get(new ProjectSpecification());

            // Assert
            var result = Assert.IsType<FileContentResult>(unknown);
            result.ContentType.Should().Be("application/zip");
            result.FileContents.Should().NotBeNull();
        }

        [Fact]
        public async Task PostReturnsMethodNotAllowed()
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
        public async Task PutReturnsMethodNotAllowed()
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
        public async Task PatchReturnsMethodNotAllowed()
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
        public async Task DeleteReturnsMethodNotAllowed()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();

            // Act
            var response = await client.DeleteAsync(Endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        class ProjectControllerBuilder
        {
            private readonly ProjectController _projectController;

            internal ProjectControllerBuilder()
            {
                var mockGenerator = new Mock<IProjectGenerator>();
                mockGenerator.Setup(g => g.GenerateProject(It.IsAny<ProjectSpecification>()))
                    .ReturnsAsync(new MemoryStream());
                _projectController = new ProjectController(mockGenerator.Object);
            }

            internal ProjectController Build()
            {
                _projectController.ControllerContext.HttpContext = new DefaultHttpContext();
                return _projectController;
            }
        }
    }
}
