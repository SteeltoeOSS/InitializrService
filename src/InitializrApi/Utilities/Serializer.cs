// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace Steeltoe.InitializrApi.Utilities
{
    /// <summary>
    /// A utility wrapper for JSON serialization/deserialization.
    /// </summary>
    public static class Serializer
    {
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
    }
}
