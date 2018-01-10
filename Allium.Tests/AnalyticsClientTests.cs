// <copyright file="AnalyticsClientTests.cs" company="Kolky">
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
    using Allium.Parameters;
    using NUnit.Framework;
    using Properties;

    /// <summary>
    /// Fixture for <see cref="AnalyticsClient"/>.
    /// </summary>
    [TestFixture]
    public class AnalyticsClientTests
    {
        /// <summary>
        /// Test for method <see cref="AnalyticsClient.Send"/>.
        /// </summary>
        [Test]
        public void SendParametersNullTest()
        {
            var client = new AnalyticsClient(true, true);
            Assert.NotNull(client);
            Assert.That(client.UserAgent.StartsWith("Allium", StringComparison.Ordinal));
            Assert.NotNull(client.Factory);

            var exception = Assert.CatchAsync<ArgumentNullException>(() => client.Send(null));
            Assert.AreEqual("parameters", exception.ParamName);
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsClient.Send"/>.
        /// </summary>
        [Test]
        public void SendInvalidResponseTest()
        {
            var client = new AnalyticsClient(true, true);
            client.Factory = null;
            Assert.NotNull(client);
            Assert.That(client.UserAgent.StartsWith("Allium", StringComparison.Ordinal));
            Assert.IsNull(client.Factory);

            var task = client.Send(new GeneralParameters(AlliumConstants.TestTrackingId));
            task.Wait();

            Assert.NotNull(task.Result);
            Assert.IsFalse(task.Result.Success);
            Assert.IsInstanceOf<AnalyticsException>(task.Result.Exception);
            Assert.AreEqual(Resources.RequestFailed, task.Result.Exception.Message);
        }
    }
}
