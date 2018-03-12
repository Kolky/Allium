// <copyright file="AnalyticsHit.cs" company="Kolky">
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
    using Allium.Interfaces.Parameters;
    using Interfaces;
    using Validation;

    /// <summary>
    /// Analytics Hit, to fill more parameters before sending.
    /// </summary>
    /// <typeparam name="T">Type of Hit Parameters.</typeparam>
    internal class AnalyticsHit<T> : IAnalyticsHit<T>
        where T : IHitParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsHit{T}"/> class.
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="parameters">parameters</param>
        public AnalyticsHit(AnalyticsSession session, T parameters)
        {
            Requires.NotNull(session, nameof(session));

            this.Session = session;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        public AnalyticsSession Session { get; private set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        public T Parameters { get; private set; }

        /// <summary>
        /// Send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Send()
        {
            return await this.Session.Client.Send(this.Parameters.Clone());
        }
    }
}
