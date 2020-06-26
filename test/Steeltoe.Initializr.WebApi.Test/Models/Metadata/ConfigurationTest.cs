using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using Steeltoe.Initializr.WebApi.Models.Metadata;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Models.Metadata
{
    public class ConfigurationTest
    {
        private Configuration Cfg1 { get; } = new Configuration();

        private JToken Cfg1Json
        {
            get => JToken.FromObject(Cfg1);
        }

        private Configuration Cfg2 { get; } = new Configuration();

        private JToken Cfg2Json
        {
            get => JToken.FromObject(Cfg2);
        }

        private Configuration Cfg3 { get; } = new Configuration();

        private JToken Cfg3Json
        {
            get => JToken.FromObject(Cfg3);
        }

        [Fact]
        public void BaseEquivalence()
        {
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
        }


        [Fact]
        public void ReleasesEquivalence()
        {
            Cfg1.Releases = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Releases.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Releases.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Releases.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Releases.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Releases.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Releases.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Releases.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Releases.Values[0].Name = "some item name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
        }

        [Fact]
        public void TargetsEquivalence()
        {
            Cfg1.Targets = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Targets.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Targets.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Targets.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Targets.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Targets.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Targets.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Targets.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Targets.Values[0].Name = "some item name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
        }

        [Fact]
        public void TemplatesEquivalence()
        {
            Cfg1.Templates = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Templates.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Templates.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Templates.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Templates.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Templates.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Templates.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Templates.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Templates.Values[0].Name = "some item name";
        }

        [Fact]
        public void LanguagesEquivalence()
        {
            Cfg1.Languages = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Languages.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Languages.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Languages.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Languages.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Languages.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Languages.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Languages.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Languages.Values[0].Name = "some item name";
        }

        [Fact]
        public void DependenciesEquivalence()
        {
            Cfg1.Dependencies = new Configuration.GroupList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies = new Configuration.GroupList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies = new Configuration.GroupList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values = new Configuration.Group[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values = new Configuration.Group[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values = new Configuration.Group[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0] = new Configuration.Group();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0] = new Configuration.Group();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0] = new Configuration.Group();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0].Name = "some group name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0].Name = "some group name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0].Name = "some group name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0].Values = new Configuration.GroupItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0].Values = new Configuration.GroupItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0].Values = new Configuration.GroupItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0].Values[0] = new Configuration.GroupItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0].Values[0] = new Configuration.GroupItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0].Values[0] = new Configuration.GroupItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0].Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0].Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0].Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0].Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0].Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0].Values[0].Name = "some item name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Dependencies.Values[0].Values[0].Description = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Dependencies.Values[0].Values[0].Description = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Dependencies.Values[0].Values[0].Description = "some item name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
        }
    }
}
