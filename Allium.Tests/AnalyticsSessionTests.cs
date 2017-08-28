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
    using System.Net;
    using System.Threading.Tasks;
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
        /// Test for constructor <see cref="AnalyticsSession(string, bool, bool)"/>.
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
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void StartTest()
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
                var task = session.Start();
                task.Wait();
                Assert.NotNull(task.Result);
                Assert.IsTrue(task.Result.Success);
                Assert.Null(task.Result.Exception);

                // Start the session again.
                task = session.Start();
                task.Wait();
                Assert.NotNull(task.Result);
                Assert.IsFalse(task.Result.Success);
                Assert.That(
                    task.Result.Exception,
                    Is.TypeOf<AnalyticsException>().And.Message.EqualTo("Can't start a new session when one is already active!"));

                // Now finish the session.
                task = session.Finish();
                task.Wait();
                Assert.NotNull(task.Result);
                Assert.IsTrue(task.Result.Success);
                Assert.Null(task.Result.Exception);
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
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
                var task = eventHit.Send();
                task.Wait();
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
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
                var task = session.TrackExceptionHit(new AnalyticsException("Failure"), true);
                task.Wait();
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void TrackPageViewHitTest()
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
                var task = session.TrackPageViewHit("Host", "Path");
                task.Wait();
            }
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
                    var task = timer.FinishAndSend();
                    task.Wait();
                }
            }
        }

        /// <summary>
        /// Test for method <see cref="AnalyticsSession.Start"/>.
        /// </summary>
        [Test]
        public void FinishTest()
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
                var task = session.Finish();
                task.Wait();
                Assert.NotNull(task.Result);
                Assert.IsFalse(task.Result.Success);
                Assert.That(
                    task.Result.Exception,
                    Is.TypeOf<AnalyticsException>().And.Message.EqualTo("Can't finish a session we haven't started yet!"));

                // Start the session.
                task = session.Start();
                task.Wait();
                Assert.NotNull(task.Result);
                Assert.IsTrue(task.Result.Success);
                Assert.Null(task.Result.Exception);

                // Now finish the session.
                task = session.Finish();
                task.Wait();
                Assert.NotNull(task.Result);
                Assert.IsTrue(task.Result.Success);
                Assert.Null(task.Result.Exception);
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

        private IWebRequestCreate ExpectWebRequest(MockRepository repository, IWebRequestCreate factory, HttpStatusCode statusCode)
        {
            var requestHeaders = repository.PartialMock<WebHeaderCollection>();
            var request = repository.Stub<HttpWebRequest>();
            var response = repository.PartialMock<HttpWebResponse>();

            // TODO: Expect parameters in URI!
            factory.Expect(x => x.Create(Arg<Uri>.Is.Anything)).Return(request).Repeat.Once();
            request.Headers = requestHeaders;
            request.Expect(x => x.GetResponseAsync()).Return(Task.Run<WebResponse>(() => response)).Repeat.Once();
            response.Expect(x => x.StatusCode).Return(statusCode).Repeat.Once();

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