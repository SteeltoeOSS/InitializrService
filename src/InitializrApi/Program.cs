// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Models;
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
        static Program()
        {
            var versionAttr =
                typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var fields = versionAttr?.InformationalVersion.Split('+');
            if (fields is null)
            {
                fields = new[] { "unknown" };
            }

            if (fields.Length == 1)
            {
                fields = new[] { fields[0], "unknown" };
            }

            About = new About
            {
                Name = typeof(Program).Namespace ?? "unknown",
                Vendor = "SteeltoeOSS/VMware",
                Url = "https://github.com/SteeltoeOSS/InitializrApi/",
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
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<IInitializrConfigService>().Initialize();
            host.Services.GetRequiredService<IProjectTemplateRegistry>().Initialize();
            host.Services.GetRequiredService<IArchiverRegistry>().Initialize();
            host.Run();
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
    }
}
