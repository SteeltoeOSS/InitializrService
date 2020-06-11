using FluentAssertions;
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Models.Metadata
{
	public class ConfigurationTest
	{
		[Fact]
		public void ObjectEquals()
		{
			var obj1 = new Configuration();
			var obj2 = new Configuration();
			obj1.Equals(obj2).Should().BeTrue();
		}

		[Fact]
		public void ObjectHashCode()
		{
			var obj1 = new Configuration();
			var obj2 = new Configuration();
			obj1.GetHashCode().Should().Be(obj2.GetHashCode());
		}
	}
}
