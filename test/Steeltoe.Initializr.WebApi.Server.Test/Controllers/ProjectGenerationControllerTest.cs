using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Steeltoe.Initializr.WebApi.Server.Test.Utils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Server.Test.Controller
{
	public class ProjectGenerationControllerTest
	{
		[Fact]
		public async Task EndpointExistsTest()
		{
			var client = new HttpClientBuilder().Build();
			var response = await client.GetAsync("/api/starter.zip");
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
	}
}
