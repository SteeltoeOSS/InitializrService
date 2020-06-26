// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.Initializr.WebApi.Models.Project;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Models.Project
{
    public class SpecificationTest
    {
        [Fact]
        public void ObjectEquals()
        {
            var obj1 = new Specification();
            var obj2 = new Specification();
            obj1.Equals(obj2).Should().BeTrue();
        }

        [Fact]
        public void ObjectHashCode()
        {
            var obj1 = new Specification();
            var obj2 = new Specification();
            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }
    }
}
