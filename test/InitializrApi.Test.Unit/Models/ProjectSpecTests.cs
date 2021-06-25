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
            spec.Application.Should().BeNull();
            spec.SteeltoeVersion.Should().BeNull();
            spec.DotNetFramework.Should().BeNull();
            spec.Language.Should().BeNull();
            spec.Packaging.Should().BeNull();
        }

        [Fact]
        public void Application_Should_Be_Default_to_Name()
        {
            // Arrange
            var spec = new ProjectSpec();

            // Act
            spec.Name = "foo";

            // Assert
            spec.Application.Should().Be("foo");

            // Act
            spec.Application = "bar";

            // Assert
            spec.Application.Should().Be("bar");
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
            s.Should().Be("[pkg=mypackaging]");

            // Arrange
            spec.Language = "mylanguage";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be("[lang=mylanguage,pkg=mypackaging]");

            // Arrange
            spec.DotNetFramework = "myframework";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[framework=myframework,lang=mylanguage,pkg=mypackaging]");

            // Arrange
            spec.SteeltoeVersion = "mysteeltoeversion";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[steeltoe=mysteeltoeversion,framework=myframework,lang=mylanguage,pkg=mypackaging]");

            // Arrange
            spec.Application = "myapp";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[app=myapp,steeltoe=mysteeltoeversion,framework=myframework,lang=mylanguage,pkg=mypackaging]");

            // Arrange
            spec.Description = "mydesc";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[app=myapp,desc=mydesc,steeltoe=mysteeltoeversion,framework=myframework,lang=mylanguage,pkg=mypackaging]");

            // Arrange
            spec.Name = "myname";

            // Act
            s = spec.ToString();

            // Assert
            s.Should().Be(
                "[name=myname,app=myapp,desc=mydesc,steeltoe=mysteeltoeversion,framework=myframework,lang=mylanguage,pkg=mypackaging]");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
