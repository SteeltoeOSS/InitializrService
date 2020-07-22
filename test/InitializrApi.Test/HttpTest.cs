using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Steeltoe.InitializrApi.Test.TestUtils;
using Xunit;

namespace Steeltoe.InitializrApi.Test
{
    public class HttpTest
    {
        [Fact]
        public async Task NotFound()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();

            // Act
            var response = await client.GetAsync("no such path");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
