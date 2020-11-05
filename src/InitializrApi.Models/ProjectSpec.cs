// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of the project generation spec.
    /// </summary>
    public sealed class ProjectSpec
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the project namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the Steeltoe version.
        /// </summary>
        public string SteeltoeVersion { get; set; }

        /// <summary>
        /// Gets or sets the DotNet framework ID.
        /// </summary>
        public string DotNetFramework { get; set; }

        /// <summary>
        /// Gets or sets the DotNet template ID.
        /// </summary>
        public string DotNetTemplate { get; set; }

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
            var delim = string.Empty;
            buf.Append('[');
            if (Name != null)
            {
                buf.Append(delim).Append("name=").Append(Name);
                delim = ",";
            }

            if (Description != null)
            {
                buf.Append(delim).Append("description=").Append(Description);
                delim = ",";
            }

            if (Namespace != null)
            {
                buf.Append(delim).Append("namespace=").Append(Namespace);
                delim = ",";
            }

            if (SteeltoeVersion != null)
            {
                buf.Append(delim).Append("steeltoeVersion=").Append(SteeltoeVersion);
                delim = ",";
            }

            if (DotNetFramework != null)
            {
                buf.Append(delim).Append("dotNetFramework=").Append(DotNetFramework);
                delim = ",";
            }

            if (DotNetTemplate != null)
            {
                buf.Append(delim).Append("dotNetTemplate=").Append(DotNetTemplate);
                delim = ",";
            }

            if (Language != null)
            {
                buf.Append(delim).Append("language=").Append(Language);
                delim = ",";
            }

            if (Packaging != null)
            {
                buf.Append(delim).Append("packaging=").Append(Packaging);
            }

            if (Dependencies != null)
            {
                buf.Append(delim).Append("dependencies=").Append(Dependencies);
            }

            buf.Append(']');
            return buf.ToString();
        }
    }
}
