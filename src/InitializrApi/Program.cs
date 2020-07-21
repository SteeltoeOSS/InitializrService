// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Steeltoe.InitializrApi
{
    /// <summary>
    /// The Steeltoe Initializr Api program.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        protected Program()
        {
        }

        /// <summary>
        /// Program entrypoint.
        /// </summary>
        public static int Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            return 0;
        }

        /// <summary>
        /// Create a generic host (see <a href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1"></a>.
        /// </summary>
        /// <param name="args">Command line args.</param>
        /// <returns>A generic host.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddConfigServer()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        /// <summary>
        /// Program "About" details, such as version.
        /// </summary>
        public class About
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="About"/> class.
            /// </summary>
            public About()
            {
                Name = typeof(Program).Namespace ?? "unknown";
                Vendor = "SteeltoeOSS/VMware";
                ProductUrl = "https://github.com/SteeltoeOSS/InitializrApi/";
                var versionAttr = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                if (versionAttr != null)
                {
                    var fields = versionAttr.InformationalVersion.Split('+');
                    Version = fields[0];
                    Commit = fields.Length > 1 ? fields[1] : "unknown";
                }
            }

            /// <summary>
            /// Gets the program name.
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Gets the program vendor.
            /// </summary>
            public string Vendor { get; }

            /// <summary>
            /// Gets the program product URL.
            /// </summary>
            public string ProductUrl { get; }

            /// <summary>
            /// Gets the program version.
            /// </summary>
            public string Version { get; }

            /// <summary>
            /// Gets the program build source control commit ID.
            /// </summary>
            public string Commit { get; }
        }
    }
}
