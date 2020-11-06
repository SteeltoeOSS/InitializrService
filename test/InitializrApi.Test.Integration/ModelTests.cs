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
            var testFile =
                new Uri("https://raw.githubusercontent.com/SteeltoeOSS/InitializrWeb/dev/start-client/dev/api.json");
            using var client = new WebClient();
            var bits = client.DownloadString(testFile);
            var metadata = Serializer.DeserializeJson<ProjectMetadata>(bits);
            metadata.SteeltoeVersion.Default.Should().Be("3.0.1");
            metadata.DotNetFramework.Default.Should().Be("netcoreapp3.1");
            metadata.DotNetTemplate.Default.Should().Be("webapi");
            metadata.Language.Default.Should().Be("csharp");
            metadata.Name.Default.Should().Be("Dev");
            metadata.Namespace.Default.Should().Be("DevNamespace");
            metadata.ApplicationName.Default.Should().Be("DevApplication");
            metadata.Description.Default.Should().Be("Development project application");
            metadata.Dependencies.Values[0].Name.Should().Be("Group One");
            metadata.Dependencies.Values[0].Values[0].Name.Should().Be("Dependency 1.1");
            metadata.Dependencies.Values[0].Values[0].SteeltoeVersionRange.ToString().Should().Be(">=2.4.0 and <3.0.0");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
