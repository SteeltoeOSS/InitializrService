// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Models
{
    public class InitializrApiConfigurationTest
    {
        [Fact]
        public void Properties()
        {
            var config = new InitializrApiConfiguration
            {
                Metadata = new ProjectMetadata
                {
                    Name = new ProjectMetadata.Text
                    {
                        Default = "a metadata name",
                    },
                },
                Templates = new[]
                {
                    new ProjectTemplateConfiguration
                    {
                        Uri = new Uri("/a uri"),
                    },
                },
            };
            config.Metadata.Name.Default.Should().Be("a metadata name");
            config.Templates[0].Uri.Should().Be("file:///a uri");
        }
    }
}
