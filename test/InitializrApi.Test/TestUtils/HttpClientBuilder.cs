// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Steeltoe.InitializrApi.Test.TestUtils
{
    public class HttpClientBuilder
    {
        private static readonly IHost Host;

        static HttpClientBuilder()
        {
            var hostBuilder = new HostBuilder().ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer();
                webHost.UseStartup<Startup>();
            });
            Host = hostBuilder.Start();
        }

        public HttpClient Build()
        {
            return Host.GetTestClient();
        }
    }
}
