// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Models
{
    public class ProjectMetadataTest
    {
        [Fact]
        public void Properties()
        {
            var metadata = new ProjectMetadata
            {
                Name = new ProjectMetadata.Text
                {
                    Type = "a name type",
                    Default = "a name default",
                },
                Description = new ProjectMetadata.Text
                {
                    Type = "a description type",
                    Default = "a description default",
                },
                SteeltoeVersion = new ProjectMetadata.SingleSelectList
                {
                    Type = "a steeltoe version type",
                    Default = "a steeltoe version default",
                    Values = new[]
                    {
                        new ProjectMetadata.SelectItem
                        {
                            Id = "a steeltoe version id",
                            Name = "a steeltoe version name",
                        },
                    }
                },
                DotNetVersion = new ProjectMetadata.SingleSelectList
                {
                    Type = "a dotnet version type",
                    Default = "a dotnet version default",
                    Values = new[]
                    {
                        new ProjectMetadata.SelectItem
                        {
                            Id = "a dotnet version id",
                            Name = "a dotnet version name",
                        },
                    }
                },
                Type = new ProjectMetadata.SingleSelectList
                {
                    Type = "a dotnet template type type",
                    Default = "a dotnet template type default",
                    Values = new[]
                    {
                        new ProjectMetadata.SelectItem
                        {
                            Id = "a dotnet template type id",
                            Name = "a dotnet template type name",
                        },
                    }
                },
                Language = new ProjectMetadata.SingleSelectList
                {
                    Type = "a language type",
                    Default = "a language default",
                    Values = new[]
                    {
                        new ProjectMetadata.SelectItem
                        {
                            Id = "a language id",
                            Name = "a language name",
                        },
                    }
                },
                Format = new ProjectMetadata.SingleSelectList
                {
                    Type = "a project bundle archive format type",
                    Default = "a project bundle archive format default",
                    Values = new[]
                    {
                        new ProjectMetadata.SelectItem
                        {
                            Id = "a project bundle archive format id",
                            Name = "a project bundle archive format name",
                        },
                    }
                },
                Dependencies = new ProjectMetadata.GroupList
                {
                    Type = "a dependencies type",
                    Values = new[]
                    {
                        new ProjectMetadata.Group
                        {
                            Name = "a dependency group name",
                            Values = new[]
                            {
                                new ProjectMetadata.GroupItem
                                {
                                    Id = "a dependency id",
                                    Name = "a dependency name",
                                    Description = "a dependency description",
                                    SteeltoeVersionRange = "a dependency steeltoe version range",
                                },
                            },
                        },
                    },
                },
            };
            metadata.Name.Type.Should().Be("a name type");
            metadata.Name.Default.Should().Be("a name default");
            metadata.Description.Type.Should().Be("a description type");
            metadata.Description.Default.Should().Be("a description default");
            metadata.SteeltoeVersion.Type.Should().Be("a steeltoe version type");
            metadata.SteeltoeVersion.Default.Should().Be("a steeltoe version default");
            metadata.SteeltoeVersion.Values[0].Id.Should().Be("a steeltoe version id");
            metadata.SteeltoeVersion.Values[0].Name.Should().Be("a steeltoe version name");
            metadata.DotNetVersion.Type.Should().Be("a dotnet version type");
            metadata.DotNetVersion.Default.Should().Be("a dotnet version default");
            metadata.DotNetVersion.Values[0].Id.Should().Be("a dotnet version id");
            metadata.DotNetVersion.Values[0].Name.Should().Be("a dotnet version name");
            metadata.Type.Type.Should().Be("a dotnet template type type");
            metadata.Type.Default.Should().Be("a dotnet template type default");
            metadata.Type.Values[0].Id.Should().Be("a dotnet template type id");
            metadata.Type.Values[0].Name.Should().Be("a dotnet template type name");
            metadata.Language.Type.Should().Be("a language type");
            metadata.Language.Default.Should().Be("a language default");
            metadata.Language.Values[0].Id.Should().Be("a language id");
            metadata.Language.Values[0].Name.Should().Be("a language name");
            metadata.Format.Type.Should().Be("a project bundle archive format type");
            metadata.Format.Default.Should().Be("a project bundle archive format default");
            metadata.Format.Values[0].Id.Should().Be("a project bundle archive format id");
            metadata.Format.Values[0].Name.Should().Be("a project bundle archive format name");
            metadata.Dependencies.Type.Should().Be("a dependencies type");
            metadata.Dependencies.Values[0].Name.Should().Be("a dependency group name");
            metadata.Dependencies.Values[0].Values[0].Id.Should().Be("a dependency id");
            metadata.Dependencies.Values[0].Values[0].Name.Should().Be("a dependency name");
            metadata.Dependencies.Values[0].Values[0].Description.Should().Be("a dependency description");
            metadata.Dependencies.Values[0].Values[0].SteeltoeVersionRange.Should().Be("a dependency steeltoe version range");
        }
    }
}
