// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Models
{
    public class ReleaseRangeTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Empty_Or_Null_Range_Accepts_All_Versions()
        {
            // Arrange
            var rr1 = new ReleaseRange(null);
            var rr2 = new ReleaseRange(string.Empty);

            // Act
            var s1 = rr1.ToString();
            var s2 = rr1.ToString();

            // Assert
            s1.Should().BeEmpty();
            s2.Should().BeEmpty();
            rr1.Accepts("v1.2").Should().BeTrue();
            rr2.Accepts("v1.2").Should().BeTrue();
        }

        [Fact]
        public void ToString_Should_Be_Similar_To_Original_String()
        {
            // Arrange
            var r1 = new ReleaseRange("1.0");
            var r2 = new ReleaseRange("[1.0,2.0]");
            var r3 = new ReleaseRange("(1.0, 2.0]");
            var r4 = new ReleaseRange("[1.0,2.0)");
            var r5 = new ReleaseRange("(1.0, 2.0)");

            // Act
            var s1 = r1.ToString();
            var s2 = r2.ToString();
            var s3 = r3.ToString();
            var s4 = r4.ToString();
            var s5 = r5.ToString();

            // Assert
            s1.Should().Be("1.0");
            s2.Should().Be("[1.0,2.0]");
            s3.Should().Be("(1.0,2.0]");
            s4.Should().Be("[1.0,2.0)");
            s5.Should().Be("(1.0,2.0)");
        }

        [Fact]
        public void ToPrettyString_Should_Represent_Range()
        {
            // Arrange
            var r0 = new ReleaseRange();
            var r1 = new ReleaseRange("1.0");
            var r2 = new ReleaseRange("[1.0,2.0]");
            var r3 = new ReleaseRange("(1.0,2.0]");
            var r4 = new ReleaseRange("[1.0,2.0)");
            var r5 = new ReleaseRange("(1.0,2.0)");

            // Act
            var s0 = r0.ToPrettyString();
            var s1 = r1.ToPrettyString();
            var s2 = r2.ToPrettyString();
            var s3 = r3.ToPrettyString();
            var s4 = r4.ToPrettyString();
            var s5 = r5.ToPrettyString();

            // Assert
            s0.Should().BeEmpty();
            s1.Should().Be(">=1.0");
            s2.Should().Be(">=1.0 and <=2.0");
            s3.Should().Be(">1.0 and <=2.0");
            s4.Should().Be(">=1.0 and <2.0");
            s5.Should().Be(">1.0 and <2.0");
        }

        [Fact]
        public void Spaces_Should_Be_Ignored()
        {
            // Arrange
            var r = new ReleaseRange("[1.0, 2.0]");

            // Act
            var s = r.ToString();

            // Assert
            s.Should().Be("[1.0,2.0]");
        }

        [Fact]
        public void Range_Should_Only_Accept_Versions_Within_Boundaries()
        {
            // Arrange
            var r1 = new ReleaseRange("prod9.5");
            var r2 = new ReleaseRange("[prod9.0, prod10.0]");
            var r3 = new ReleaseRange("(prod9.0, prod10.0]");
            var r4 = new ReleaseRange("[prod9.0, prod10.0)");
            var r5 = new ReleaseRange("(prod9.0, prod10.0)");

            // Act

            // Assert
            r1.Accepts("prod9.0").Should().BeFalse();
            r2.Accepts("prod9.0").Should().BeTrue();
            r3.Accepts("prod9.0").Should().BeFalse();
            r4.Accepts("prod9.0").Should().BeTrue();
            r5.Accepts("prod9.0").Should().BeFalse();
            r1.Accepts("prod9.5").Should().BeTrue();
            r2.Accepts("prod9.5").Should().BeTrue();
            r3.Accepts("prod9.5").Should().BeTrue();
            r4.Accepts("prod9.5").Should().BeTrue();
            r5.Accepts("prod9.5").Should().BeTrue();
            r1.Accepts("prod10.0").Should().BeTrue();
            r2.Accepts("prod10.0").Should().BeTrue();
            r3.Accepts("prod10.0").Should().BeTrue();
            r4.Accepts("prod10.0").Should().BeFalse();
            r5.Accepts("prod10.0").Should().BeFalse();
        }

        [Fact]
        public void Release_Version_To_String_Should_Be_Original_Version()
        {
            // Arrange
            var v = new ReleaseRange.ReleaseVersion("1.2.3");

            // Act
            var s = v.ToString();

            // Assert
            s.Should().Be("1.2.3");
        }

        [Fact]
        public void Earlier_Release_Version_Should_Be_Less_Than_Later_Version()
        {
            // Arrange
            var v1 = new ReleaseRange.ReleaseVersion("1.0");
            var v2 = new ReleaseRange.ReleaseVersion("1.1");
            var v3 = new ReleaseRange.ReleaseVersion("2.0");

            // Act

            // Assert
            (v1 < v2).Should().Be(true);
            (v1 > v2).Should().Be(false);
            (v1 < v3).Should().Be(true);
            (v1 > v3).Should().Be(false);
            (v2 < v3).Should().Be(true);
            (v2 > v3).Should().Be(false);
        }

        [Fact]
        public void Later_Release_Version_Should_Be_Greater_Than_Later_Version()
        {
            // Arrange
            var v1 = new ReleaseRange.ReleaseVersion("1.0");
            var v2 = new ReleaseRange.ReleaseVersion("1.1");
            var v3 = new ReleaseRange.ReleaseVersion("2.0");

            // Act

            // Assert
            (v3 > v2).Should().Be(true);
            (v3 < v2).Should().Be(false);
            (v3 > v1).Should().Be(true);
            (v3 < v1).Should().Be(false);
            (v2 > v1).Should().Be(true);
            (v2 < v1).Should().Be(false);
        }

        [Fact]
        public void Equivalent_Release_Versions_Should_Be_Equal()
        {
            // Arrange
            var v1 = new ReleaseRange.ReleaseVersion("1.0");
            var v2 = new ReleaseRange.ReleaseVersion("1.0");

            // Act

            // Assert
            (v1 == v2).Should().Be(true);
            (v1 != v2).Should().Be(false);
        }

        [Fact]
        public void Different_Release_Versions_Should_Not_Be_Equal()
        {
            // Arrange
            var v1 = new ReleaseRange.ReleaseVersion("1.0");
            var v2 = new ReleaseRange.ReleaseVersion("2.0");

            // Act

            // Assert
            (v1 != v2).Should().Be(true);
            (v1 == v2).Should().Be(false);
        }

        [Fact]
        public void Release_Version_Prefixes_Should_Be_Ignored()
        {
            // Arrange
            var v1 = new ReleaseRange.ReleaseVersion("netcoreapp2.1");
            var v2 = new ReleaseRange.ReleaseVersion("netcoreapp3.1");
            var v3 = new ReleaseRange.ReleaseVersion("net5.0");

            // Act

            // Assert
            (v1 < v2).Should().Be(true);
            (v1 > v2).Should().Be(false);
            (v1 < v3).Should().Be(true);
            (v1 > v3).Should().Be(false);
            (v2 < v3).Should().Be(true);
            (v2 > v3).Should().Be(false);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Range_Must_Follow_Format()
        {
            // Arrange

            // Act
            Action a1 = () =>
            {
                var _ = new ReleaseRange("1.0,2.0");
            };
            Action a2 = () =>
            {
                var _ = new ReleaseRange("[1.0,2.0");
            };
            Action a3 = () =>
            {
                var _ = new ReleaseRange("myversion");
            };

            // Assert
            a1.Should().Throw<ArgumentException>()
                .WithMessage("Release range start version must begin with '[' or '(': '1.0'");
            a2.Should().Throw<ArgumentException>()
                .WithMessage("Release range stop version must end with ']' or ')': '2.0'");
            a3.Should().Throw<ArgumentException>()
                .WithMessage("Release range must contain 1 or 2 versions: 'myversion'");
        }

        [Fact]
        public void Range_Cannot_Contain_More_Than_Two_Versions()
        {
            // Arrange

            // Act
            Action a = () =>
            {
                var _ = new ReleaseRange("[1.0,2.0,3.0]");
            };

            // Assert
            a.Should().Throw<ArgumentException>()
                .WithMessage("Release range cannot contain more than 2 versions: '[1.0,2.0,3.0]'");
        }

        [Fact]
        public void Release_Version_Cannot_Be_Empty_Or_Null()
        {
            // Arrange

            // Act
            Action a1 = () =>
            {
                var _ = new ReleaseRange.ReleaseVersion(null);
            };
            Action a2 = () =>
            {
                var _ = new ReleaseRange.ReleaseVersion("");
            };

            // Assert
            a1.Should().Throw<ArgumentException>().WithMessage("Release version cannot be empty or null");
            a2.Should().Throw<ArgumentException>().WithMessage("Release version cannot be empty or null");
        }

        [Fact]
        public void Release_Version_Should_Not_Contain_Empty_Nodes()
        {
            // Arrange

            // Act
            Action a1 = () =>
            {
                var _ = new ReleaseRange.ReleaseVersion("1..2");
            };

            // Assert
            a1.Should().Throw<ArgumentException>().WithMessage("Version not in correct format: '1..2'");
        }
    }
}
