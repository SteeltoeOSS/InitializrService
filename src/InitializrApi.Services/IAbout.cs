// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models;

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for object that can proide <see cref="About"/>.
    /// </summary>
    public interface IAbout
    {
        /// <summary>
        /// Gets the <see cref="About"/> details.
        /// </summary>
        /// <returns>about.</returns>
        public About GetAbout();
    }
}
