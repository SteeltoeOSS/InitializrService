using System;
using FluentAssertions;
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.Initializr.WebApi.Models;
using Steeltoe.Initializr.WebApi.Services;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Services
{
    public class StubbleProjectGeneratorTest
    {
        [Fact]
        public void NotImplemented()
        {
            // Arrange
            var spec = new ProjectSpecification();
            var generator = new StubbleProjectGenerator();

            // Act
            Action act = () => generator.GenerateProject(spec);

            // Assert
            act.Should().Throw<NotImplementedException>();
        }
    }
}
