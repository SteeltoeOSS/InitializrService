// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Initializr.WebApi.Services;

namespace Steeltoe.Initializr.WebApi.Controllers
{
    /// <summary>
    /// Project generation configuration metadata endpoint.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly IMetadataRepository _metadataRepository;

        /// <summary>
        /// Create a new ConfigurationController.
        /// </summary>
        /// <param name="metadataRepository">configuration repository</param>
        public MetadataController(IMetadataRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
        }

        /// <summary>
        /// Implements <code>GET</code>..
        /// </summary>
        /// <returns>Project generation configuration metadata</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var config = await _metadataRepository.GetConfiguration();
            return Ok(config);
        }
    }
}
