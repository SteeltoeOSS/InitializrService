// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Configuration;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Configuration
{
    public class ConfigServerConfigurationRepositoryTest
    {
        [Fact]
        public async Task ConfigurationShouldNotBeNull()
        {
            // Arrange
            var expected = new InitializrApiConfiguration();
            var mockConfigOptions = new Mock<IOptions<InitializrApiConfiguration>>();
            mockConfigOptions.Setup(opts => opts.Value).Returns(expected);
            var mockSettings = new ConfigServerClientSettingsOptions {Uri = "a uri"};
            var mockSettingsOptions = new Mock<IOptions<ConfigServerClientSettingsOptions>>();
            mockSettingsOptions.Setup(settings => settings.Value).Returns(mockSettings);
            var repo = new ConfigServerConfigurationRepository(mockConfigOptions.Object);

            // Act
            var actual = await repo.GetConfiguration();

            // Assert
            Assert.IsType<InitializrApiConfiguration>(actual);
            actual.Should().Be(expected);
        }
    }
}
