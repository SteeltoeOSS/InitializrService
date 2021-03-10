// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of a parameter expression.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Gets or sets the parameter name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the parameter expression.
        /// </summary>
        public string Expression { get; set; }
    }
}
