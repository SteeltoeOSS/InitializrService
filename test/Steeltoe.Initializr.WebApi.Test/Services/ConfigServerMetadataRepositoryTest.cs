using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Steeltoe.Initializr.WebApi.Services;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Services
{
	public class ConfigServerMetadataRepositoryTest
	{
		[Fact]
		public async Task ConfigurationShouldNotBeNull()
		{
			// Arrange
			var expectedConfig = new Configuration
				{Releases = new Configuration.SingleSelectList {Default = "a default", Type = "a type"}};
			var mockConfigOptions = new Mock<IOptions<Configuration>>();
			mockConfigOptions.Setup(opts => opts.Value).Returns(expectedConfig);
			var mockSettings = new ConfigServerClientSettingsOptions {Uri = "a uri"};
			var mockSettingsOptions = new Mock<IOptions<ConfigServerClientSettingsOptions>>();
			mockSettingsOptions.Setup(settings => settings.Value).Returns(mockSettings);
			var repo = new ConfigServerMetadataRepository(mockConfigOptions.Object, mockSettingsOptions.Object,
				new NullLogger<ConfigServerMetadataRepository>());

			// Act
			var actualConfig = await repo.GetConfiguration();

			// Assert
			Assert.IsType<Configuration>(actualConfig);
			actualConfig.Should().Be(expectedConfig);
		}
	}
}
