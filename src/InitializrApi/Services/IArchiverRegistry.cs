// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.InitializrApi.Services
{
    /// <summary>
    /// Contract for a archiver registry implementations.
    /// </summary>
    public interface IArchiverRegistry : IRegistry<IArchiver, string>
    {
    }
}
