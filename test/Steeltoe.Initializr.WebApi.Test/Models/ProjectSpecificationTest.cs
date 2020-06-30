// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
                DotnetFrameworkTargetId = "some DotNet framework target ID",
                DotnetLanguageId = "some Dotnet language ID",
                DotnetTemplateId = "some DotNet template ID",
            };
        }
    }
}
