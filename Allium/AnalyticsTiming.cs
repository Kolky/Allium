// <copyright file="AnalyticsTiming.cs" company="Kolky">
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
    using Interfaces;
    using Interfaces.Parameters.Hits;
    using Parameters.Hits;
    using Validation;

    /// <summary>
    /// Analytics Timing.
    /// </summary>
    internal class AnalyticsTiming : IAnalyticsTiming
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsTiming"/> class.
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="category">category</param>
        /// <param name="name">name</param>
        public AnalyticsTiming(AnalyticsSession session, string category, string name)
        {
            Requires.NotNull(session, nameof(session));

            this.Session = session;
            this.Parameters = new TimingHitParameters(this.Session.Parameters.Clone(), category, name);
            this.Started = DateTime.Now;
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        public AnalyticsSession Session { get; private set; }

        /// <summary>
        /// Gets the parameters for this timing.
        /// </summary>
        public ITimingParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the moment the timing was started.
        /// </summary>
        public DateTime Started { get; private set; }

        /// <summary>
        /// Gets the moment the timing was finished.
        /// </summary>
        public DateTime? Finished { get; private set; }

        /// <summary>
        /// Gets the time elapsed.
        /// </summary>
        public TimeSpan? Elapsed
        {
            get
            {
                if (this.Finished.HasValue)
                {
                    return this.Finished.Value - this.Started;
                }

                return null;
            }
        }

        /// <summary>
        /// Finish the timing measurement.
        /// NOTE: Sequential calls updates the finished value.
        /// </summary>
        public void Finish()
        {
            this.Finished = DateTime.Now;
        }

        /// <summary>
        /// Finish the timing measurement and directly send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> FinishAndSend()
        {
            this.Finish();
            return await this.Send();
        }

        /// <summary>
        /// If the timing is finished, send the parameters.
        /// </summary>
        /// <returns>Analytics Results</returns>
        public async Task<IAnalyticsResult> Send()
        {
            var parameters = new TimingHitParameters(this.Parameters.Clone(), this.Parameters.UserTimingCategory, this.Parameters.UserTimingVariableName);
            parameters.UserTimingTime = this.Elapsed.HasValue ? (int)this.Elapsed.Value.TotalMilliseconds : 0;
            return await this.Session.Client.Send(parameters);
        }

        /// <summary>
        /// Dispose; this finished the timer and directly sends the parameters.
        /// </summary>
        public void Dispose()
        {
            Task.Run(this.FinishAndSend);
        }
    }
}
