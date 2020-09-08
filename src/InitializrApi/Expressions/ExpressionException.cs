// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Steeltoe.InitializrApi.Expressions
{
    /// <summary>
    /// An exception representing an error in an expression.
    /// </summary>
    public class ExpressionException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ExpressionException(string message)
            : base(message)
        {
        }
    }
}
