// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for services that can be initialized and reinitialized.
    /// </summary>
    public interface IInitializeable
    {
        /// <summary>
        /// Perform initialization.
        /// Called when started or reconfigured.
        /// </summary>
        void Initialize();
    }
}
