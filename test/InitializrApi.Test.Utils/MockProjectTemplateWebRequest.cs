// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;

namespace Steeltoe.InitializrApi.Test.Utils
{
    public class MockProjectTemplateWebRequest : WebRequest
    {
        private Uri _uri;

        public MockProjectTemplateWebRequest(Uri uri)
        {
            _uri = uri;
        }

        public override WebResponse GetResponse()
        {
            return new MockProjectTemplateWebResponse(_uri);
        }
    }
}
