// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Models
{
    public class InitializrConfigTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var config = new InitializrConfig();

            // Act

            // Assert
            config.ProjectMetadata.Should().BeNull();
            config.ProjectTemplates.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
