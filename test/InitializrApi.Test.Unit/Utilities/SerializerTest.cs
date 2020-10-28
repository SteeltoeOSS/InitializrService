// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Steeltoe.InitializrApi.Utilities;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Utilities
{
    public class SerializerTest
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void DeserializeJson_Should_Return_Deserialized_Object()
        {
            // Arrange
            var json = @"{""DummyProperty"": ""dummy""}";

            // Act
            var dummy = Serializer.DeserializeJson<DummyObject>(json);

            // Assert
            dummy.Should().NotBeNull();
            dummy.DummyProperty.Should().Be("dummy");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * test helpers                                                      *
         * ----------------------------------------------------------------- */

        public class DummyObject
        {
            public string DummyProperty { get; set; }
        }
    }
}
