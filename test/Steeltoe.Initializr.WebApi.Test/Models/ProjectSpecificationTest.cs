// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.Initializr.WebApi.Models;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Models
{
    public class ProjectSpecificationTest
    {
        [Fact]
        public void ProjectSpecificationProperties()
        {
            var spec = new ProjectSpecification
            {
                ProjectName = "some name",
                ProjectDescription = "some description",
                SteeltoeReleaseId = "some Steeltoe release ID",
                DotnetFrameworkTargetId = "some DotNet framework ID",
                DotnetLanguageId = "some DotNet language ID",
                DotnetTemplateId = "some DotNet template ID",
            };
            spec.ProjectName.Should().Be("some name");
            spec.ProjectDescription.Should().Be("some description");
            spec.SteeltoeReleaseId.Should().Be("some Steeltoe release ID");
            spec.DotnetFrameworkTargetId.Should().Be("some DotNet framework ID");
            spec.DotnetLanguageId.Should().Be("some DotNet language ID");
            spec.DotnetTemplateId.Should().Be("some DotNet template ID");
        }
    }
}
