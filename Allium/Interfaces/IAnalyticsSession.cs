// <copyright file="IAnalyticsSession.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Allium.Interfaces.Parameters.Hits;
    using Enums;
    using Parameters;

    /// <summary>
    /// Interface for a Analytics session.
    /// NOTE: When disposing and a <see cref="SessionControl.Start"/> was sent, this will automatically send a <see cref="SessionControl.End"/>.
    /// </summary>
    public interface IAnalyticsSession : IDisposable
    {
        /// <summary>
        /// Gets the parameters for the whole session.
        /// NOTE: These are shallow copied into each hit.
        /// </summary>
        IGeneralParameters Parameters { get; }

        /// <summary>
        /// Start the current session.
        /// NOTE: Sends a message with <see cref="SessionControl.Start"/>.
        /// </summary>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Start();

        /// <summary>
        /// Track an Event.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="action">action</param>
        /// <returns>Analytics Event</returns>
        IAnalyticsHit<IEventParameters> TrackEventHit(string category, string action);

        /// <summary>
        /// Track an Exception.
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="wasFatal">wasFatal</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> TrackExceptionHit(Exception exception, bool? wasFatal);

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="uri">url</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> TrackPageViewHit(string uri);

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> TrackPageViewHit(Uri url);

        /// <summary>
        /// Track a PageView.
        /// </summary>
        /// <param name="hostName">hostName</param>
        /// <param name="path">path</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> TrackPageViewHit(string hostName, string path);

        /// <summary>
        /// Track a ScreenView.
        /// </summary>
        /// <param name="screen">screen</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> TrackScreenViewHit(string screen);

        /// <summary>
        /// Track a Social Interaction.
        /// </summary>
        /// <param name="network">network</param>
        /// <param name="action">action</param>
        /// <param name="target">target</param>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> TrackSocialHit(string network, string action, string target);

        /// <summary>
        /// Track a E-Commerce Transaction.
        /// </summary>
        /// <param name="transactionId">transactionId</param>
        /// <returns>Analytics Results</returns>
        IAnalyticsHit<IEcommerceTransactionParameters> TrackTransactionHit(string transactionId);

        /// <summary>
        /// Track a E-Commerce Item.
        /// </summary>
        /// <param name="transactionId">transactionId</param>
        /// <param name="itemName">itemId</param>
        /// <returns>Analytics Results</returns>
        IAnalyticsHit<IEcommerceItemParameters> TrackItemHit(string transactionId, string itemName);

        /// <summary>
        /// Start a timer to track.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="name">name</param>
        /// <returns>Analytics Timing</returns>
        IAnalyticsTiming TrackTimerHit(string category, string name);

        /// <summary>
        /// Finish the current session.
        /// NOTE: Sends a message with <see cref="SessionControl.End"/>.
        /// </summary>
        /// <returns>Analytics Results</returns>
        Task<IAnalyticsResult> Finish();
    }
}
