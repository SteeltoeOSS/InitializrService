using System.Threading.Tasks;
using FluentAssertions;
using Steeltoe.Initializr.WebApi.Server.Data;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Server.Test.Data
{
	public class LocalConfigurationRepositoryTest
	{
		[Fact]
		public async Task LocalGetConfiguration()
		{
			var configRepo = new LocalConfigurationRepository(null);
			var config = await configRepo.GetConfiguration();
			config.Should().NotBeNull();
		}
	}
}
