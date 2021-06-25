// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Steeltoe.InitializrApi.Models.Utilities
{
    /// <summary>
    /// Deserializes a <see cref="ReleaseRange"/> in JSON expressions.
    /// </summary>
    public class ReleaseRangeJsonConverter : JsonConverter<ReleaseRange>
    {
        /// <inheritdoc />
        public override ReleaseRange Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new ReleaseRange(reader.GetString());
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, ReleaseRange value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
