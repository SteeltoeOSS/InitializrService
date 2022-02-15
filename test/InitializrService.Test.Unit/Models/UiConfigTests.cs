// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrService.Config;
using Xunit;

namespace Steeltoe.InitializrService.Test.Unit.Models
{
    public class UiConfigTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void Properties_Should_Be_Defined()
        {
            // Arrange
            var uiConfig = new UiConfig();

            // Act

            // Assert
            uiConfig.Name.Should().BeNull();
            uiConfig.Namespace.Should().BeNull();
            uiConfig.Description.Should().BeNull();
            uiConfig.SteeltoeVersion.Should().BeNull();
            uiConfig.DotNetFramework.Should().BeNull();
            uiConfig.Language.Should().BeNull();
            uiConfig.Packaging.Should().BeNull();
            uiConfig.Dependencies.Should().NotBeNull();
        }

        [Fact]
        public void Item_Name_Should_Default_To_Id()
        {
            // Arrange
            var item = new ConcreteItem { Id = "joe" };

            // Act

            // Assert
            item.Name.Should().Be("joe");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * helpers                                                           *
         * ----------------------------------------------------------------- */

        class ConcreteItem : UiConfig.Item
        {
        }
    }
}
