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
        public async Task Get_Config_Should_Return_InitializrConfig()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var config = await response.Content.ReadFromJsonAsync<InitializrConfig>();
            config.ProjectMetadata.Should().NotBeNull();
            config.ProjectTemplates.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Config_ProjectMetadata_Should_Return_ProjectMetadata()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/projectMetadata");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var metadata = await response.Content.ReadFromJsonAsync<ProjectMetadata>();
            metadata.Name.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Config_Steeltoe_Versions_Should_Return_Steeltoe_Versions()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/steeltoeVersions");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var versions = await response.Content.ReadFromJsonAsync<ProjectMetadata.SelectItem[]>();
            versions.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_DotNet_Frameworks_Should_Return_DotNet_Frameworks()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/dotNetFrameworks");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var versions = await response.Content.ReadFromJsonAsync<ProjectMetadata.SelectItem[]>();
            versions.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_DotNet_Templates_Should_Return_DotNet_Templates()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/dotNetTemplates");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var versions = await response.Content.ReadFromJsonAsync<ProjectMetadata.SelectItem[]>();
            versions.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_Languages_Should_Return_Languages()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/languages");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var versions = await response.Content.ReadFromJsonAsync<ProjectMetadata.SelectItem[]>();
            versions.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_Archive_Types_Should_Return_Archive_Types()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/archiveTypes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var versions = await response.Content.ReadFromJsonAsync<ProjectMetadata.SelectItem[]>();
            versions.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_Dependencies_Should_Return_Dependencies()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/dependencies");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var versions = await response.Content.ReadFromJsonAsync<ProjectMetadata.SelectItem[]>();
            versions.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Config_ProjectTemplates_Should_Return_ProjectTemplates()
        {
            // Act
            var response = await HttpClient.GetAsync(ConfigurationEndpoint + "/projectTemplates");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var templates = await response.Content.ReadFromJsonAsync<ProjectTemplate[]>();
            templates.Length.Should().BeGreaterThan(0);
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
            fixture.Services.GetRequiredService<IInitializrConfigService>().Initialize();
            HttpClient = fixture.CreateClient();
        }
    }
}
