// <copyright file="AnalyticsSessionTests.cs" company="Kolky">
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
    using Interfaces;
    using Interfaces.Parameters;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// Fixture for <see cref="AnalyticsSession"/>.
    /// </summary>
    [TestFixture]
    public class AnalyticsSessionTests
    {
        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession.AnalyticsSession(string)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest()
        {
            const string trackingId = "UA-XXXX-Y";

            var session = new AnalyticsSession(trackingId);
            Assert.NotNull(session);
            this.AssertClient(session.Client, false, true);
            this.AssertParameters(session.Parameters, trackingId);
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession.AnalyticsSession(string, bool)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest1()
        {
            const string trackingId = "UA-XXXX-Y";

            var session1 = new AnalyticsSession(trackingId, useHttps: true); // default for useHttps is true
            Assert.NotNull(session1);
            this.AssertClient(session1.Client, false, true);
            this.AssertParameters(session1.Parameters, trackingId);

            var session2 = new AnalyticsSession(trackingId, useHttps: false); // default for useHttps is true
            Assert.NotNull(session2);
            this.AssertClient(session2.Client, false, false);
            this.AssertParameters(session2.Parameters, trackingId);
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession.AnalyticsSession(string, bool, bool)"/>
        /// </summary>
        [Test]
        public void AnalyticsSessionTest2()
        {
            const string trackingId = "UA-XXXX-Y";

            var session1 = new AnalyticsSession(trackingId, useHttps: false, sendToDebugServer: true); // default for sendToDebugServer is false
            Assert.NotNull(session1);
            this.AssertClient(session1.Client, true, false);
            this.AssertParameters(session1.Parameters, trackingId);

            var session2 = new AnalyticsSession(trackingId, useHttps: false, sendToDebugServer: false); // default for sendToDebugServer is false
            Assert.NotNull(session2);
            this.AssertClient(session2.Client, false, false);
            this.AssertParameters(session2.Parameters, trackingId);
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession.AnalyticsSession(string, bool, bool)"/>
        /// </summary>
        [Test]
        public void AnalyticsSessionTest3()
        {
            const string trackingId = "UA-XXXX-Y";

            var client = MockRepository.GenerateStrictMock<IAnalyticsClient>();
            var session = new AnalyticsSession(trackingId, client);
            Assert.NotNull(session);
            this.AssertParameters(session.Parameters, trackingId);
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void StartTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackEventHitTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackExceptionHitTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackPageViewHitTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackPageViewHitTest1()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackScreenViewHitTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackSocialHitTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackTimerHitTest()
        {
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void FinishTest()
        {
        }

        private void AssertClient(IAnalyticsClient client, bool debug, bool secure)
        {
            Assert.NotNull(client);
            Assert.AreEqual(debug, client.IsDebug);
            Assert.AreEqual(secure, client.IsSecure);
            Assert.That(client.UserAgent.StartsWith("Allium"));
            if (secure)
            {
                Assert.That(client.BeaconUrl.StartsWith("https://ssl.google-analytics.com/"));
            }
            else
            {
                Assert.That(client.BeaconUrl.StartsWith("http://www.google-analytics.com/"));
            }

            if (debug)
            {
                Assert.That(client.BeaconUrl.EndsWith("debug/collect"));
            }
            else
            {
                Assert.That(client.BeaconUrl.EndsWith("collect"));
            }
        }

        private void AssertParameters(IGeneralParameters parameters, string trackingId)
        {
            Assert.NotNull(parameters);
            Assert.AreEqual(trackingId, parameters.TrackingId);
            Assert.AreEqual("1", parameters.ProtocolVersion);
        }
    }
}