// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
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
            var cfg = new InitializrConfig
            {
                ProjectTemplates = new ProjectTemplateConfiguration[0],
            };
            var configOptions = new Mock<IOptions<InitializrConfig>>();
            configOptions.Setup(opts => opts.Value).Returns(cfg);
            var logger = new Mock<ILogger<InitializrConfigService>>();
            var service = new InitializrConfigService(configOptions.Object, logger.Object);

            // Act
            service.Initialize();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project metadata missing.");
        }

        [Fact]
        public void Null_Project_Templates_Configuration_Should_Log_An_Error()
        {
            // Arrange
            var cfg = new InitializrConfig
            {
                ProjectMetadata = new ProjectMetadata(),
            };
            cfg.ProjectMetadata = new ProjectMetadata();
            var configOptions = new Mock<IOptions<InitializrConfig>>();
            configOptions.Setup(opts => opts.Value).Returns(cfg);
            var logger = new Mock<ILogger<InitializrConfigService>>();
            var service = new InitializrConfigService(configOptions.Object, logger.Object);

            // Act
            service.Initialize();

            // Assert
            logger.VerifyLog(LogLevel.Error, "Project templates configuration missing.");
        }
    }
}
