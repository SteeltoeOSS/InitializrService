// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using FluentAssertions;
using Steeltoe.InitializrService.Config;
using Steeltoe.InitializrService.Utilities;
using Xunit;

namespace Steeltoe.InitializrService.Test.Integration
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
                new Uri("https://raw.githubusercontent.com/SteeltoeOSS/InitializrWeb/dev/start-client/dev/api.mock.json");
            using var client = new WebClient();
            var bits = client.DownloadString(testFile);
            var uiConfig = Serializer.DeserializeJson<UiConfig>(bits);
            uiConfig.SteeltoeVersion.Default.Should().Be("3.0.2");
            uiConfig.DotNetFramework.Default.Should().Be("netcoreapp3.1");
            uiConfig.Language.Default.Should().Be("csharp");
            uiConfig.Name.Default.Should().Be("Dev");
            uiConfig.Namespace.Default.Should().Be("DevNamespace");
            uiConfig.Description.Default.Should().Be("Development project application");
            uiConfig.Dependencies.Values[0].Name.Should().Be("Focus Group");
            uiConfig.Dependencies.Values[0].Values[0].Name.Should().Be("Focus Dependency");
            uiConfig.Dependencies.Values[0].Values[0].SteeltoeVersionRange.Should().Be("[2.4.0,3.0.0)");
            uiConfig.Dependencies.Values[0].Values[0].DotNetFrameworkRange.Should().Be("netcoreapp3.1");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
