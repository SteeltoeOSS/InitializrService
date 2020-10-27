// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Configuration;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Test.Utils;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Configuration
{
    public class InitializrConfigServiceTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Null_Project_Metadata_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectTemplates = new ProjectTemplateConfiguration[0],
            };
            var cfgServerConfig = new Mock<IOptions<InitializrConfig>>();
            cfgServerConfig.Setup(o => o.Value).Returns(config);
            var settings = new ConfigServerClientSettingsOptions();
            var cfgServerSettings = new Mock<IOptions<ConfigServerClientSettingsOptions>>();
            cfgServerSettings.Setup(o => o.Value).Returns(settings);
            var logger = new Mock<ILogger<InitializrConfigService>>();
            var service = new InitializrConfigService(cfgServerConfig.Object, cfgServerSettings.Object, logger.Object);

            // Act
            service.Initialize();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project metadata missing.");
        }

        [Fact]
        public void Null_Project_Templates_Configuration_Should_Log_An_Error()
        {
            // Arrange
            var config = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata(),
            };
            config.ProjectMetadata = new ProjectMetadata();
            var cfgServerConfig = new Mock<IOptions<InitializrConfig>>();
            cfgServerConfig.Setup(o => o.Value).Returns(config);
            var settings = new ConfigServerClientSettingsOptions();
            var cfgServerSettings = new Mock<IOptions<ConfigServerClientSettingsOptions>>();
            cfgServerSettings.Setup(o => o.Value).Returns(settings);
            var logger = new Mock<ILogger<InitializrConfigService>>();
            var service = new InitializrConfigService(cfgServerConfig.Object, cfgServerSettings.Object, logger.Object);

            // Act
            service.Initialize();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project templates configuration missing.");
        }
    }
}
