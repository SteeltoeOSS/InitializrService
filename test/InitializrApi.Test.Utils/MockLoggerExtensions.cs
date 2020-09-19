// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// Borrowed from https://gist.github.com/changhuixu/c59b18ad7523562eabfa2016546d3b26

using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace Steeltoe.InitializrApi.Test.Utils
{
    public static class MockLoggerExtensions
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message,
            string failMessage = null)
        {
            loggerMock.VerifyLog(level, message, Times.Once(), failMessage);
        }

        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Times times,
            string failMessage = null)
        {
            loggerMock.Verify(l => l.Log(level, It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, _) => o.ToString() == message), It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                times, failMessage);
        }
    }
}
