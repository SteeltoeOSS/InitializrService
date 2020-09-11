// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;

namespace Steeltoe.InitializrApi.Services
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
        /// <returns>The generated project, or null if not able to generate project per spec.</returns>
        Project GenerateProject(ProjectSpec spec);
    }
}
