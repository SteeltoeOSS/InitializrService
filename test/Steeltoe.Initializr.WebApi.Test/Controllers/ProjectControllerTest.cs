using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.Initializr.WebApi.Controllers;
using Steeltoe.Initializr.WebApi.Models.Project;
using Steeltoe.Initializr.WebApi.Services;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Controllers
{
    public class ProjectControllerTest
    {
        [Fact]
        public async Task EndpointReturnsBytes()
        {
            // Arrange
            var mockGenerator = new Mock<IProjectGenerator>();
            mockGenerator.Setup(g => g.GenerateProject(new Specification())).ReturnsAsync(new MemoryStream());
            var controller = new ProjectController(mockGenerator.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeOfType<byte[]>();
        }
    }
}
