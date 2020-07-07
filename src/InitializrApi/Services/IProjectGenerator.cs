// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Threading.Tasks;
using Steeltoe.InitializrApi.Models;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for project generator implementations.
    /// </summary>
    public interface IProjectGenerator
    {
        /// <summary>
        /// Generates a project as a byte stream.
        /// </summary>
        /// <param name="specification">Project configuration</param>
        /// <returns>project bundle  byte stream</returns>
        public Task<Stream> GenerateProject(ProjectSpecification specification);
    }
}
