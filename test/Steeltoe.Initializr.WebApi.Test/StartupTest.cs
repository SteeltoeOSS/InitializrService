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
        public void MetadataControllerRegistered()
        {
            // Arrange
            IServiceCollection svcs = new ServiceCollection();
            var metadata = new TempFile();
            var cfg = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"MetadataRepository:Uri", metadata.Path},
                })
                .Build();
            var tgt = new Startup(cfg);
            svcs.AddSingleton<ILogger<IMetadataRepository>>(new NullLogger<IMetadataRepository>());

            // Act
            tgt.ConfigureServices(svcs);
            svcs.AddTransient<MetadataController>();

            // Assert
            var controller = svcs.BuildServiceProvider().GetService<MetadataController>();
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
