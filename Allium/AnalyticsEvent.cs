// <copyright file="AnalyticsEvent.cs" company="Kolky">
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
    using System.Threading.Tasks;
    using Interfaces;
    using Interfaces.Parameters.Hits;
    using Parameters.Hits;
    using Validation;

    /// <summary>
    /// Analytics Event.
    /// </summary>
    internal class AnalyticsEvent : IAnalyticsEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsEvent"/> class.
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="category">category</param>
        /// <param name="action">action</param>
        public AnalyticsEvent(AnalyticsSession session, string category, string action)
        {
            Requires.NotNull(session, nameof(session));

            this.Session = session;
            this.Parameters = new EventHitParameters(session.Parameters.Clone(), category, action);
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        public AnalyticsSession Session { get; private set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        public IEventParameters Parameters { get; private set; }

        /// <summary>
        /// Send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Send()
        {
            var parameters = new EventHitParameters(this.Parameters.Clone(), this.Parameters.EventCategory, this.Parameters.EventAction);
            return await this.Session.Client.Send(parameters);
        }
    }
}
