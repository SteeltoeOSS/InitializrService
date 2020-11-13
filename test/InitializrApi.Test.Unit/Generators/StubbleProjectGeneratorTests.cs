// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Steeltoe.InitializrApi.Generators;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Generators
{
    public class StubbleProjectGeneratorTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void GenerateProject_Should_Generate_A_Project()
        {
            // Arrange
            var spec = new ProjectSpec();
            var template = new ProjectTemplate { Manifest = new FileEntry[0] };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.Should().NotBeNull();
        }

        [Fact]
        public void GenerateProject_Should_Contain_Top_Level_Directory()
        {
            // Arrange
            var spec = new ProjectSpec { Name = "tld", };
            var template = new ProjectTemplate { Manifest = new FileEntry[0] };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[0].Path.Should().Be("tld/");
        }

        [Fact]
        public void GenerateProject_Should_Render_Files()
        {
            // Arrange
            var spec = new ProjectSpec
            {
                Name = "my name",
                Description = "my description",
                Namespace = "my namespace",
                SteeltoeVersion = "my steeltoe version",
                DotNetFramework = "my dotnet framework",
                DotNetTemplate = "my dotnet template",
                Language = "my language",
            };
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "name", Text = "{{Name}}" },
                    new FileEntry { Path = "description", Text = "{{Description}}" },
                    new FileEntry { Path = "namespace", Text = "{{Namespace}}" },
                    new FileEntry { Path = "steeltoe version", Text = "{{SteeltoeVersion}}" },
                    new FileEntry { Path = "dotnet framework", Text = "{{DotNetFramework}}" },
                    new FileEntry { Path = "dotnet template", Text = "{{DotNetTemplate}}" },
                    new FileEntry { Path = "language", Text = "{{Language}}" },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            var i = 1;
            project.FileEntries[i++].Text.Should().Be("my name");
            project.FileEntries[i++].Text.Should().Be("my description");
            project.FileEntries[i++].Text.Should().Be("my namespace");
            project.FileEntries[i++].Text.Should().Be("my steeltoe version");
            project.FileEntries[i++].Text.Should().Be("my dotnet framework");
            project.FileEntries[i++].Text.Should().Be("my dotnet template");
            project.FileEntries[i].Text.Should().Be("my language");
        }

        [Fact]
        public void GenerateProject_Should_Render_Directories()
        {
            // Arrange
            var spec = new ProjectSpec();
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "d1/" },
                    new FileEntry { Path = "d1/d2/" },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[1].Path.Should().Be("d1/");
            project.FileEntries[1].Text.Should().BeNull();
            project.FileEntries[2].Path.Should().Be("d1/d2/");
            project.FileEntries[2].Text.Should().BeNull();
        }

        [Fact]
        public void GenerateProject_Should_Rename_Files()
        {
            // Arrange
            var spec = new ProjectSpec { Namespace = "My.Namespace" };
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "oldName", Rename = "{{Namespace}}" },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[1].Path.Should().Be("My.Namespace");
        }

        [Fact]
        public void GenerateProject_Should_Omit_Files_If_Dependency_Not_Met()
        {
            // Arrange
            var spec = new ProjectSpec { Dependencies = "dep0,dep1" };
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "f0" },
                    new FileEntry { Path = "f1", Dependencies = "dep0" },
                    new FileEntry { Path = "f2", Dependencies = "dep1" },
                    new FileEntry { Path = "f3", Dependencies = "dep0,dep1" },
                    new FileEntry { Path = "f4", Dependencies = "dep2" },
                    new FileEntry { Path = "f5", Dependencies = "dep0,dep2" },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries.Count.Should().Be(6);
            var i = 1;
            project.FileEntries[i++].Path.Should().Be("f0");
            project.FileEntries[i++].Path.Should().Be("f1");
            project.FileEntries[i++].Path.Should().Be("f2");
            project.FileEntries[i++].Path.Should().Be("f3");
            project.FileEntries[i].Path.Should().Be("f5");
        }

        [Fact]
        public void GenerateProject_Should_Evaluate_Parameter_Value()
        {
            // Arrange
            var spec = new ProjectSpec();
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "f", Text = "{{Param}}" },
                },
                Parameters = new[]
                {
                    new Parameter()
                    {
                        Name = "Param",
                        Value = "text",
                    },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[1].Text.Should().Be("text");
        }

        [Fact]
        public void GenerateProject_Should_Evaluate_Parameter_Expression()
        {
            // Arrange
            var spec = new ProjectSpec { Dependencies = "Dep1,Dep2" };
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "f", Text = "{{#Or}}text{{/Or}}" },
                },
                Parameters = new[]
                {
                    new Parameter()
                    {
                        Name = "Or",
                        Expression = "Dep1||Dep2",
                    },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[1].Text.Should().Be("text");
        }

        [Fact]
        public void GenerateProject_Should_Give_Precedence_To_Parameter_Value()
        {
            // Arrange
            var spec = new ProjectSpec { Dependencies = "Dep1" };
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "f", Text = "{{Which}}" },
                },
                Parameters = new[]
                {
                    new Parameter()
                    {
                        Name = "Which",
                        Value = "text",
                        Expression = "Dep",
                    },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[1].Text.Should().Be("text");
        }

        [Fact]
        public void GenerateProject_Should_NoOp_Parameter_With_Value_Or_Expression()
        {
            // Arrange
            var spec = new ProjectSpec { Dependencies = "Dep1" };
            var template = new ProjectTemplate
            {
                Manifest = new[]
                {
                    new FileEntry { Path = "f", Text = "{{Which}}" },
                },
                Parameters = new[]
                {
                    new Parameter()
                    {
                        Name = "Which",
                    },
                },
            };
            var generator = new StubbleProjectGeneratorBuilder().WithProjectTemplate(template).Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.FileEntries[1].Text.Should().Be(string.Empty);
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void No_Template_Should_Return_Null_Project()
        {
            // Arrange
            var spec = new ProjectSpec();
            var generator = new StubbleProjectGeneratorBuilder().Build();

            // Act
            var project = generator.GenerateProject(spec);

            // Assert
            project.Should().BeNull();
        }

        /* ----------------------------------------------------------------- *
         * test helpers                                                      *
         * ----------------------------------------------------------------- */

        class StubbleProjectGeneratorBuilder
        {
            private Mock<IProjectTemplateRegistry> _registry = new Mock<IProjectTemplateRegistry>();

            private ILogger<StubbleProjectGenerator> _logger = new NullLogger<StubbleProjectGenerator>();

            internal StubbleProjectGenerator Build()
            {
                return new StubbleProjectGenerator(_registry.Object, _logger);
            }

            internal StubbleProjectGeneratorBuilder WithProjectTemplate(ProjectTemplate template)
            {
                _registry.Setup(reg => reg.Lookup(It.IsAny<ProjectSpec>())).Returns(template);
                return this;
            }
        }
    }
}
