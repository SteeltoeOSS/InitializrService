// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;
using Steeltoe.InitializrApi.Services;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

namespace Steeltoe.InitializrApi.Archivers
{
    /// <summary>
    /// An <see cref="IArchiver"/> implementation using the ZIP archive file format.
    /// </summary>
    public class ZipArchiver : IArchiver
    {
        /* ----------------------------------------------------------------- *
         * constants                                                         *
         * ----------------------------------------------------------------- */

        private const string Packaging = "zip";

        private const string FileExtension = ".zip";

        /* ----------------------------------------------------------------- *
         * Fix UNIX permissions in Zip archive extraction                    *
         *                                             Owner                 *
         *                                                 Group             *
         *                                                     Other         *
         *                                             r w r   r             *
         * ----------------------------------------------------------------- */
        private const int UnixFilePermissions = 0b_0000_0001_1010_0100_0000_0000_0000_0000;
        private const int UnixDirectoryPermissions = 0b_0000_0001_1110_1101_0000_0000_0000_0000;

        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly CompressionLevel _compression;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipArchiver"/> class.
        /// </summary>
        /// <param name="compression">Compression level default <see cref="CompressionLevel.Fastest"/>.</param>
        public ZipArchiver(CompressionLevel compression = CompressionLevel.Fastest)
        {
            _compression = compression;
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc/>
        public byte[] ToBytes(IEnumerable<FileEntry> fileEntries)
        {
            var buffer = new MemoryStream();
            var archive = new ZipArchive(buffer, ZipArchiveMode.Create, true);
            foreach (var fileEntry in fileEntries)
            {
                var zipEntry = archive.CreateEntry(fileEntry.Path, _compression);
                if (fileEntry.Text == null)
                {
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        zipEntry.ExternalAttributes = UnixDirectoryPermissions;
                    }

                    continue;
                }

                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    zipEntry.ExternalAttributes = UnixFilePermissions;
                }

                using var textStream = new MemoryStream(Encoding.UTF8.GetBytes(fileEntry.Text));
                using var zipStream = zipEntry.Open();
                textStream.CopyTo(zipStream);
            }

            archive.Dispose();
            buffer.Seek(0, SeekOrigin.Begin);
            return buffer.ToArray();
        }

        /// <inheritdoc/>
        public string GetPackaging()
        {
            return Packaging;
        }

        /// <inheritdoc/>
        public string GetFileExtension()
        {
            return FileExtension;
        }
    }
}
