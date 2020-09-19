// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for registries.
    /// </summary>
    /// <typeparam name="TValue">The value to be registered.</typeparam>
    /// <typeparam name="TLookup">Key used to lookup a value.</typeparam>
    public interface IRegistry<TValue, TLookup> : IInitializeable
    {
        /// <summary>
        /// Registers the value.
        /// </summary>
        /// <param name="value">The value to register.</param>
        void Register(TValue value);

        /// <summary>
        /// Look for a value that satisfies the key.
        /// </summary>
        /// <param name="lookup">The lookup key of the value to get.</param>
        /// <returns>A value associated with the lookup key, or <c>null</c> if no value found.</returns>
        TValue Lookup(TLookup lookup);
    }
}
