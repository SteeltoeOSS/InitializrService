// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Services;
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
        public class About : IAbout
        {
            /// <summary>
            /// Get the "about" details of this program.
            /// </summary>
            /// <returns>program about.</returns>
            public Models.About GetAbout()
            {
                var about = new Models.About();
                about.Name = typeof(Program).Namespace ?? "unknown";
                about.Vendor = "SteeltoeOSS/VMware";
                about.Url = "https://github.com/SteeltoeOSS/InitializrApi/";
                var versionAttr =
                    typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                if (versionAttr != null)
                {
                    var fields = versionAttr.InformationalVersion.Split('+');
                    about.Version = fields[0];
                    about.Commit = fields.Length > 1 ? fields[1] : "unknown";
                }

                return about;
            }
        }
    }
}
