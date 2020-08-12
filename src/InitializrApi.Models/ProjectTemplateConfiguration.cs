// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of project template configuration metadata used by InitializrApi.
    /// </summary>
    public class ProjectTemplateConfiguration
    {
        /// <summary>
        /// Gets or sets the project template URI.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the project template Steeltoe version range.
        /// </summary>
        public string SteeltoeVersionRange { get; set; }

        /// <summary>
        /// Gets or sets the project template DotNet version range.
        /// </summary>
        public string DotNetVersionRange { get; set; }

        /// <summary>
        /// Gets or sets the project template DotNet template type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the project template language.
        /// </summary>
        public string Language { get; set; }
    }
}
