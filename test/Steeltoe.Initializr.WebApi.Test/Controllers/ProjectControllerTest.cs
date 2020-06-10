using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.Initializr.WebApi.Controllers;
using Steeltoe.Initializr.WebApi.Models.Project;
using Steeltoe.Initializr.WebApi.Services;
using Steeltoe.Initializr.WebApi.Test.Utils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Controllers
{
	public class ProjectControllerTest
	{
		[Fact]
		public async Task EndpointExists()
		{
			// Arrange
			var client = new HttpClientBuilder().Build();

			// Act
			var response = await client.GetAsync("/api/project");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task EndpointReturnsBytes()
		{
			// Arrange
			var mockGenerator = new Mock<IProjectGenerator>();
			mockGenerator.Setup(g => g.GenerateProject(new Configuration())).ReturnsAsync(new MemoryStream());
			var controller = new ProjectController(mockGenerator.Object);

			// Act
			var result = await controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			okResult.Value.Should().BeOfType<byte[]>();
		}
	}
}
