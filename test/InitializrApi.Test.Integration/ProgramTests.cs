// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Integration
{
    public class ProgramTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void About_Should_Contain_About_Details()
        {
            var about = Program.About;
            about.Name.Should().Be("Steeltoe.InitializrApi");
            about.Vendor.Should().Be("SteeltoeOSS/VMware");
            about.Url.Should().Be("https://github.com/SteeltoeOSS/InitializrApi/");
            about.Version.Should().NotBeNull();
            about.Version.Should().StartWith("0.12.0");
            about.Commit.Should().NotBeNull();
            about.Commit.Length.Should().Be(40); // Git SHA string length
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
