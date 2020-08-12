// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.InitializrApi.Configuration;
using Steeltoe.InitializrApi.Generators;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System.Diagnostics.CodeAnalysis;

namespace Steeltoe.InitializrApi
{
    /// <summary>
    /// The Steeltoe Initializr Api dependency injection setup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Injected configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Called by the runtime.  Adds <see cref="IConfigurationRepository"/> and <see cref="IProjectGenerator"/> services.
        /// </summary>
        /// <param name="services">Injected services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.ConfigureConfigServerClientOptions(Configuration);
            services.Configure<InitializrApiConfiguration>(Configuration);
            services.AddSingleton<IConfigurationRepository, ConfigServerConfigurationRepository>();
            services.AddSingleton<IProjectGenerator, StubbleProjectGenerator>();
            services.AddControllers();
        }

        /// <summary>
        /// Called by the runtime.  Sets up HTTP request pipeline.
        /// </summary>
        /// <param name="app">Injected IApplicationBuilder.</param>
        /// <param name="env">Injected IWebHostEnvironment.</param>
        /// <param name="logger">Injected ILogger.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            var about = Program.About;
            logger.LogInformation($"{about.Name}, version {about.Version} [{about.Commit}]");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
