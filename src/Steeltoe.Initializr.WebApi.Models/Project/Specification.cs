// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.Initializr.WebApi.Models.Project
{
    /// <summary>
    /// A model of the specification used to generate a project.
    /// </summary>
    public class Specification
    {
        /// <summary>
        /// Compares the specified object to this object.
        /// </summary>
        /// <param name="obj">other instance</param>
        /// <returns>whether objects are equal</returns>
        public override bool Equals(object obj)
        {
            return true;
        }

        /// <summary>
        /// Returns the hash code for this object.
        /// </summary>
        /// <returns>object hash code</returns>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
