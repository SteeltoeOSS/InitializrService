// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;
using Steeltoe.InitializrApi.Services;
using System.Collections.Generic;

namespace Steeltoe.InitializrApi.Archivers
{
    /// <summary>
    /// An in-memory <see cref="IArchiverRegistry"/> implementation.
    /// </summary>
    public class ArchiverRegistry : InitializrApiServiceBase, IArchiverRegistry
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly Dictionary<string, IArchiver> _archivers = new Dictionary<string, IArchiver>();

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiverRegistry"/> class.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        public ArchiverRegistry(ILogger<ArchiverRegistry> logger)
            : base(logger)
        {
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <inheritdoc />
        public void Initialize()
        {
            Logger.LogInformation("Initializing archiver registry.");

            _archivers.Clear();
            Register(new ZipArchiver());
        }

        /// <inheritdoc />
        public void Register(IArchiver archiver)
        {
            Logger.LogInformation(
                "Registering archiver: {Packaging} -> {Archiver}",
                archiver.GetPackaging(),
                archiver.GetType());
            _archivers.Add(archiver.GetPackaging(), archiver);
        }

        /// <inheritdoc />
        public IArchiver Lookup(string packaging)
        {
            _archivers.TryGetValue(packaging, out var archiver);
            return archiver;
        }
    }
}
