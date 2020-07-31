// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.ConfigServer;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.ConfigServer
{
    public class ConfigServerConfigurationRepositoryTest
    {
        [Fact]
        public async Task ConfigurationShouldNotBeNull()
        {
            // Arrange
            var about = new Mock<IAbout>();
            about.Setup(a => a.GetAbout()).Returns(new About());
            var expected = new Configuration
                {SteeltoeRelease = new Configuration.SingleSelectList {Default = "a default", Type = "a type"}};
            var mockConfigOptions = new Mock<IOptions<Configuration>>();
            mockConfigOptions.Setup(opts => opts.Value).Returns(expected);
            var mockSettings = new ConfigServerClientSettingsOptions {Uri = "a uri"};
            var mockSettingsOptions = new Mock<IOptions<ConfigServerClientSettingsOptions>>();
            mockSettingsOptions.Setup(settings => settings.Value).Returns(mockSettings);
            var repo = new ConfigServerConfigurationRepository(mockConfigOptions.Object, mockSettingsOptions.Object,
                about.Object,
                new NullLogger<ConfigServerConfigurationRepository>());

            // Act
            var actual = await repo.GetConfiguration();

            // Assert
            Assert.IsType<Configuration>(actual);
            actual.Should().Be(expected);
            actual.About.Should().NotBeNull();
        }
    }
}
