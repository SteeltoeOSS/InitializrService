// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Models
{
    public class ProjectSpecConstraintsTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var constraints = new ProjectSpecConstraints();

            // Act

            // Assert
            constraints.SteeltoeVersionRange.Should().BeNull();
            constraints.DotNetFrameworkRange.Should().BeNull();
            constraints.DotNetTemplate.Should().BeNull();
            constraints.Language.Should().BeNull();
        }

        [Fact]
        public void ToString_Should_Be_User_Friendly()
        {
            // Arrange
            var constraints = new ProjectSpecConstraints();

            // Act
            var s = constraints.ToString();

            // Assert
            s.Should().Be("[steeltoeVersionRange=,dotNetFrameworkRange=,dotNetTemplate=,language=]");

            // Arrange
            constraints.SteeltoeVersionRange = new ReleaseRange("myversion1.0");

            // Act
            s = constraints.ToString();

            // Assert
            s.Should().Be(
                "[steeltoeVersionRange=>=myversion1.0,dotNetFrameworkRange=,dotNetTemplate=,language=]");

            // Arrange
            constraints.DotNetFrameworkRange = new ReleaseRange("myframework1.0");

            // Act
            s = constraints.ToString();

            // Assert
            s.Should().Be(
                "[steeltoeVersionRange=>=myversion1.0,dotNetFrameworkRange=>=myframework1.0,dotNetTemplate=,language=]");

            // Arrange
            constraints.DotNetTemplate = "mytemplate";

            // Act
            s = constraints.ToString();

            // Assert
            s.Should().Be(
                "[steeltoeVersionRange=>=myversion1.0,dotNetFrameworkRange=>=myframework1.0,dotNetTemplate=mytemplate,language=]");

            // Arrange
            constraints.Language = "mylanguage";

            // Act
            s = constraints.ToString();

            // Assert
            s.Should().Be(
                "[steeltoeVersionRange=>=myversion1.0,dotNetFrameworkRange=>=myframework1.0,dotNetTemplate=mytemplate,language=mylanguage]");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
