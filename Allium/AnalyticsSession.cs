// <copyright file="AnalyticsSession.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Enums;
    using Interfaces;
    using Interfaces.Parameters;
    using Parameters;
    using Parameters.Hits;
    using Properties;
    using Validation;

    /// <summary>
    /// Google Analytics Session.
    /// </summary>
    public sealed class AnalyticsSession : IAnalyticsSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        public AnalyticsSession(string trackingId)
            : this(trackingId, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="clientId">The client Id to anonymously identity a user.</param>
        public AnalyticsSession(string trackingId, Guid clientId)
            : this(trackingId, clientId, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="userId">The user Id to identify a user.</param>
        public AnalyticsSession(string trackingId, string userId)
            : this(trackingId, userId, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        public AnalyticsSession(string trackingId, bool useHttps)
            : this(trackingId, useHttps, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="clientId">The client Id to anonymously identity a user.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        public AnalyticsSession(string trackingId, Guid clientId, bool useHttps)
            : this(trackingId, clientId, useHttps, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="userId">The user Id to identify a user.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        public AnalyticsSession(string trackingId, string userId, bool useHttps)
            : this(trackingId, userId, useHttps, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        /// <param name="sendToDebugServer">Whether to debug analytics calls by send them to the debug server.</param>
        public AnalyticsSession(string trackingId, bool useHttps, bool sendToDebugServer)
            : this(trackingId, new AnalyticsClient(useHttps, sendToDebugServer))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="clientId">The client Id to anonymously identity a user.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        /// <param name="sendToDebugServer">Whether to debug analytics calls by send them to the debug server.</param>
        public AnalyticsSession(string trackingId, Guid clientId, bool useHttps, bool sendToDebugServer)
            : this(trackingId, clientId, new AnalyticsClient(useHttps, sendToDebugServer))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="userId">The user Id to identify a user.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        /// <param name="sendToDebugServer">Whether to debug analytics calls by send them to the debug server.</param>
        public AnalyticsSession(string trackingId, string userId, bool useHttps, bool sendToDebugServer)
            : this(trackingId, userId, new AnalyticsClient(useHttps, sendToDebugServer))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="client">Analytics client</param>
        internal AnalyticsSession(string trackingId, IAnalyticsClient client)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));
            Requires.Range(trackingId.StartsWith("UA", StringComparison.Ordinal), nameof(trackingId));
            Requires.NotNull(client, nameof(client));

            this.Client = client;
            this.Parameters = new GeneralParameters(trackingId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="clientId">The client Id to anonymously identity a user.</param>
        /// <param name="client">Analytics client</param>
        internal AnalyticsSession(string trackingId, Guid clientId, IAnalyticsClient client)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));
            Requires.Range(trackingId.StartsWith("UA", StringComparison.Ordinal), nameof(trackingId));
            Requires.NotNull(client, nameof(client));

            this.Client = client;
            this.Parameters = new GeneralParameters(trackingId, clientId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="userId">The user Id to identify a user.</param>
        /// <param name="client">Analytics client</param>
        internal AnalyticsSession(string trackingId, string userId, IAnalyticsClient client)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));
            Requires.Range(trackingId.StartsWith("UA", StringComparison.Ordinal), nameof(trackingId));
            Requires.NotNull(client, nameof(client));

            this.Client = client;
            this.Parameters = new GeneralParameters(trackingId, userId);
        }

        /// <summary>
        /// Gets the parameters for the whole session.
        /// NOTE: These are shallow copied into each hit.
        /// </summary>
        public IGeneralParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the client.
        /// </summary>
        internal IAnalyticsClient Client { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the session was started.
        /// </summary>
        internal bool SessionStarted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the session was finished.
        /// </summary>
        internal bool SessionFinished { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the timing was disposed.
        /// </summary>
        internal bool Disposed { get; private set; }

        /// <summary>
        /// Start the current session.
        /// NOTE: Sends a message with <see cref="SessionControl.Start"/>.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Start()
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            if (!this.SessionStarted || this.SessionFinished)
            {
                var parameters = this.Parameters.Clone();
                parameters.Session.SessionControl = SessionControl.Start;
                var results = await this.Client.Send(parameters);
                this.SessionStarted = results != null && results.Success;
                this.SessionFinished = !this.SessionStarted;
                return results;
            }

            // Can't start a session if one is active!
            return new AnalyticsResult(new AnalyticsException(Resources.HasAlreadyStartedSession));
        }

        /// <summary>
        /// Track an Event.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="action">action</param>
        /// <returns>Analytics Event</returns>
        public IAnalyticsEvent TrackEventHit(string category, string action)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            return new AnalyticsEvent(this, category, action);
        }

        /// <summary>
        /// Track an Exception.
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="wasFatal">wasFatal</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackExceptionHit(Exception exception, bool? wasFatal)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            var parameters = new ExceptionHitParameters(this.Parameters.Clone(), exception, wasFatal);
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="uri">uri</param>
        /// <returns>Analytics Results</returns>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads", Justification = "Does not see it is fixed, bug?")]
        public async Task<IAnalyticsResult> TrackPageViewHit(string uri)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            return await this.TrackPageViewHit(new Uri(uri));
        }

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackPageViewHit(Uri url)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            var parameters = new PageViewHitParameters(this.Parameters.Clone(), url.ToString());
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="hostName">hostName</param>
        /// <param name="path">path</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackPageViewHit(string hostName, string path)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            var parameters = new PageViewHitParameters(this.Parameters.Clone(), hostName, path);
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track a ScreenView.
        /// </summary>
        /// <param name="screen">screen</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackScreenViewHit(string screen)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            var parameters = new ScreenViewHitParameters(this.Parameters.Clone(), screen);
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track a Social Interaction.
        /// </summary>
        /// <param name="network">network</param>
        /// <param name="action">action</param>
        /// <param name="target">target</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackSocialHit(string network, string action, string target)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            var parameters = new SocialHitParameters(this.Parameters.Clone(), network, action, target);
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Start a timer to track.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="name">name</param>
        /// <returns>Analytics Timing</returns>
        public IAnalyticsTiming TrackTimerHit(string category, string name)
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            return new AnalyticsTiming(this, category, name);
        }

        /// <summary>
        /// Finish the current session.
        /// NOTE: Sends a message with <see cref="SessionControl.End"/>.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Finish()
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsSession));
            }

            if (!this.SessionStarted)
            {
                return new AnalyticsResult(new AnalyticsException(Resources.HasNotYetStartedSession));
            }

            if (this.SessionFinished)
            {
                return new AnalyticsResult(new AnalyticsException(Resources.HasAlreadyFinishedSession));
            }

            var parameters = this.Parameters.Clone();
            parameters.Session.SessionControl = SessionControl.End;
            var results = await this.Client.Send(parameters);
            this.SessionFinished = results != null && results.Success;
            return results;
        }

        /// <summary>
        /// Dispose; this finishes the session (if started) and directly sends the parameters.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Actually execute the dispose.
        /// </summary>
        /// <param name="disposing">disposing</param>
        internal void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                try
                {
                    // Finish any started session, but do nothing otherwise (it might fail)
                    if (this.SessionStarted && !this.SessionFinished)
                    {
                        var sendTask = this.Finish();
                        sendTask.Wait();

                        // Throw exception if finishing fails.. (otherwise nobody would ever find out!)
                        if (sendTask.Result != null && !sendTask.Result.Success)
                        {
                            throw sendTask.Result.Exception;
                        }
                    }
                }
                finally
                {
                    this.Disposed = true;
                }
            }
        }
    }
}
