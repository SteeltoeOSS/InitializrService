// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.InitializrService.Models;
using Steeltoe.InitializrService.Services;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Steeltoe.InitializrService.Generators
{
    /// <summary>
    /// a A project generator that uses a NetCoreToolService.
    /// </summary>
    public class NetCoreToolProjectGenerator : InitializrServiceBase, IProjectGenerator
    {
        private static readonly HttpClient Client = new ();

        private readonly string _netCoreToolServiceUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetCoreToolProjectGenerator"/> class.
        /// </summary>
        /// <param name="options">Injected options.</param>
        /// <param name="logger">Injected logger.</param>
        public NetCoreToolProjectGenerator(
            IOptions<InitializrServiceOptions> options,
            ILogger<NetCoreToolProjectGenerator> logger)
            : base(logger)
        {
            _netCoreToolServiceUri = options.Value.NetCoreToolServiceUri;
        }

        /// <inheritdoc/>
        public async Task<byte[]> GenerateProjectArchive(ProjectSpec spec)
        {
            var options = new StringBuilder();
            options.Append("output=").Append(spec.Namespace)
                .Append(",description=").Append(spec.Description)
                .Append(",steeltoe=").Append(spec.SteeltoeVersion)
                .Append(",framework=").Append(spec.DotNetFramework)
                .Append(",language=").Append(spec.Language);
            var projectUrl = new StringBuilder();
            projectUrl.Append(_netCoreToolServiceUri)
                .Append("/new/steeltoe-webapi")
                .Append('?')
                .Append("packaging=").Append(spec.Packaging)
                .Append('&')
                .Append("options=")
                .Append(HttpUtility.UrlEncode(options.ToString()));
            if (spec.Dependencies is not null)
            {
                foreach (var dependency in spec.Dependencies.Split(','))
                {
                    projectUrl.Append(',').Append(dependency);
                }
            }

            projectUrl.Append(",no-restore");

            var response = await Client.GetAsync(projectUrl.ToString());
            var buffer = new MemoryStream();
            await response.Content.CopyToAsync(buffer);
            var bytes = buffer.GetBuffer();
            return bytes;
        }
    }
}
