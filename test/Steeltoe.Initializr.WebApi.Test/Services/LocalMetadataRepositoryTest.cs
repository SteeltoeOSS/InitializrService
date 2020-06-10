using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Steeltoe.Initializr.WebApi.Services;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Services
{
	public class LocalMetadataRepositoryTest
	{
		[Fact]
		public async Task ConfigurationShouldNotBeNull()
		{
			var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			var configRepo = new LocalMetadataRepository(new NullLoggerFactory(), settings);
			var config = await configRepo.GetConfiguration();
			config.Should().NotBeNull();
		}
	}
}
