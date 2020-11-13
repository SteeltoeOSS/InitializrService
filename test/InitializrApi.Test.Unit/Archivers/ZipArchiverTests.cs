// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.IO.Compression;
using FluentAssertions;
using Steeltoe.InitializrApi.Archivers;
using Steeltoe.InitializrApi.Models;
using Xunit;

namespace Steeltoe.InitializrApi.Test.Unit.Archivers
{
    public class ZipArchiverTests
    {
        /* ----------------------------------------------------------------- *
         * positive tests                                                    *
         * ----------------------------------------------------------------- */

        [Fact]
        public void ToStream_Should_Create_Zip_Archive()
        {
            // Arrange
            var archiver = new ZipArchiver();
            var files = new FileEntry[0];

            // Act
            var buf = archiver.ToBytes(files);

            // Assert
            new ZipArchive(new MemoryStream(buf)).Should().BeOfType<ZipArchive>();
        }

        [Fact]
        public void ToStream_Should_Archive_File_Contents()
        {
            // Arrange
            var archiver = new ZipArchiver();
            var files = new[]
            {
                new FileEntry { Path = "d1/f1", Text = "f1 stuff" },
            };


            // Act
            var buf = archiver.ToBytes(files);

            // Assert
            var zip = new ZipArchive(new MemoryStream(buf));
            using var entries = zip.Entries.GetEnumerator();
            entries.MoveNext().Should().BeTrue();
            var entry = entries.Current;
            Assert.NotNull(entry);
            entry.Name.Should().Be("f1");
            entry.FullName.Should().Be("d1/f1");
            using var reader = new StreamReader(entry.Open());
            reader.ReadToEnd().Should().Be("f1 stuff");
            entries.MoveNext().Should().BeFalse();
        }

        [Fact]
        public void ToStream_Should_Archive_Directories()
        {
            // Arrange
            var archiver = new ZipArchiver();
            var files = new[]
            {
                new FileEntry { Path = "d1/d2" },
            };

            // Act
            var buf = archiver.ToBytes(files);

            // Assert
            var zip = new ZipArchive(new MemoryStream(buf));
            using var entries = zip.Entries.GetEnumerator();
            entries.MoveNext().Should().BeTrue();
            var entry = entries.Current;
            Assert.NotNull(entry);
            entry.Name.Should().Be("d2");
            entry.FullName.Should().Be("d1/d2");
            using var reader = new StreamReader(entry.Open());
            reader.ReadToEnd().Should().BeEmpty();
            entries.MoveNext().Should().BeFalse();
        }

        [Fact]
        public void GetPackaging_Should_Be_application_zip()
        {
            // Arrange
            var archiver = new ZipArchiver();

            // Act
            var packaging = archiver.GetPackaging();

            // Assert
            packaging.Should().Be("zip");
        }

        [Fact]
        public void GetFileExtension_Should_Be_zip()
        {
            // Arrange
            var archiver = new ZipArchiver();

            // Act
            var ext = archiver.GetFileExtension();

            // Assert
            ext.Should().Be(".zip");
        }

        /* ----------------------------------------------------------------- *
         * negative tests                                                    *
         * ----------------------------------------------------------------- */
    }
}
