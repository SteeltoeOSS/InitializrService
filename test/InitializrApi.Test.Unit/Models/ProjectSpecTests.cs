// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Models
{
    public class ProjectSpecTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var spec = new ProjectSpec();

            // Act

            // Assert
            spec.Name.Should().BeNull();
            spec.Description.Should().BeNull();
            spec.Namespace.Should().BeNull();
            spec.SteeltoeVersion.Should().BeNull();
            spec.DotNetFramework.Should().BeNull();
            spec.DotNetTemplate.Should().BeNull();
            spec.Language.Should().BeNull();
            spec.Packaging.Should().BeNull();
        }

        [Fact]
        public void Application_Should_Be_Assumed()
        {
            // Arrange
            var spec = new ProjectSpec { Packaging = "foo" };

            // Act

            // Assert
            spec.Packaging.Should().Be("foo");
        }

        [Fact]
        public void ToString_Should_Be_User_Friendly()
        {
            // Arrange
            var spec = new ProjectSpec();

            // Act
            var s = spec.ToString();

            // Assert
            s.Should().Be("[]");

            // Arrange
            spec.Packaging = "mypackaging";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be("[packaging=mypackaging]");

            // Arrange
            spec.Language = "mylanguage";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be("[language=mylanguage,packaging=mypackaging]");

            // Arrange
            spec.DotNetTemplate = "mytemplate";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be("[dotNetTemplate=mytemplate,language=mylanguage,packaging=mypackaging]");

            // Arrange
            spec.DotNetFramework = "myframework";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[dotNetFramework=myframework,dotNetTemplate=mytemplate,language=mylanguage,packaging=mypackaging]");

            // Arrange
            spec.SteeltoeVersion = "mysteeltoeversion";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[steeltoeVersion=mysteeltoeversion,dotNetFramework=myframework,dotNetTemplate=mytemplate,language=mylanguage,packaging=mypackaging]");

            // Arrange
            spec.Namespace = "mynamespace";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[namespace=mynamespace,steeltoeVersion=mysteeltoeversion,dotNetFramework=myframework,dotNetTemplate=mytemplate,language=mylanguage,packaging=mypackaging]");

            // Arrange
            spec.Description = "mydesc";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[description=mydesc,namespace=mynamespace,steeltoeVersion=mysteeltoeversion,dotNetFramework=myframework,dotNetTemplate=mytemplate,language=mylanguage,packaging=mypackaging]");

            // Arrange
            spec.Name = "myname";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[name=myname,description=mydesc,namespace=mynamespace,steeltoeVersion=mysteeltoeversion,dotNetFramework=myframework,dotNetTemplate=mytemplate,language=mylanguage,packaging=mypackaging]");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
