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
    using Properties;
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
            this.TimingFinished = false;
            this.TimingSend = false;
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
        /// Gets a value indicating whether the timing was finished.
        /// </summary>
        internal bool TimingFinished { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the timing was sent.
        /// </summary>
        internal bool TimingSend { get; private set; }

        /// <summary>
        /// Finish the timing measurement.
        /// NOTE: Sequential calls updates the finished value.
        /// </summary>
        public void Finish()
        {
            if (!this.TimingFinished && !this.TimingSend)
            {
                this.Finished = DateTime.Now;
                this.TimingFinished = true;
            }
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
            if (this.TimingFinished && !this.TimingSend)
            {
                var parameters = new TimingHitParameters(this.Parameters.Clone(), this.Parameters.UserTimingCategory, this.Parameters.UserTimingVariableName);
                parameters.UserTimingTime = this.Elapsed.HasValue ? (int)this.Elapsed.Value.TotalMilliseconds : 0;
                var result = await this.Session.Client.Send(parameters);
                this.TimingSend = true;
                return result;
            }

            return new AnalyticsResult(new AnalyticsException(Resources.HasNotYetFinishedTiming));
        }

        /// <summary>
        /// Dispose; this finished the timer and directly sends the parameters.
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
            if (disposing)
            {
                if (!this.TimingFinished || !this.TimingSend)
                {
                    var sendTask = this.FinishAndSend();
                    sendTask.Wait();
                }
            }
        }
    }
}
