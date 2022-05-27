// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;

namespace Steeltoe.InitializrService.Test.Utils
{
    public class MockProjectTemplateWebScheme : IWebRequestCreate
    {
        public WebRequest Create(Uri uri)
        {
            return new MockProjectTemplateWebRequest(uri);
        }
    }
}