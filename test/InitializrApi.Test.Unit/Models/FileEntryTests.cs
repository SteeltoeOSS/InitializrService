// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Models
{
    public class FileEntryTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var entry = new FileEntry();

            // Act

            // Assert
            entry.Path.Should().BeNull();
            entry.Text.Should().BeNull();
            entry.Rename.Should().BeNull();
            entry.Dependencies.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
