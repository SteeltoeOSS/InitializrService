using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Initializr.WebApi.MetadataRepository;
using Steeltoe.Initializr.WebApi.Services;

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
			var options = new MetadataRepositoryOptions();
			services.Configure<MetadataRepositoryOptions>(
				Configuration.GetSection(MetadataRepositoryOptions.MetatdataRepository));
			services.AddSingleton<IMetadataRepository, LocalMetadataRepository>();
			services.AddSingleton<IProjectGenerator, DummyProjectGenerator>();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
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
