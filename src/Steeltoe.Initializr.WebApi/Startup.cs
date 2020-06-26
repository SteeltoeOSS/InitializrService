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
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Steeltoe.Initializr.WebApi.Services;
using System.Reflection;

namespace Steeltoe.Initializr.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.ConfigureConfigServerClientOptions(Configuration);
            services.Configure<Configuration>(Configuration);
            services.AddSingleton<IMetadataRepository, ConfigServerMetadataRepository>();
            services.AddSingleton<IProjectGenerator, DummyProjectGenerator>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            var versionAttr = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var name = typeof(Program).Namespace ?? "unknown";
            var version = versionAttr?.InformationalVersion ?? "unknown";
            logger.LogInformation($"{name}, version {version}");

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
