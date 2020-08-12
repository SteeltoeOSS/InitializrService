// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Models
{
    public class ProjectSpecificationTest
    {
        [Fact]
        public void Properties()
        {
            var spec = new ProjectSpecification
            {
                Name = "some name",
                Description = "some description",
                SteeltoeVersion = "some Steeltoe release ID",
                DotnetTargetFrameworkId = "some DotNet framework ID",
                DotnetLanguageId = "some DotNet language ID",
                DotnetTemplateId = "some DotNet template ID",
            };
            spec.Name.Should().Be("some name");
            spec.Description.Should().Be("some description");
            spec.SteeltoeVersion.Should().Be("some Steeltoe release ID");
            spec.DotnetTargetFrameworkId.Should().Be("some DotNet framework ID");
            spec.DotnetLanguageId.Should().Be("some DotNet language ID");
            spec.DotnetTemplateId.Should().Be("some DotNet template ID");
        }
    }
}
