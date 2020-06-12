using FluentAssertions;
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Models.Metadata
{
	public class ConfigurationTest
	{
		private readonly Configuration _obj1 = new Configuration();
		private readonly Configuration _obj2 = new Configuration();
		private readonly Configuration _obj3 = new Configuration();

		[Fact]
		public void BaseEquivalence()
		{
			_obj1.Equals(null).Should().BeFalse();
			_obj1.Equals(_obj2).Should().BeTrue();
		}

		[Fact]
		public void ReleasesEquivalence()
		{
			_obj1.Releases = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases = new Configuration.ItemList();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Releases.Default = "some default";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases.Default = "some default";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases.Default = "some default";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Releases.Type = "some type";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases.Type = "some type";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases.Type = "some type";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Releases.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases.Values = new Configuration.ItemList.Item[1];
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Releases.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases.Values[0] = new Configuration.ItemList.Item();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Releases.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases.Values[0].Id = "some item id";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Releases.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Releases.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Releases.Values[0].Name = "some item name";
			_obj3.Equals(_obj1).Should().BeTrue();
			_obj1.Equals(_obj2).Should().BeTrue();
		}

		[Fact]
		public void TargetsEquivalence()
		{
			_obj1.Targets = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets = new Configuration.ItemList();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Targets.Default = "some default";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets.Default = "some default";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets.Default = "some default";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Targets.Type = "some type";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets.Type = "some type";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets.Type = "some type";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Targets.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets.Values = new Configuration.ItemList.Item[1];
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Targets.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets.Values[0] = new Configuration.ItemList.Item();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Targets.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets.Values[0].Id = "some item id";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Targets.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Targets.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Targets.Values[0].Name = "some item name";
			_obj3.Equals(_obj1).Should().BeTrue();
		}

		[Fact]
		public void TemplatesEquivalence()
		{
			_obj1.Templates = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates = new Configuration.ItemList();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Templates.Default = "some default";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates.Default = "some default";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates.Default = "some default";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Templates.Type = "some type";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates.Type = "some type";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates.Type = "some type";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Templates.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates.Values = new Configuration.ItemList.Item[1];
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Templates.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates.Values[0] = new Configuration.ItemList.Item();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Templates.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates.Values[0].Id = "some item id";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Templates.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Templates.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Templates.Values[0].Name = "some item name";
		}

		[Fact]
		public void LanguagesEquivalence()
		{
			_obj1.Languages = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages = new Configuration.ItemList();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages = new Configuration.ItemList();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Languages.Default = "some default";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages.Default = "some default";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages.Default = "some default";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Languages.Type = "some type";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages.Type = "some type";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages.Type = "some type";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Languages.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages.Values = new Configuration.ItemList.Item[1];
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages.Values = new Configuration.ItemList.Item[1];
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Languages.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages.Values[0] = new Configuration.ItemList.Item();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages.Values[0] = new Configuration.ItemList.Item();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Languages.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages.Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages.Values[0].Id = "some item id";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Languages.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Languages.Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Languages.Values[0].Name = "some item name";
		}

		[Fact]
		public void DependenciesEquivalence()
		{
			_obj1.Dependencies = new Configuration.GroupedItemList();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies = new Configuration.GroupedItemList();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies = new Configuration.GroupedItemList();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Type = "some type";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Type = "some type";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Type = "some type";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values = new Configuration.GroupedItemList.Group[1];
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values = new Configuration.GroupedItemList.Group[1];
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values = new Configuration.GroupedItemList.Group[1];
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0] = new Configuration.GroupedItemList.Group();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0] = new Configuration.GroupedItemList.Group();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0] = new Configuration.GroupedItemList.Group();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0].Name = "some group name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0].Name = "some group name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0].Name = "some group name";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0].Values = new Configuration.GroupedItemList.Group.Item[1];
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0].Values = new Configuration.GroupedItemList.Group.Item[1];
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0].Values = new Configuration.GroupedItemList.Group.Item[1];
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0].Values[0] = new Configuration.GroupedItemList.Group.Item();
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0].Values[0] = new Configuration.GroupedItemList.Group.Item();
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0].Values[0] = new Configuration.GroupedItemList.Group.Item();
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0].Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0].Values[0].Id = "some item id";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0].Values[0].Id = "some item id";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0].Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0].Values[0].Name = "some item name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0].Values[0].Name = "some item name";
			_obj3.Equals(_obj1).Should().BeTrue();

			_obj1.Dependencies.Values[0].Values[0].Description = "some item name";
			_obj1.Equals(_obj2).Should().BeFalse();
			_obj2.Dependencies.Values[0].Values[0].Description = "some item name";
			_obj1.Equals(_obj2).Should().BeTrue();
			_obj3.Equals(_obj1).Should().BeFalse();
			_obj3.Dependencies.Values[0].Values[0].Description = "some item name";
			_obj3.Equals(_obj1).Should().BeTrue();
		}
	}
}
