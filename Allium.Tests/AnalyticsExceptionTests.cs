// <copyright file="AnalyticsExceptionTests.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Tests
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Allium;
    using NUnit.Framework;

    /// <summary>
    /// Fixture for <see cref="AnalyticsException"/>.
    /// </summary>
    [TestFixture]
    public class AnalyticsExceptionTests
    {
        /// <summary>
        /// Test for constructor <see cref="AnalyticsException()"/>.
        /// </summary>
        [Test]
        public void AnalyticsExceptionTest()
        {
            var exception = new AnalyticsException();
            Assert.NotNull(exception);
            Assert.AreEqual("Exception of type 'Allium.AnalyticsException' was thrown.", exception.Message);
            Assert.IsNull(exception.InnerException);
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsException(string)"/>.
        /// </summary>
        [Test]
        public void AnalyticsExceptionTest1()
        {
            var exception = new AnalyticsException("Message");
            Assert.NotNull(exception);
            Assert.AreEqual("Message", exception.Message);
            Assert.IsNull(exception.InnerException);
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsException(string, Exception)"/>.
        /// </summary>
        [Test]
        public void AnalyticsExceptionTest2()
        {
            var innerException = new NotSupportedException();
            var exception = new AnalyticsException("Message", innerException);
            Assert.NotNull(exception);
            Assert.AreEqual("Message", exception.Message);
            Assert.AreSame(innerException, exception.InnerException);
        }

        /// <summary>
        /// Test for private constructor.
        /// </summary>
        [Test]
        public void AnalyticsExceptionTest3()
        {
            var innerException = new NotSupportedException();
            var exception = new AnalyticsException("Message", innerException);
            Assert.NotNull(exception);
            Assert.AreEqual("Message", exception.Message);
            Assert.AreSame(innerException, exception.InnerException);

            var formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));

            byte[] serializedMessage;
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, exception);
                serializedMessage = stream.GetBuffer();
            }

            using (var stream = new MemoryStream(serializedMessage))
            {
                var deserializedException = formatter.Deserialize(stream) as AnalyticsException;
                Assert.NotNull(deserializedException);
                Assert.AreEqual("Message", deserializedException.Message);
                Assert.NotNull(deserializedException.InnerException);
                Assert.IsInstanceOf<NotSupportedException>(deserializedException.InnerException);
            }
        }
    }
}