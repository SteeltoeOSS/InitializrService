// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Steeltoe.InitializrApi.Configuration;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Configuration
{
    public class InitializrConfigFileTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Non_Existent_Config_File_Should_Throw_ArgumentException()
        {
            // Arrange
            var options = new Mock<IOptions<InitializrOptions>>();
            options.Setup(opts => opts.Value).Returns(new InitializrOptions
                { Configuration = new Dictionary<string, string> { { "Path", "no_such_path" } } });
            var logger = new NullLogger<InitializrConfigFile>();
            var config = new InitializrConfigFile(options.Object, logger);

            // Act
            Action act = () => config.Initialize();

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Configuration file path does not exist: no_such_path");
        }

        [Fact]
        public void Directory_As_Config_File_Should_Throw_ArgumentException()
        {
            // Arrange
            var options = new Mock<IOptions<InitializrOptions>>();
            options.Setup(opts => opts.Value).Returns(new InitializrOptions
                { Configuration = new Dictionary<string, string> { { "Path", "." } } });
            var logger = new NullLogger<InitializrConfigFile>();
            var config = new InitializrConfigFile(options.Object, logger);

            // Act
            Action act = () => config.Initialize();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Configuration file path is not a file or cannot be read: .");
        }

        [Fact]
        public void Non_Json_Config_File_Should_Throw_ArgumentException()
        {
            // Arrange
            var options = new Mock<IOptions<InitializrOptions>>();
            options.Setup(opts => opts.Value).Returns(new InitializrOptions
                { Configuration = new Dictionary<string, string> { { "Path", "Steeltoe.InitializrApi.dll" } } });
            var logger = new NullLogger<InitializrConfigFile>();
            var config = new InitializrConfigFile(options.Object, logger);

            // Act
            Action act = () => config.Initialize();

            // Assert
            act.Should().Throw<System.Text.Json.JsonException>();
        }
    }
}
