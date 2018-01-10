// <copyright file="AnalyticsTimingTests.cs" company="Kolky">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using Allium.Properties;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// Fixture for <see cref="AnalyticsTiming"/>.
    /// </summary>
    [TestFixture]
    public class AnalyticsTimingTests
    {
        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackTimerHit"/>.
        /// </summary>
        [Test]
        public void TrackTimerHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Finish Timer
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                using (var timer = session.TrackTimerHit("Category", "Name"))
                {
                    Assert.IsNull(timer.Elapsed);
                    Assert.NotNull(timer.Parameters);
                    Assert.AreNotSame(session.Parameters, timer.Parameters);
                    Assert.AreEqual("Category", timer.Parameters.UserTimingCategory);
                    Assert.AreEqual("Name", timer.Parameters.UserTimingVariableName);
                    Assert.LessOrEqual(timer.Started.Ticks, DateTime.Now.Ticks);
                    Assert.IsNull(timer.Finished);

                    AlliumAssert.Success(timer.FinishAndSend());
                    Assert.NotNull(timer.Elapsed);
                    Assert.NotNull(timer.Finished);
                    Assert.Greater(timer.Finished.Value, timer.Started);

                    // Test that we do not change the value when we call finish after the timer was already sent.
                    var previousFinish = timer.Finished;
                    timer.Finish();
                    Assert.AreEqual(previousFinish, timer.Finished.Value);

                    // Test that another send fails
                    AlliumAssert.Failed(timer.Send(), Resources.HasAlreadySentTiming);
                }
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsTiming.Dispose(bool)"/>
        /// </summary>
        [Test]
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Required for this test!")]
        public void TimerHitDisposedTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Timer Finish (during Dispose)
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;

                var timer = session.TrackTimerHit("Category", "Name");
                timer.Dispose();

                // Calling a second dispose does nothing
                timer.Dispose();

                // Catch dispose exception after trying to send after being disposed
                var timerDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => timer.Send());
                Assert.AreEqual(nameof(AnalyticsTiming), timerDisposedException.ObjectName);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsTiming.Dispose(bool)"/>
        /// </summary>
        [Test]
        public void TimerHitFailDisposedTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.InternalServerError); // Timer Finish (during Dispose)
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;

                var timer = session.TrackTimerHit("Category", "Name");

                // Catch dispose exception after trying to send after being disposed
                var analyticsException = Assert.Throws<AnalyticsException>(() => timer.Dispose());
                Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, Resources.InvalidResponse, HttpStatusCode.InternalServerError), analyticsException.Message);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackTimerHit"/>.
        /// </summary>
        [Test]
        public void TrackTimerHitNotYetFinishedTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // ?
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                using (var timer = session.TrackTimerHit("Category", "Name"))
                {
                    Assert.IsNull(timer.Elapsed);
                    Assert.NotNull(timer.Parameters);
                    Assert.AreNotSame(session.Parameters, timer.Parameters);
                    Assert.AreEqual("Category", timer.Parameters.UserTimingCategory);
                    Assert.AreEqual("Name", timer.Parameters.UserTimingVariableName);

                    AlliumAssert.Failed(timer.Send(), Resources.HasNotYetFinishedTiming);
                }
            }
        }
    }
}
