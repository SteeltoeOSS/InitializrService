// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Steeltoe.InitializrService.Models
{
    /// <summary>
    /// A model of the project generation spec.
    /// </summary>
    public sealed class ProjectSpec
    {
        /* ----------------------------------------------------------------- *
         * fields                                                             *
         * ----------------------------------------------------------------- */

        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the project namespace.
        /// </summary>
        public string Namespace
        {
            get => field ?? Name;
            set;
        }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Steeltoe version.
        /// </summary>
        public string SteeltoeVersion { get; set; }

        /// <summary>
        /// Gets or sets the DotNet framework ID.
        /// </summary>
        public string DotNetFramework { get; set; }

        /// <summary>
        /// Gets or sets the programming language ID.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the project archive mime type.
        /// </summary>
        public string Packaging { get; set; }

        /// <summary>
        /// Gets or sets the project dependencies.
        /// </summary>
        public string Dependencies { get; set; }

        /// <summary>
        /// Returns a user-friendly representation of the current object.
        /// </summary>
        /// <returns>Returns a user-friendly string that represents the current object.</returns>
        public override string ToString()
        {
            var buf = new StringBuilder();
            var delimiter = string.Empty;
            buf.Append('[');
            if (Name != null)
            {
                buf.Append(delimiter).Append("name=").Append(Name);
                delimiter = ",";
            }

            if (Namespace != null)
            {
                buf.Append(delimiter).Append("namespace=").Append(Namespace);
                delimiter = ",";
            }

            if (Description != null)
            {
                buf.Append(delimiter).Append("description=").Append(Description);
                delimiter = ",";
            }

            if (SteeltoeVersion != null)
            {
                buf.Append(delimiter).Append("steeltoeVersion=").Append(SteeltoeVersion);
                delimiter = ",";
            }

            if (DotNetFramework != null)
            {
                buf.Append(delimiter).Append("dotNetFramework=").Append(DotNetFramework);
                delimiter = ",";
            }

            if (Language != null)
            {
                buf.Append(delimiter).Append("language=").Append(Language);
                delimiter = ",";
            }

            if (Packaging != null)
            {
                buf.Append(delimiter).Append("packaging=").Append(Packaging);
            }

            if (Dependencies != null)
            {
                buf.Append(delimiter).Append("dependencies=").Append(Dependencies);
            }

            buf.Append(']');
            return buf.ToString();
        }
    }
}
