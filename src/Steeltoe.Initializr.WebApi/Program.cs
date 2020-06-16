using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Steeltoe.Initializr.WebApi
{
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
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}
