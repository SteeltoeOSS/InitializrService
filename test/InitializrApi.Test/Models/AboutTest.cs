// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Models
{
    public class AboutTest
    {
        [Fact]
        public void Properties()
        {
            var about = new About
            {
                Name = "some name",
                Vendor = "some vendor",
                Url = "some url",
                Version = "some version",
                Commit = "some commit",
            };
            about.Name.Should().Be("some name");
            about.Vendor.Should().Be("some vendor");
            about.Url.Should().Be("some url");
            about.Version.Should().Be("some version");
            about.Commit.Should().Be("some commit");
        }
    }
}
