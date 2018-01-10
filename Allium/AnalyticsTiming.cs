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
        /// Gets a value indicating whether the timing was sent.
        /// </summary>
        internal bool TimingSend { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the timing was disposed.
        /// </summary>
        internal bool Disposed { get; private set; }

        /// <summary>
        /// Finish the timing measurement.
        /// NOTE: Sequential calls updates the finished value (only if not already send).
        /// </summary>
        public void Finish()
        {
            if (!this.Finished.HasValue && !this.TimingSend)
            {
                this.Finished = DateTime.Now;
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
            if (this.Disposed)
            {
                throw new ObjectDisposedException(nameof(AnalyticsTiming));
            }

            if (!this.Finished.HasValue)
            {
                return new AnalyticsResult(new AnalyticsException(Resources.HasNotYetFinishedTiming));
            }

            if (this.TimingSend)
            {
                return new AnalyticsResult(new AnalyticsException(Resources.HasAlreadySentTiming));
            }

            var parameters = new TimingHitParameters(this.Parameters.Clone(), this.Parameters.UserTimingCategory, this.Parameters.UserTimingVariableName);
            parameters.UserTimingTime = (int)this.Elapsed.Value.TotalMilliseconds;
            var result = await this.Session.Client.Send(parameters);
            this.TimingSend = true;
            return result;
        }

        /// <summary>
        /// Dispose; this finishes the timer (if this was not already done) and directly sends the parameters.
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
                    // Finish and sent session, but do nothing otherwise (it might fail)
                    if (!this.TimingSend)
                    {
                        var sendTask = this.FinishAndSend();
                        sendTask.Wait();

                        // Throw exception if sending fails.. (otherwise nobody would ever find out!)
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
