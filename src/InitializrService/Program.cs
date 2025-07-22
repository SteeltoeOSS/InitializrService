// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Configuration.ConfigServer;
using Steeltoe.InitializrService.Models;
using Steeltoe.Logging.DynamicConsole;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Steeltoe.InitializrService
{
    /// <summary>
    /// The Steeltoe Initializr Api program.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static Program()
        {
            var versionAttr =
                typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var fields = versionAttr?.InformationalVersion.Split('+') ?? ["unknown"];

            if (fields.Length == 1)
            {
                fields = [fields[0], "unknown"];
            }

            About = new About
            {
                Name = typeof(Program).Namespace ?? "unknown",
                Vendor = "SteeltoeOSS/Broadcom",
                Url = "https://github.com/SteeltoeOSS/InitializrService/",
                Version = fields[0],
                Commit = fields[1],
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        protected Program()
        {
        }

        /// <summary>
        /// Gets or sets "About" details, such as version.
        /// </summary>
        public static About About { get; set; }

        /// <summary>
        /// Program entrypoint.
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create a generic host (see <a href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1"></a>.
        /// </summary>
        /// <param name="args">Command line args.</param>
        /// <returns>A generic host.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddConfigServer()
                .ConfigureLogging((_, builder) => builder.AddDynamicConsole())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
