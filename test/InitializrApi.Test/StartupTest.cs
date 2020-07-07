// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Steeltoe.InitializrApi.Controllers;
using Steeltoe.InitializrApi.Services;
using Xunit;

namespace Steeltoe.InitializrApi.Test
{
    public class StartupTest
    {
        [Fact]
        public void ConfigurationControllerRegistered()
        {
            // Arrange
            var cfg = new ConfigurationBuilder().Build();
            var startup = new Startup(cfg);
            IServiceCollection svcs = new ServiceCollection();
            svcs.AddSingleton<ILogger<IConfigurationRepository>>(new NullLogger<IConfigurationRepository>());
            startup.ConfigureServices(svcs);

            // Act
            svcs.AddTransient<ConfigurationController>();

            // Assert
            var controller = svcs.BuildServiceProvider().GetService<ConfigurationController>();
            controller.Should().NotBeNull();
        }

        [Fact]
        public void ProjectControllerRegisterer()
        {
            // Arrange
            var cfg = new ConfigurationBuilder().Build();
            var startup = new Startup(cfg);
            IServiceCollection svcs = new ServiceCollection();
            startup.ConfigureServices(svcs);

            // Act
            svcs.AddTransient<ProjectController>();

            // Assert
            var controller = svcs.BuildServiceProvider().GetService<ProjectController>();
            controller.Should().NotBeNull();
        }
    }
}
