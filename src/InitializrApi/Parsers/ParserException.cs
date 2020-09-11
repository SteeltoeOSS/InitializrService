// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Steeltoe.InitializrApi.Parsers
{
    /// <summary>
    /// The exception that is throw when an parser encounters an error.
    /// </summary>
    public class ParserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserException"/> class.
        /// </summary>
        public ParserException(string message)
            : base(message)
        {
        }
    }
}
