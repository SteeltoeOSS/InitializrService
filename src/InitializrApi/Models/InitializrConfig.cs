// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of an InitializrApi server configuration.
    /// </summary>
    public sealed class InitializrConfig
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the project metadata.
        /// </summary>
        public ProjectMetadata ProjectMetadata { get; set; }

        /// <summary>
        /// Gets or sets the project templates configuration.
        /// </summary>
        public ProjectTemplateConfiguration[] ProjectTemplates { get; set; }
    }
}
