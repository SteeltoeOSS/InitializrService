using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Integration
{
    public class HttpTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public async Task Get_UiConfig_Should_Return_UiConfig()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var config = await response.Content.ReadFromJsonAsync<UiConfig>();
            config.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_UiConfig_Steeltoe_Versions_Should_Return_Steeltoe_Versions()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint + "/steeltoeVersions");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var steeltoes = await response.Content.ReadFromJsonAsync<UiConfig.SelectItem[]>();
            steeltoes.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_DotNet_Frameworks_Should_Return_DotNet_Frameworks()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint + "/dotNetFrameworks");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var frameworks = await response.Content.ReadFromJsonAsync<UiConfig.SelectItem[]>();
            frameworks.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_DotNet_Templates_Should_Return_DotNet_Templates()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint + "/dotNetTemplates");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var templates = await response.Content.ReadFromJsonAsync<UiConfig.SelectItem[]>();
            templates.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_Languages_Should_Return_Languages()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint + "/languages");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var languages = await response.Content.ReadFromJsonAsync<UiConfig.SelectItem[]>();
            languages.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_Archive_Types_Should_Return_Archive_Types()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint + "/archiveTypes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var archivers = await response.Content.ReadFromJsonAsync<UiConfig.SelectItem[]>();
            archivers.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_Dependencies_Should_Return_Dependencies()
        {
            // Act
            var response = await HttpClient.GetAsync(UiConfigEndpoint + "/dependencies");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var deps = await response.Content.ReadFromJsonAsync<UiConfig.SelectItem[]>();
            deps.Length.Should().BeGreaterThan(0);
        }

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
        [InlineData(UiConfigEndpoint)]
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
        [InlineData(UiConfigEndpoint)]
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
        [InlineData(UiConfigEndpoint)]
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
        [InlineData(UiConfigEndpoint)]
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

        private const string UiConfigEndpoint = "/api/uiconfig";

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
            fixture.Services.GetRequiredService<IUiConfigService>().Initialize();
            HttpClient = fixture.CreateClient();
        }
    }
}
