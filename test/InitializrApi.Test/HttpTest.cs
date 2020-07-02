using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Steeltoe.Initializr.WebApi.Test.TestUtils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test
{
    public class HttpTest
    {
        [Fact]
        public async Task NotFound()
        {
            // Arrange
            var client = new HttpClientBuilder().Build();
            var content = new Mock<HttpContent>();

            // Act
            var response = await client.GetAsync("no such path");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
