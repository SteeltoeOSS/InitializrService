// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Steeltoe.InitializrApi.Test.Utils
{
    public class MockProjectTemplateWebResponse : WebResponse
    {
        private Uri _uri;

        public MockProjectTemplateWebResponse(Uri uri)
        {
            _uri = uri;
        }

        public override Stream GetResponseStream()
        {
            switch (_uri.AbsolutePath)
            {
                case "/error":
                    return Error();
                case "/izr":
                    return IzrSteam();
                case "/no/such/file":
                    return NoSuchFile();
                case "/no/such/dir/":
                    return NoSuchDirectory();
                case "/stream/empty":
                    return EmptyStream();
                case "/stream/null":
                    return NullSteam();
                case "/zip":
                    return ZipSteam();
                default:
                    throw new UriFormatException($"Don't know how to create test stream: {_uri}");
            }
        }

        private Stream IzrSteam()
        {
            var buf = new MemoryStream();
            var archive = new ZipArchive(buf, ZipArchiveMode.Create, true);

            var queryParams = new Dictionary<string, string>()
            {
                { "constraints", "yes" },
                { "description", "project template description" },
                { "dotNetFrameworkRange", "" },
                { "dotNetTemplate", "" },
                { "language", "" },
                { "manifest", "yes" },
                { "metadata", "yes" },
                { "missingfile", "" },
                { "steeltoeVersionRange", "" },
            };
            if (_uri.Query.StartsWith('?'))
            {
                foreach (var nameValuePair in _uri.Query.Substring(1).Split('&'))
                {
                    var queryParam = nameValuePair.Split('=', 2);
                    queryParams[queryParam[0]] = queryParam[1];
                }
            }

            if (!queryParams["metadata"].Equals("no"))
            {
                var metadata = new StringBuilder();
                if (queryParams["metadata"].Equals("malformed"))
                {
                    metadata.Append("malformed yaml");
                }
                else
                {
                    metadata.Append("description: ").Append(queryParams["description"]).Append(Environment.NewLine);
                    if (!queryParams["constraints"].Equals("no"))
                    {
                        metadata.Append("constraints:").Append(Environment.NewLine);
                        metadata.Append($"  steeltoeVersionRange: {queryParams["steeltoeVersionRange"]}")
                            .Append(Environment.NewLine);
                        metadata.Append(
                                $"  dotNetFrameworkRange: {queryParams["dotNetFrameworkRange"]}")
                            .Append(Environment.NewLine);
                        metadata.Append($"  dotNetTemplate: {queryParams["dotNetTemplate"]}")
                            .Append(Environment.NewLine);
                        metadata.Append($"  language: {queryParams["language"]}").Append(Environment.NewLine);
                    }

                    // manifest
                    if (!queryParams["manifest"].Equals("no"))
                    {
                        metadata.Append("manifest:").Append(Environment.NewLine);
                        metadata.Append("- path: f1").Append(Environment.NewLine);
                        metadata.Append("- path: d1/").Append(Environment.NewLine);
                        metadata.Append("- path: r1").Append(Environment.NewLine);
                        metadata.Append("  rename: n1").Append(Environment.NewLine);
                    }
                }

                WriteEntryText(archive.CreateEntry(".IZR/metadata.yaml"), metadata.ToString());
            }

            if (!queryParams["missingfile"].Equals("yes"))
            {
                WriteEntryText(archive.CreateEntry("f1"), "my file f1");
            }

            archive.CreateEntry("d1/");
            WriteEntryText(archive.CreateEntry("r1"), "my file r1->n1");

            archive.Dispose();
            return buf;
        }

        private static void WriteEntryText(ZipArchiveEntry entry, string text)
        {
            using var stream = entry.Open();
            using var writer = new StreamWriter(stream);
            writer.Write(text);
        }

        private Stream ZipSteam()
        {
            var buf = new MemoryStream();
            var archive = new ZipArchive(buf, ZipArchiveMode.Create, true);
            archive.Dispose();
            return buf;
        }

        private Stream EmptyStream()
        {
            return new MemoryStream();
        }

        private Stream NullSteam()
        {
            return null;
        }

        private Stream NoSuchFile()
        {
            throw new WebException("URI not found", new FileNotFoundException(_uri.ToString()));
        }

        private Stream NoSuchDirectory()
        {
            throw new WebException("URI not found", new DirectoryNotFoundException(_uri.ToString()));
        }

        private Stream Error()
        {
            throw new Exception($"some error message");
        }
    }
}
