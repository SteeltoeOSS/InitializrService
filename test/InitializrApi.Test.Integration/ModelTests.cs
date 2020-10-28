// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Utilities;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Integration
{
    public class ModelTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void ProjectSpec_Should_Load_UI_Test_File()
        {
            var testFile = new Uri("https://raw.githubusercontent.com/SteeltoeOSS/InitializrWeb/dev/start-client/dev/api.json");
            using var client = new WebClient();
            var bits = client.DownloadString(testFile);
            var metadata = Serializer.DeserializeJson<ProjectMetadata>(bits);
            metadata.SteeltoeVersion.Default.Should().Be("3.0.0");
            metadata.DotNetFramework.Default.Should().Be("netcoreapp3.1");
            metadata.DotNetTemplate.Default.Should().Be("webapi");
            metadata.Language.Default.Should().Be("csharp");
            metadata.Project.Default.Should().Be("Sample");
            metadata.Namespace.Default.Should().Be("Sample");
            metadata.Application.Default.Should().Be("SampleApplication");
            metadata.Description.Default.Should().Be("Sample application project");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
