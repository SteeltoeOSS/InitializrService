// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
            var expected = new Configuration
                {Releases = new Configuration.SingleSelectList {Default = "a default", Type = "a type"}};
            var mockConfigOptions = new Mock<IOptions<Configuration>>();
            mockConfigOptions.Setup(opts => opts.Value).Returns(expected);
            var mockSettings = new ConfigServerClientSettingsOptions {Uri = "a uri"};
            var mockSettingsOptions = new Mock<IOptions<ConfigServerClientSettingsOptions>>();
            mockSettingsOptions.Setup(settings => settings.Value).Returns(mockSettings);
            var repo = new ConfigServerMetadataRepository(mockConfigOptions.Object, mockSettingsOptions.Object,
                new NullLogger<ConfigServerMetadataRepository>());

            // Act
            var actual = await repo.GetConfiguration();

            // Assert
            Assert.IsType<Configuration>(actual);
            actual.Should().Be(expected);
            actual.About.Should().NotBeNull();
            actual.About.Name.Should().NotBeNull();
            actual.About.Vendor.Should().NotBeNull();
            actual.About.Url.Should().NotBeNull();
            actual.About.Version.Should().NotBeNull();
            actual.About.Commit.Should().NotBeNull();
        }
    }
}
