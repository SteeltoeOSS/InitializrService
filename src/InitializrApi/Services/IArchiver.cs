// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;
using System.Collections.Generic;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for archiver implementations.
    /// </summary>
    public interface IArchiver
    {
        /// <summary>
        /// Returns the archive as a byte array.
        /// </summary>
        /// <param name="fileEntries">File entries to be archived.</param>
        /// <returns>A new stream containing the archive.</returns>
        byte[] ToBytes(IEnumerable<FileEntry> fileEntries);

        /// <summary>
        /// Gets the packaging for the archive format.
        /// </summary>
        /// <returns>The packaging.</returns>
        string GetPackaging();

        /// <summary>
        /// Gets the file extension for the archive.
        /// </summary>
        /// <returns>The file extension.</returns>
        string GetFileExtension();
    }
}
