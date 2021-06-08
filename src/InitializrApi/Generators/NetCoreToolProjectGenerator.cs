// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;

namespace Steeltoe.InitializrApi.Generators
{
    /// <summary>
    /// a A project generator that uses a NetCoreToolService.
    /// </summary>
    public class NetCoreToolProjectGenerator : IProjectGenerator
    {
        /// <inheritdoc/>
        public Project GenerateProject(ProjectSpec spec)
        {
            throw new System.NotImplementedException();
        }
    }
}
