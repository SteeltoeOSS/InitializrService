// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Xunit;

namespace Steeltoe.InitializrService.Test.Integration
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
            about.Name.Should().Be("Steeltoe.InitializrService");
            about.Vendor.Should().Be("SteeltoeOSS/Broadcom");
            about.Url.Should().Be("https://github.com/SteeltoeOSS/InitializrService/");
            about.Version.Should().NotBeNull();
            about.Version.Should().Contain("1.0.0");
            about.Commit.Should().NotBeNull();
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
