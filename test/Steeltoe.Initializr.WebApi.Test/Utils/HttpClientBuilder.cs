using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Steeltoe.Initializr.WebApi.Test.Utils
{
	public class HttpClientBuilder
	{
		private static readonly IHost Host;

		static HttpClientBuilder()
		{
			var hostBuilder = new HostBuilder().ConfigureWebHost(webHost =>
			{
				webHost.UseTestServer();
				webHost.UseStartup<Startup>();
			});
			Host = hostBuilder.Start();
		}

		public HttpClient Build()
		{
			return Host.GetTestClient();
		}
	}
}
