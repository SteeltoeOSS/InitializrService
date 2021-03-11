// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of a project.
    /// </summary>
    public sealed class Project
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the spec used to create the project.
        /// </summary>
        public ProjectSpec Spec { get; set; }

        /// <summary>
        /// Gets or sets the file entries.
        /// </summary>
        public List<FileEntry> FileEntries { get; set; } = new List<FileEntry>();
    }
}
