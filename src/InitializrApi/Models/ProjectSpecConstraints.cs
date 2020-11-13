// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of project spec constraints.
    /// </summary>
    public sealed class ProjectSpecConstraints
    {
        /* ----------------------------------------------------------------- *
         * properties                                                        *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Gets or sets the Steeltoe version range.
        /// </summary>
        ///
        public ReleaseRange SteeltoeVersionRange { get; set; }

        /// <summary>
        /// Gets or sets the DotNet framework range.
        /// </summary>
        public ReleaseRange DotNetFrameworkRange { get; set; }

        /// <summary>
        /// Gets or sets the DotNet template.
        /// </summary>
        public string DotNetTemplate { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Returns a user-friendly representation of the current object.
        /// </summary>
        /// <returns>Returns a user-friendly string that represents the current object.</returns>
        public override string ToString()
        {
            var buf = new StringBuilder();
            buf.Append('[');
            buf.Append("steeltoeVersionRange=");
            buf.Append(SteeltoeVersionRange?.ToPrettyString());
            buf.Append(",dotNetFrameworkRange=");
            buf.Append(DotNetFrameworkRange?.ToPrettyString());
            buf.Append(",dotNetTemplate=");
            buf.Append(DotNetTemplate);
            buf.Append(",language=");
            buf.Append(Language);
            buf.Append(']');
            return buf.ToString();
        }
    }
}
