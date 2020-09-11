// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of a project template manifest an an IZR template bundle.
    /// </summary>
    public sealed class ProjectTemplate
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the constraints.
        /// </summary>
        public ProjectSpecConstraints Constraints { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        public FileEntry[] Manifest { get; set; }

        /// <summary>
        /// Gets or sets the parameter expressions.
        /// </summary>
        public Parameter[] Parameters { get; set; }
    }
}
