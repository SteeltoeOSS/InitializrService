// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace Steeltoe.Initializr.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        protected Program()
        {
        }

        public static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.Error.WriteLine("too many args");
                return 1;
            }

            CreateHostBuilder(args).Build().Run();
            return 0;
        }

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

            public About()
            {
                Name = typeof(Program).Namespace ?? "unknown";
                Vendor = "SteeltoeOSS/VMware";
                ProductUrl = "https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.WebApi/";
                var versionAttr = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                if (versionAttr != null)
                {
                    var fields = versionAttr.InformationalVersion.Split('+');
                    Version = fields[0];
                    Commit = fields[1];
                }
            }
        }
    }
}
