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
    using System.Threading.Tasks;
    using Interfaces;
    using Interfaces.Parameters;
    using NUnit.Framework;
    using Properties;
    using Rhino.Mocks;

    /// <summary>
    /// Fixture for <see cref="AnalyticsSession"/>.
    /// </summary>
    [TestFixture]
    public class AnalyticsSessionTests
    {
        private const string TestUserAgent = "Allium/Mock";
        private const string TestTrackingId = "UA-XXXX-Y";

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest()
        {
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                this.AssertSession(session);
            }
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string, bool)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest1()
        {
            using (var session = new AnalyticsSession(TestTrackingId, useHttps: true)) // default for useHttps is true
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(TestTrackingId, useHttps: false)) // default for useHttps is true
            {
                this.AssertSession(session);
            }
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string, bool, bool)"/>
        /// </summary>
        [Test]
        public void AnalyticsSessionTest2()
        {
            using (var session = new AnalyticsSession(TestTrackingId, useHttps: false, sendToDebugServer: true)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }

            using (var session = new AnalyticsSession(TestTrackingId, useHttps: false, sendToDebugServer: false)) // default for sendToDebugServer is false
            {
                this.AssertSession(session);
            }
        }

        /// <summary>
        /// Test for constructor <see cref="AnalyticsSession(string, IAnalyticsClient)"/>.
        /// </summary>
        [Test]
        public void AnalyticsSessionTest3()
        {
            var repository = new MockRepository();
            var client = repository.StrictMock<IAnalyticsClient>();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                client.Expect(x => x.UserAgent).Return(TestUserAgent);
                client.Expect(x => x.Factory).Return(factory);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId, client))
            {
                Assert.NotNull(session);
                Assert.AreEqual(client, session.Client);
                Assert.AreEqual(TestUserAgent, session.Client.UserAgent);
                Assert.AreEqual(factory, session.Client.Factory);
                this.AssertParameters(session.Parameters);
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertParameters(session.Parameters);

                // Start the session.
                this.AssertResultSuccess(session.Start());

                // Start the session again.
                this.AssertResultFailed(session.Start(), Resources.HasAlreadyStartedSession);

                // Now finish the session.
                this.AssertResultSuccess(session.Finish());
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                var eventHit = session.TrackEventHit("Category", "Action");
                Assert.NotNull(eventHit);
                Assert.NotNull(eventHit.Parameters);
                Assert.AreNotSame(session.Parameters, eventHit.Parameters);
                Assert.AreEqual("Category", eventHit.Parameters.EventCategory);
                Assert.AreEqual("Action", eventHit.Parameters.EventAction);

                this.AssertResultSuccess(eventHit.Send());
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultSuccess(session.TrackExceptionHit(new AnalyticsException("Failure"), true));
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultSuccess(session.TrackPageViewHit("Host", "Path"));
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultSuccess(session.TrackPageViewHit(new Uri("http://localhost")));
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultSuccess(session.TrackPageViewHit("http://localhost"));
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultSuccess(session.TrackScreenViewHit("Screen"));
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.ServiceUnavailable);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultFailed(
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertResultSuccess(session.TrackSocialHit("Network", "Action", "Target"));
            }
        }

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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                using (var timer = session.TrackTimerHit("Category", "Name"))
                {
                    Assert.IsNull(timer.Elapsed);
                    Assert.NotNull(timer.Parameters);
                    Assert.AreNotSame(session.Parameters, timer.Parameters);
                    Assert.AreEqual("Category", timer.Parameters.UserTimingCategory);
                    Assert.AreEqual("Name", timer.Parameters.UserTimingVariableName);

                    this.AssertResultSuccess(timer.FinishAndSend());
                    Assert.NotNull(timer.Elapsed);
                }
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
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                using (var timer = session.TrackTimerHit("Category", "Name"))
                {
                    Assert.IsNull(timer.Elapsed);
                    Assert.NotNull(timer.Parameters);
                    Assert.AreNotSame(session.Parameters, timer.Parameters);
                    Assert.AreEqual("Category", timer.Parameters.UserTimingCategory);
                    Assert.AreEqual("Name", timer.Parameters.UserTimingVariableName);

                    this.AssertResultFailed(timer.Send(), Resources.HasNotYetFinishedTiming);
                }
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Finish"/> and <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void FinishAndStartTest()
        {
            var repository = new MockRepository();
            var factory = repository.StrictMock<IWebRequestCreate>();
            using (repository.Record())
            {
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
                this.ExpectWebRequest(repository, factory, HttpStatusCode.OK);
            }

            using (repository.Playback())
            using (var session = new AnalyticsSession(TestTrackingId))
            {
                session.Client.Factory = factory;
                this.AssertParameters(session.Parameters);

                // Finish without start fails the result
                this.AssertResultFailed(session.Finish(), Resources.HasNotYetStartedSession);

                // Start the session.
                this.AssertResultSuccess(session.Start());

                // Now finish the session.
                this.AssertResultSuccess(session.Finish());
            }
        }

        private void AssertSession(AnalyticsSession session)
        {
            Assert.NotNull(session);
            Assert.NotNull(session.Client);
            Assert.That(session.Client.UserAgent.StartsWith("Allium", StringComparison.Ordinal));
            Assert.NotNull(session.Client.Factory);
            this.AssertParameters(session.Parameters);

            // NOTE: This overrides the WebRequest factory to simulate real output.
            var repository = new MockRepository();
            session.Client.Factory = this.ExpectWebRequest(repository, repository.DynamicMock<IWebRequestCreate>(), HttpStatusCode.OK);
        }

        private void AssertResultSuccess(Task<IAnalyticsResult> task)
        {
            Assert.NotNull(task);
            task.Wait();

            Assert.NotNull(task.Result);
            Assert.IsTrue(task.Result.Success);
            Assert.IsNull(task.Result.Exception);
        }

        private void AssertResultFailed(Task<IAnalyticsResult> task, string message)
        {
            Assert.NotNull(task);
            task.Wait();

            Assert.NotNull(task.Result);
            Assert.IsFalse(task.Result.Success);
            Assert.IsInstanceOf<AnalyticsException>(task.Result.Exception);
            Assert.AreEqual(message, task.Result.Exception.Message);
        }

        private IWebRequestCreate ExpectWebRequest(MockRepository repository, IWebRequestCreate factory, HttpStatusCode statusCode)
        {
            var requestHeaders = repository.PartialMock<WebHeaderCollection>();
            var request = repository.Stub<HttpWebRequest>();
            var response = repository.PartialMock<HttpWebResponse>();

            // TODO: Expect parameters in URI!
            factory.Expect(x => x.Create(Arg<Uri>.Is.Anything)).Return(request).Repeat.Once();
            request.Headers = requestHeaders;
            request.Expect(x => x.GetResponseAsync()).Return(Task.Run<WebResponse>(() => response)).Repeat.Once();
            response.Expect(x => x.StatusCode).Return(statusCode).Repeat.Any();

            return factory;
        }

        private void AssertParameters(IGeneralParameters parameters)
        {
            Assert.NotNull(parameters);
            Assert.AreEqual(TestTrackingId, parameters.TrackingId);
            Assert.AreEqual("1", parameters.ProtocolVersion);
        }
    }
}