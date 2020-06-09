using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Steeltoe.Initializr.WebApi.Server.Test.Utils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Server.Test.Controller
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
	}
}
