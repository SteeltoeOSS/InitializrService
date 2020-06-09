using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Steeltoe.Initializr.WebApi.Server.Controllers;
using Steeltoe.Initializr.WebApi.Server.Data;
using Steeltoe.Initializr.WebApi.Server.Models.Metadata;
using Steeltoe.Initializr.WebApi.Server.Test.Utils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Server.Test.Controllers
{
	public class ConfigurationControllerTest
	{
		[Fact]
		public async Task EndpointExistsTest()
		{
			var client = new HttpClientBuilder().Build();
			var response = await client.GetAsync("/api/configuration");
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task TestIndexReturnConfiguration()
		{
			// Arrange
			var config = new Configuration();
			var mockRepo = new Mock<IConfigurationRepository>();
			mockRepo.Setup(repo => repo.GetConfiguration()).ReturnsAsync(config);
			var controller = new ConfigurationController(mockRepo.Object);

			// Act
			var result = await controller.Index();

			// Assert
			var indexResult = Assert.IsType<OkObjectResult>(result);
			var model = Assert.IsAssignableFrom<Configuration>(indexResult.Value);
		}
	}
}
