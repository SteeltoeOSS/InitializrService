using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Steeltoe.Initializr.WebApi.Services;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Services
{
	public class LocalConfigurationRepositoryTest
	{
		[Fact]
		public async Task ConfigurationShouldNotBeNull()
		{
			var configRepo = new LocalConfigurationRepository(new NullLoggerFactory());
			var config = await configRepo.GetConfiguration();
			config.Should().NotBeNull();
		}
	}
}
