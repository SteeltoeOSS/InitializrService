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
using Steeltoe.InitializrApi.Config;
using Steeltoe.InitializrApi.Configuration;
using Steeltoe.InitializrApi.Generators;
using Steeltoe.InitializrApi.Services;
using Steeltoe.Management.Endpoint;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Steeltoe.InitializrApi
{
    /// <summary>
    /// The Steeltoe Initializr Api dependency injection setup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private string _netCoreToolServiceUri;

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
        /// Called by the runtime.  Adds <see cref="IUiConfigService"/> and <see cref="IProjectGenerator"/> services.
        /// </summary>
        /// <param name="services">Injected services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<InitializrApiOptions>(Configuration.GetSection(InitializrApiOptions.InitializrApi));
            InitializrApiOptions options;
            try
            {
                options = Configuration.GetSection(InitializrApiOptions.InitializrApi).Get<InitializrApiOptions>();
            }
            catch (Exception e)
            {
                if (e.InnerException is KeyNotFoundException)
                {
                    throw new ArgumentException("InitializrApi configuration is missing or errant");
                }

                throw;
            }

            _netCoreToolServiceUri = options?.NetCoreToolServiceUri;
            if (options?.UiConfigPath is null)
            {
                services.ConfigureConfigServerClientOptions(Configuration);
                services.Configure<UiConfig>(Configuration);
                services.AddSingleton<IUiConfigService, UiConfigService>();
            }
            else
            {
                services.AddSingleton<IUiConfigService, UiConfigFile>();
            }

            services.AddResponseCompression();
            services.AddTransient<IProjectGenerator, NetCoreToolProjectGenerator>();
            services.AddAllActuators();
            services.AddControllers().AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
                jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
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
            logger.LogInformation("Net Core Tool Service URI: {NetCoreToolService}", _netCoreToolServiceUri);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAllActuators();
                endpoints.MapControllers();
            });
        }
    }
}
