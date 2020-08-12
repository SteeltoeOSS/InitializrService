// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;
using System.IO;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for a project generator implementations.
    /// </summary>
    public interface IProjectGenerator
    {
        /// <summary>
        /// Generates a project bundle as a ZIP archive byte stream.
        /// </summary>
        /// <param name="specification">Project specification.</param>
        /// <returns>A task containing generated project bundle.</returns>
        public Task<Stream> GenerateProject(ProjectSpecification specification);
    }
}
