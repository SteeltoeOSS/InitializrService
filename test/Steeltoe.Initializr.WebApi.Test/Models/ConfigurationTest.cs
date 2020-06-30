// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using Steeltoe.Initializr.WebApi.Models;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test.Models
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
        public void NameEquivalence()
        {
            Cfg1.Name = new Configuration.Text();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Name = new Configuration.Text();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Name = new Configuration.Text();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Name.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Name.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Name.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Name.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Name.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Name.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
        }

        [Fact]
        public void DescriptionEquivalence()
        {
            Cfg1.Description = new Configuration.Text();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Description = new Configuration.Text();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Description = new Configuration.Text();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Description.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Description.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Description.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.Description.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.Description.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.Description.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
        }

        [Fact]
        public void AboutEquivalence()
        {
            Cfg1.About = new Configuration.Product();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.About = new Configuration.Product();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.About = new Configuration.Product();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.About.Name = "some name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.About.Name = "some name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.About.Name = "some name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.About.Vendor = "some vendor";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.About.Vendor = "some vendor";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.About.Vendor = "some vendor";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.About.Url = "some url";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.About.Url = "some url";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.About.Url = "some url";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.About.Version = "some version";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.About.Version = "some version";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.About.Version = "some version";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.About.Commit = "some commit";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.About.Commit = "some commit";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.About.Commit = "some commit";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
        }

        [Fact]
        public void ReleasesEquivalence()
        {
            Cfg1.SteeltoeRelease = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.SteeltoeRelease.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.SteeltoeRelease.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.SteeltoeRelease.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.SteeltoeRelease.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.SteeltoeRelease.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.SteeltoeRelease.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.SteeltoeRelease.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.SteeltoeRelease.Values[0].Name = "some item name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
        }

        [Fact]
        public void TargetsEquivalence()
        {
            Cfg1.DotnetFrameworkTarget = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetFrameworkTarget.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetFrameworkTarget.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetFrameworkTarget.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetFrameworkTarget.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetFrameworkTarget.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetFrameworkTarget.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetFrameworkTarget.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetFrameworkTarget.Values[0].Name = "some item name";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
        }

        [Fact]
        public void TemplatesEquivalence()
        {
            Cfg1.DotnetTemplate = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetTemplate.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetTemplate.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetTemplate.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetTemplate.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetTemplate.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetTemplate.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetTemplate.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetTemplate.Values[0].Name = "some item name";
        }

        [Fact]
        public void LanguagesEquivalence()
        {
            Cfg1.DotnetLanguage = new Configuration.SingleSelectList();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage = new Configuration.SingleSelectList();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage = new Configuration.SingleSelectList();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetLanguage.Default = "some default";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage.Default = "some default";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage.Default = "some default";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetLanguage.Type = "some type";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage.Type = "some type";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage.Type = "some type";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetLanguage.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage.Values = new Configuration.SelectItem[1];
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage.Values = new Configuration.SelectItem[1];
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetLanguage.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage.Values[0] = new Configuration.SelectItem();
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage.Values[0] = new Configuration.SelectItem();
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetLanguage.Values[0].Id = "some item id";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage.Values[0].Id = "some item id";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage.Values[0].Id = "some item id";
            Cfg3Json.Should().BeEquivalentTo(Cfg1Json);
            Cfg1.DotnetLanguage.Values[0].Name = "some item name";
            Cfg1Json.Should().NotBeEquivalentTo(Cfg2Json);
            Cfg2.DotnetLanguage.Values[0].Name = "some item name";
            Cfg1Json.Should().BeEquivalentTo(Cfg2Json);
            Cfg3Json.Should().NotBeEquivalentTo(Cfg1Json);
            Cfg3.DotnetLanguage.Values[0].Name = "some item name";
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
