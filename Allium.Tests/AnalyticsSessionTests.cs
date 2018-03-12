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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using Interfaces;
    using NUnit.Framework;
    using Properties;
    using Rhino.Mocks;

    /// <summary>
    /// Fixture for <see cref="AnalyticsSession"/>.
    /// </summary>
    [TestFixture]
    public class AnalyticsSessionTests
    {
        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string)"/>, <see cref="AnalyticsSession(string, string)"/> and <see cref="AnalyticsSession(string, Guid)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest()
        {
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, "User"))
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, Guid.NewGuid()))
            {
                this.AssertSession(session);
            }
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string, bool)"/>, <see cref="AnalyticsSession(string, string, bool)"/> and <see cref="AnalyticsSession(string, Guid, bool)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest1()
        {
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId)) // default for useHttps is true
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, "User")) // default for useHttps is true
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, Guid.NewGuid())) // default for useHttps is true
            {
                this.AssertSession(session);
            }
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string, bool)"/>, <see cref="AnalyticsSession(string, string, bool)"/> and <see cref="AnalyticsSession(string, Guid, bool)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest2()
        {
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, sendToDebugServer: true)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, sendToDebugServer: false)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, "User", sendToDebugServer: true)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, "User", sendToDebugServer: false)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, Guid.NewGuid(), sendToDebugServer: true)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, Guid.NewGuid(), sendToDebugServer: false)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string, IAnalyticsClient)"/>, <see cref="AnalyticsSession(string, string, IAnalyticsClient)"/> and <see cref="AnalyticsSession(string, Guid, IAnalyticsClient)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest3()
        {
            var repository = new MockRepository();
            var client = repository.StrictMock<IAnalyticsClient>();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                client.Expect(x => x.UserAgent).Return(AlliumConstants.TestUserAgent);
                client.Expect(x => x.Factory).Return(factory);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId, client))
            {
                Assert.NotNull(session);
                Assert.AreEqual(client, session.Client);
                Assert.AreEqual(AlliumConstants.TestUserAgent, session.Client.UserAgent);
                Assert.AreEqual(factory, session.Client.Factory);
                AlliumAssert.Parameters(session.Parameters);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/> and <see cref="AnalyticsSession.Finish"/>.
        /// </summary>
        [Test]
        public void StartAndFinishTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Parameters(session.Parameters);

                // Try to finish the session.
                AlliumAssert.Failed(session.Finish(), Resources.HasNotYetStartedSession);

                // Start the session.
                AlliumAssert.Success(session.Start());

                // Start the session again.
                AlliumAssert.Failed(session.Start(), Resources.HasAlreadyStartedSession);

                // Now finish the session.
                AlliumAssert.Success(session.Finish());

                // Finish the session again.
                AlliumAssert.Failed(session.Finish(), Resources.HasAlreadyFinishedSession);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackEventHit"/>.
        /// </summary>
        [Test]
        public void TrackEventHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                var eventHit = session.TrackEventHit("Category", "Action");
                Assert.NotNull(eventHit);
                Assert.NotNull(eventHit.Parameters);
                Assert.AreNotSame(session.Parameters, eventHit.Parameters);
                Assert.AreEqual("Category", eventHit.Parameters.EventCategory);
                Assert.AreEqual("Action", eventHit.Parameters.EventAction);

                AlliumAssert.Success(eventHit.Send());
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackExceptionHit"/>.
        /// </summary>
        [Test]
        public void TrackExceptionHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackExceptionHit(new AnalyticsException("Failure"), false));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackExceptionHit"/>.
        /// </summary>
        [Test]
        public void TrackEmptyExceptionHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackExceptionHit(null, null));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackExceptionHit"/>.
        /// </summary>
        [Test]
        public void TrackFatalExceptionHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackExceptionHit(new AnalyticsException("Failure"), true));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackPageViewHit(string, string)"/>.
        /// </summary>
        [Test]
        public void TrackPageViewHitTest1()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackPageViewHit("Host", "Path"));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackPageViewHit(Uri)"/>.
        /// </summary>
        [Test]
        public void TrackPageViewHitTest2()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackPageViewHit(new Uri("http://localhost")));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackPageViewHit(string)"/>.
        /// </summary>
        [Test]
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Justification = "Need to hit for CodeCoverage.")]
        public void TrackPageViewHitTest3()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackPageViewHit("http://localhost"));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackScreenViewHit"/>.
        /// </summary>
        [Test]
        public void TrackScreenViewHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackScreenViewHit("Screen"));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackScreenViewHit"/>.
        /// </summary>
        [Test]
        public void TrackScreenViewHitServiceUnavailableTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.ServiceUnavailable);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Failed(
                    session.TrackScreenViewHit("Screen"),
                    string.Format(CultureInfo.InvariantCulture, Resources.InvalidResponse, HttpStatusCode.ServiceUnavailable));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.TrackSocialHit"/>.
        /// </summary>
        [Test]
        public void TrackSocialHitTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Success(session.TrackSocialHit("Network", "Action", "Target"));
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Dispose(bool)"/>.
        /// </summary>
        [Test]
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Required for this test!")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caused by double dispose statement!")]
        public void DisposeWithoutStartTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
            }

            using (repository.Playback())
            {
                // Do nothing, should throw nothing during dispose!
                var session = new AnalyticsSession(AlliumConstants.TestTrackingId);
                session.Client.Factory = factory;
                session.Dispose();

                // Calling a second dispose does nothing
                session.Dispose();
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Dispose(bool)"/>.
        /// </summary>
        [Test]
        public void FinishByDisposingTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Start
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Finish (during Dispose)
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                // Start a session that we never finish, so dispose will have to take care of it..
                session.Client.Factory = factory;
                AlliumAssert.Success(session.Start());
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Dispose(bool)"/>.
        /// </summary>
        [Test]
        public void FailFinishByDisposingTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Start
                repository.ExpectWebRequest(factory, HttpStatusCode.InternalServerError); // Session Finish (during Dispose)
            }

            using (repository.Playback())
            {
                // Start a session that we never finish, so dispose will have to take care of it..
                var session = new AnalyticsSession(AlliumConstants.TestTrackingId);
                session.Client.Factory = factory;
                AlliumAssert.Success(session.Start());

                var analyticsException = Assert.Throws<AnalyticsException>(() => session.Dispose());
                Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, Resources.InvalidResponse, HttpStatusCode.InternalServerError), analyticsException.Message);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Dispose(bool)"/>
        /// </summary>
        [Test]
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Required for this test!")]
        public void SessionDisposedTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Start
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Finish (during Dispose)
            }

            using (repository.Playback())
            {
                var session = new AnalyticsSession(AlliumConstants.TestTrackingId);
                session.Client.Factory = factory;
                AlliumAssert.Success(session.Start());

                // Dispose the session, to see what happens after..
                session.Dispose();

                // Catch dispose exception after trying to do anything after being disposed
                var sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.Start());
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.Finish());
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.Throws<ObjectDisposedException>(() => session.TrackEventHit("Category", "Action"));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.TrackExceptionHit(new AnalyticsException("Failure"), true));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.TrackPageViewHit("http://localhost"));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.TrackPageViewHit(new Uri("http://localhost")));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.TrackPageViewHit("Hostname", "Path"));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.TrackScreenViewHit("Screen"));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.CatchAsync<ObjectDisposedException>(() => session.TrackSocialHit("Network", "Action", "Target"));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);

                sessionDisposedException = Assert.Throws<ObjectDisposedException>(() => session.TrackTimerHit("Category", "Name"));
                Assert.AreEqual(nameof(AnalyticsSession), sessionDisposedException.ObjectName);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Finish"/> and <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void FinishAndStartSessionTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Start
                repository.ExpectWebRequest(factory, HttpStatusCode.OK); // Session Finish
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(AlliumConstants.TestTrackingId))
            {
                session.Client.Factory = factory;
                AlliumAssert.Parameters(session.Parameters);

                // Finish without start fails the result
                AlliumAssert.Failed(session.Finish(), Resources.HasNotYetStartedSession);

                // Start the session.
                AlliumAssert.Success(session.Start());

                // Now finish the session.
                AlliumAssert.Success(session.Finish());
            }
        }

        private void AssertSession(AnalyticsSession session)
        {
            Assert.NotNull(session);
            Assert.NotNull(session.Client);
            Assert.That(session.Client.UserAgent.StartsWith("Allium", StringComparison.Ordinal));
            Assert.NotNull(session.Client.Factory);
            AlliumAssert.Parameters(session.Parameters);

            // NOTE: This overrides the WebRequest factory to simulate real output.
            var repository = new MockRepository();
            var factory = repository.DynamicMock<IWebRequestCreate>();
            session.Client.Factory = factory;

            // Expect a success result
            repository.ExpectWebRequest(factory, HttpStatusCode.OK);
        }
    }
}