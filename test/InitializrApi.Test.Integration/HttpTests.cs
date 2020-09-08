using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Integration
{
    public class HttpTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public async Task Get_Unknown_Path_Should_Return_404_Not_Found()
        {
            // Act
            var response = await HttpClient.GetAsync("no such path");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(AboutEndpoint)]
        [InlineData(ConfigurationEndpoint)]
        [InlineData(ProjectEndpoint)]
        public async Task Post_Should_Return_405_Method_Not_Allowed(string path)
        {
            // Act
            var response = await HttpClient.PostAsync(path, new StringContent(""));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Theory]
        [InlineData(AboutEndpoint)]
        [InlineData(ConfigurationEndpoint)]
        [InlineData(ProjectEndpoint)]
        public async Task Put_Should_Return_405_Method_Not_Allowed(string path)
        {
            // Act
            var response = await HttpClient.PutAsync(path, new StringContent(""));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Theory]
        [InlineData(AboutEndpoint)]
        [InlineData(ConfigurationEndpoint)]
        [InlineData(ProjectEndpoint)]
        public async Task Patch_Should_Return_405_Method_Not_Allowed(string path)
        {
            // Act
            var response = await HttpClient.PatchAsync(path, new StringContent(""));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Theory]
        [InlineData(AboutEndpoint)]
        [InlineData(ConfigurationEndpoint)]
        [InlineData(ProjectEndpoint)]
        public async Task Delete_Should_Return_405_Method_Not_Allowed(string path)
        {
            // Act
            var response = await HttpClient.DeleteAsync(path);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        /* ----------------------------------------------------------------- *
         * constants                                                         *
         * ----------------------------------------------------------------- */

        private const string AboutEndpoint = "/api/about";

        private const string ConfigurationEndpoint = "/api/config";

        private const string ProjectEndpoint = "/api/project";

        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private HttpClient HttpClient { get; }

        /* ----------------------------------------------------------------- *
         * constructors
         * ----------------------------------------------------------------- */

        public HttpTests(WebApplicationFactory<Startup> fixture)
        {
            HttpClient = fixture.CreateClient();
        }

    }
}
