// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Steeltoe.Initializr.WebApi.Controllers;
using Steeltoe.Initializr.WebApi.Services;
using Steeltoe.Initializr.WebApi.Test.TestUtils;
using Xunit;

namespace Steeltoe.Initializr.WebApi.Test
{
    public class StartupTest
    {
        [Fact]
        public void ConfigurationControllerRegistered()
        {
            // Arrange
            IServiceCollection svcs = new ServiceCollection();
            var cfgFile = new TempFile();
            var cfg = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"MetadataRepository:Uri", cfgFile.Path},
                })
                .Build();
            var tgt = new Startup(cfg);
            svcs.AddSingleton<ILogger<IConfigurationRepository>>(new NullLogger<IConfigurationRepository>());

            // Act
            tgt.ConfigureServices(svcs);
            svcs.AddTransient<ConfigurationController>();

            // Assert
            var controller = svcs.BuildServiceProvider().GetService<ConfigurationController>();
            controller.Should().NotBeNull();
        }

        [Fact]
        public void ProjectControllerRegisterer()
        {
            // Arrange
            IServiceCollection svcs = new ServiceCollection();
            var cfg = new ConfigurationBuilder().Build();
            var tgt = new Startup(cfg);

            // Act
            tgt.ConfigureServices(svcs);
            svcs.AddTransient<ProjectController>();

            // Assert
            var controller = svcs.BuildServiceProvider().GetService<ProjectController>();
            controller.Should().NotBeNull();
        }
    }
}
