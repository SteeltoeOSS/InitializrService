// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Steeltoe.Initializr.WebApi.Models;

namespace Steeltoe.Initializr.WebApi.Services
{
    /// <summary>
    /// A project generator that uses the Mustache templating framework.
    /// </summary>
    public class DummyProjectGenerator : IProjectGenerator
    {
        public Task<Stream> GenerateProject(ProjectSpecification specification)
        {
            var bytes = new UnicodeEncoding().GetBytes("DummyProject");
            var stream = new MemoryStream(bytes.Length);
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            var result = new TaskCompletionSource<Stream>();
            result.SetResult(stream);
            return result.Task;
        }
    }
}
