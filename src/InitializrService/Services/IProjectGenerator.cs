// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrService.Models;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Steeltoe.InitializrService.Services
{
    /// <summary>
    /// Contract for a project generator implementations.
    /// </summary>
    public interface IProjectGenerator
    {
        /// <summary>
        /// Generates a project based on the spec.
        /// </summary>
        /// <param name="spec">Project specification.</param>
        /// <returns>The generated project archive as a byte array, or null if not able to generate project per spec.</returns>
        Task<byte[]> GenerateProjectArchive(ProjectSpec spec);
    }

    /// <inheritdoc />
    [Serializable]
    public class NoProjectForSpecException : Exception
    {
        /// <inheritdoc cref="Exception"/>
        public NoProjectForSpecException(string message)
            : base(message)
        {
        }

        /// <inheritdoc cref="Exception"/>
        protected NoProjectForSpecException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <inheritdoc />
    [Serializable]
    public class InvalidSpecException : Exception
    {
        /// <inheritdoc cref="Exception"/>
        public InvalidSpecException(string message)
            : base(message)
        {
        }

        /// <inheritdoc cref="Exception"/>
        protected InvalidSpecException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
