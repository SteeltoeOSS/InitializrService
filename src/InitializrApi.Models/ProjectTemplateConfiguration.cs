// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of project template configuration.
    /// </summary>
    public sealed class ProjectTemplateConfiguration
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the project template URI.
        /// </summary>
        public Uri Uri { get; set; }
    }
}
