// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrService.Models;
using Xunit;

namespace Steeltoe.InitializrService.Test.Unit.Models
{
    public class AboutTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var about = new About();

            // Act

            // Assert
            about.Name.Should().BeNull();
            about.Vendor.Should().BeNull();
            about.Url.Should().BeNull();
            about.Version.Should().BeNull();
            about.Commit.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
