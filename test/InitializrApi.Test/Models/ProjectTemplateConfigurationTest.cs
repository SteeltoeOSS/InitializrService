// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Models
{
    public class ProjectTemplateConfigurationTest
    {
        [Fact]
        public void Properties()
        {
            var config = new ProjectTemplateConfiguration
            {
                Uri = new Uri("file:///a uri"),
                SteeltoeVersionRange = "a steeltoe version range",
                DotNetVersionRange = "a dotnet version range",
                Type = "a dotnet template type",
                Language = "a language",
            };
            config.Uri.Should().Be("file:///a uri");
            config.SteeltoeVersionRange.Should().Be("a steeltoe version range");
            config.DotNetVersionRange.Should().Be("a dotnet version range");
            config.Type.Should().Be("a dotnet template type");
            config.Language.Should().Be("a language");
        }
    }
}
