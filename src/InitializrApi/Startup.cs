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
using Steeltoe.InitializrApi.Archivers;
using Steeltoe.InitializrApi.Configuration;
using Steeltoe.InitializrApi.Generators;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using Steeltoe.InitializrApi.Templates;
using System.Diagnostics.CodeAnalysis;

namespace Steeltoe.InitializrApi
{
    /// <summary>
    /// The Steeltoe Initializr Api dependency injection setup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private string _corsOrigin;

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
        /// Called by the runtime.  Adds <see cref="IInitializrConfigService"/> and <see cref="IProjectGenerator"/> services.
        /// </summary>
        /// <param name="services">Injected services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<InitializrOptions>(Configuration.GetSection(InitializrOptions.Initializr));
            var initializrOptions = Configuration.GetSection(InitializrOptions.Initializr).Get<InitializrOptions>();
            if (initializrOptions?.ConfigurationPath is null)
            {
                services.ConfigureConfigServerClientOptions(Configuration);
                services.Configure<InitializrConfig>(Configuration);
                services.AddSingleton<IInitializrConfigService, InitializrConfigService>();
            }
            else
            {
                services.AddSingleton<IInitializrConfigService, InitializrConfigFile>();
            }

            if (!(initializrOptions?.CorsOrigin is null))
            {
                _corsOrigin = "ConfiguredOrigins";
                services.AddCors(options =>
                {
                    options.AddPolicy(
                        name: _corsOrigin,
                        builder => { builder.WithOrigins(initializrOptions.CorsOrigin); });
                });
            }

            services.AddResponseCompression();
            services.AddSingleton<IProjectTemplateRegistry, ProjectTemplateRegistry>();
            services.AddSingleton<IArchiverRegistry, ArchiverRegistry>();
            services.AddTransient<IProjectGenerator, StubbleProjectGenerator>();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
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
            logger.LogInformation("{Program}, version {Version} [{Commit}]", about.Name, about.Version, about.Commit);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();
            if (!(_corsOrigin is null))
            {
                app.UseCors(_corsOrigin);
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
