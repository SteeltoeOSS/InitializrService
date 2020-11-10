// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models.Utilities;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Steeltoe.InitializrApi.Utilities
{
    /// <summary>
    /// A utility wrapper for JSON serialization/deserialization.
    /// </summary>
    public static class Serializer
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        private static JsonSerializerOptions Options { get; } = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Deserializes a JSON string into an object.
        /// </summary>
        /// <param name="json">JSON string.</param>
        /// <typeparam name="T">Target object type.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        /// <summary>
        /// Deserializes a YAML string into an object.
        /// </summary>
        /// <param name="yaml">YAML string.</param>
        /// <typeparam name="T">Target object type.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeYaml<T>(string yaml)
        {
            return new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeConverter(new ReleaseRangeYamlConverter())
                .Build().Deserialize<T>(yaml);
        }
    }
}
