// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model a file entry or a directory entry.
    /// A file entry must have a path that does not end in '/' and text that is not null.
    /// A directory entry must have a path that ends in '/' and text that is null.
    /// </summary>
    public sealed class FileEntry
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the file text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the name to be renamed.
        /// </summary>
        public string Rename { get; set; }

        /// <summary>
        /// Gets or sets the associated dependencies.
        /// </summary>
        public string Dependencies { get; set; }
    }
}
