using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Steeltoe.Initializr.WebApi.Server.Test.Utils
{
	public class HttpClientBuilder
	{
		private static readonly IHost _Host;

		static HttpClientBuilder()
		{
			var hostBuilder = new HostBuilder().ConfigureWebHost(webHost =>
			{
				webHost.UseTestServer();
				webHost.UseStartup<Startup>();
			});
			_Host = hostBuilder.Start();
		}

		public HttpClient Build()
		{
			return _Host.GetTestClient();
		}
	}
}
