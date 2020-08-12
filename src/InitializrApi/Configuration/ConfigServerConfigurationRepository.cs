// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Options;
using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System.Threading.Tasks;

namespace Steeltoe.InitializrApi.Configuration
{
    /// <summary>
    /// An <see cref="IConfigurationRepository"/> using a <a href="https://cloud.spring.io/spring-cloud-config/reference/html/#_spring_cloud_config_server">Spring Cloud Config Server</a> backend.
    /// </summary>
    public class ConfigServerConfigurationRepository : IConfigurationRepository
    {
        private readonly InitializrApiConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationRepository"/> class.
        /// </summary>
        /// <param name="configuration">Injected configuration from Config Server.</param>
        public ConfigServerConfigurationRepository(IOptions<InitializrApiConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        /// <inheritdoc/>
        public Task<InitializrApiConfiguration> GetConfiguration()
        {
            var result = new TaskCompletionSource<InitializrApiConfiguration>();
            result.SetResult(_configuration);
            return result.Task;
        }
    }
}
