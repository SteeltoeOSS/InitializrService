// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Models
{
    public class ProjectMetadataTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var metadata = new ProjectMetadata();

            // Act

            // Assert
            metadata.Project.Should().BeNull();
            metadata.Namespace.Should().BeNull();
            metadata.Application.Should().BeNull();
            metadata.Description.Should().BeNull();
            metadata.SteeltoeVersion.Should().BeNull();
            metadata.DotNetFramework.Should().BeNull();
            metadata.DotNetTemplate.Should().BeNull();
            metadata.Language.Should().BeNull();
            metadata.ArchiveMimeType.Should().BeNull();
            metadata.Dependencies.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
