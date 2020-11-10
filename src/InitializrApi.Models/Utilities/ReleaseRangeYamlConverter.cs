// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable
using System;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Steeltoe.InitializrApi.Models.Utilities
{
    /// <summary>
    /// Deserializes a <see cref="ReleaseRange"/> in YAML expressions.
    /// </summary>
    public class ReleaseRangeYamlConverter : IYamlTypeConverter
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */
        private static readonly Type RangeType = typeof(ReleaseRange);

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Returns whether type is of <see cref="ReleaseRange"/>.
        /// </summary>
        /// <param name="type">Type to be tested.</param>
        /// <returns>Whether the type is of <see cref="ReleaseRange"/>.</returns>
        public bool Accepts(Type type)
        {
            return type == RangeType;
        }

        /// <inheritdoc/>
        public object? ReadYaml(IParser parser, Type type)
        {
            if (parser.TryConsume<Scalar>(out var expression))
            {
                if (string.IsNullOrEmpty(expression.Value))
                {
                    return null;
                }

                return new ReleaseRange(expression.Value);
            }

            throw new InvalidDataException($"Invalid YAML content: {parser.Current}");
        }

        /// <inheritdoc/>
        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
