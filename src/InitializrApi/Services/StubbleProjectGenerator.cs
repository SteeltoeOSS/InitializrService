// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// An <see cref="IProjectGenerator"/> implementation using <a gref="https://github.com/StubbleOrg/Stubble">Stubble</a>, a Mustache template engine implemented in C#.
    /// </summary>
    public class StubbleProjectGenerator : IProjectGenerator
    {
        /// <inheritdoc/>
        public Task<Stream> GenerateProject(ProjectSpecification specification)
        {
            throw new NotImplementedException();
        }
    }
}
