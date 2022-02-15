// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.InitializrService.Configuration;
using Xunit;

namespace Steeltoe.InitializrService.Test.Unit.Configuration
{
    public class InitializrServiceOptionsTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Non_Existent_UiConfig_Path_Should_Throw_ArgumentException()
        {
            // Arrange
            var options = new Mock<IOptions<InitializrServiceOptions>>();
            options.Setup(opts => opts.Value).Returns(new InitializrServiceOptions
                { UiConfig = new Dictionary<string, string> { { "Path", "no_such_path" } } });
            var logger = new NullLogger<UiConfigFile>();

            // Act
            Action act = () =>
            {
                var _ = new UiConfigFile(options.Object, logger);
            };

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("UI configuration file path does not exist: no_such_path");
        }

        [Fact]
        public void Directory_UiConfig_Path_Should_Throw_ArgumentException()
        {
            // Arrange
            var options = new Mock<IOptions<InitializrServiceOptions>>();
            options.Setup(opts => opts.Value).Returns(new InitializrServiceOptions
                { UiConfig = new Dictionary<string, string> { { "Path", "." } } });
            var logger = new NullLogger<UiConfigFile>();

            // Act
            Action act = () =>
            {
                var _ = new UiConfigFile(options.Object, logger);
            };

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("UI configuration file path is not a file or cannot be read: .");
        }

        [Fact]
        public void Non_Json_UiConfig_Should_Throw_ArgumentException()
        {
            // Arrange
            var options = new Mock<IOptions<InitializrServiceOptions>>();
            options.Setup(opts => opts.Value).Returns(new InitializrServiceOptions
                { UiConfig = new Dictionary<string, string> { { "Path", "Steeltoe.InitializrService.dll" } } });
            var logger = new NullLogger<UiConfigFile>();

            // Act
            Action act = () =>
            {
                var _ = new UiConfigFile(options.Object, logger);
            };

            // Assert
            act.Should().Throw<System.Text.Json.JsonException>();
        }
    }
}
