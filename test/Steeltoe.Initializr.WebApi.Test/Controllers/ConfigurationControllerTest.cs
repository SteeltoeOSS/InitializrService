using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.Initializr.WebApi.Controllers;
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Steeltoe.Initializr.WebApi.Services;
using Steeltoe.Initializr.WebApi.Test.Utils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Controllers
{
	public class ConfigurationControllerTest
	{
		[Fact]
		public async Task EndpointExists()
		{
			var client = new HttpClientBuilder().Build();
			var response = await client.GetAsync("/api/configuration");
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

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
