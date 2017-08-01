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
    using System.Threading.Tasks;
    using Enums;
    using Interfaces;
    using Interfaces.Parameters;
    using Parameters;
    using Parameters.Hits;
    using Validation;

    /// <summary>
    /// Google Analytics Session.
    /// </summary>
    public class AnalyticsSession : IAnalyticsSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        public AnalyticsSession(string trackingId)
            : this(trackingId, false, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        public AnalyticsSession(string trackingId, bool useHttps)
            : this(trackingId, false, useHttps)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSession"/> class.
        /// </summary>
        /// <param name="trackingId">The tracking ID / web property ID. The format is UA-XXXX-Y.</param>
        /// <param name="useHttps">Whether to use https while sending analytics to google.</param>
        /// <param name="sendToDebugServer">Whether to debug analytics calls by send them to the debug server.</param>
        public AnalyticsSession(string trackingId, bool useHttps, bool sendToDebugServer)
        {
            Requires.NotNullOrWhiteSpace(trackingId, nameof(trackingId));
            Requires.Range(trackingId.StartsWith("UA", StringComparison.Ordinal), nameof(trackingId));

            this.Client = new AnalyticsClient(trackingId, useHttps, sendToDebugServer);
            this.Parameters = new GeneralParameters(trackingId);
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
        /// Start the current session.
        /// NOTE: Sends a message with <see cref="SessionControl.Start"/>.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Start()
        {
            var parameters = this.Parameters.Clone();
            parameters.Session.SessionControl = SessionControl.Start;
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track an Event.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="action">action</param>
        /// <returns>Analytics Event</returns>
        public IAnalyticsEvent TrackEventHit(string category, string action)
        {
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
            var parameters = new ExceptionHitParameters(this.Parameters.Clone(), exception, wasFatal);
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackPageViewHit(string url)
        {
            var parameters = new PageViewHitParameters(this.Parameters.Clone(), url);
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
            var parameters = new ScreenViewHitParameters(this.Parameters.Clone(), screen);
            return await this.Client.Send(parameters);
        }

        /// <summary>
        /// Track a Social Interaction.
        /// </summary>
        /// <param name="network">network</param>
        /// <param name="action">action</param>
        /// <param name="url">url</param>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> TrackSocialHit(string network, string action, string url)
        {
            var parameters = new SocialHitParameters(this.Parameters.Clone(), network, action, url);
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
            return new AnalyticsTiming(this, category, name);
        }

        /// <summary>
        /// Finish the current session.
        /// NOTE: Sends a message with <see cref="SessionControl.End"/>.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Finish()
        {
            var parameters = this.Parameters.Clone();
            parameters.Session.SessionControl = SessionControl.End;
            return await this.Client.Send(parameters);
        }
    }
}
