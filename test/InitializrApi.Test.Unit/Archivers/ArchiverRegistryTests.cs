// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Steeltoe.InitializrApi.Archivers;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Archivers
{
    public class ArchiverRegistryTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Zip_Format_Should_Return_ZipArchiver()
        {
            // Arrange
            var registry = new ArchiverRegistry(new NullLogger<ArchiverRegistry>());
            registry.Initialize();

            // Act
            var archiver = registry.Lookup("zip");

            // Assert
            archiver.Should().NotBeNull();
            archiver.Should().BeOfType<ZipArchiver>();
        }

        [Fact]
        public void Initialize_Should_Reset_State()
        {
            // Arrange
            var registry = new ArchiverRegistry(new NullLogger<ArchiverRegistry>());
            registry.Initialize();
            var myArchiver = new Mock<IArchiver>();
            myArchiver.Setup(a => a.GetPackaging()).Returns("mypackaging");
            registry.Register(myArchiver.Object);

            // Act
            var archiver = registry.Lookup("mypackaging");

            // Assert
            archiver.Should().NotBeNull();

            // Act
            registry.Initialize();
            archiver = registry.Lookup("mytype");

            // Assert
            archiver.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Unknown_Format_Should_Return_Null()
        {
            // Arrange
            var registry = new ArchiverRegistry(new NullLogger<ArchiverRegistry>());
            registry.Initialize();

            // Act
            var archiver = registry.Lookup("unknown");

            // Assert
            archiver.Should().BeNull();
        }
    }
}
