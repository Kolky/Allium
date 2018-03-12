// <copyright file="MainWindow.xaml.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Example
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using Interfaces;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string trackingId;
        private bool sendToDebugServer;
        private IAnalyticsSession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.SendToDebugServer = false;
            this.DataContext = this;
            this.Closed += (sender, e) => this.Session?.Dispose();
            this.InitializeComponent();
        }

        /// <summary>
        /// Event when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets a value indicating whether the sesion was created.
        /// </summary>
        public bool SessionCreated
        {
            get { return this.Session != null; }
        }

        /// <summary>
        /// Gets or sets the tracking id.
        /// </summary>
        public string TrackingId
        {
            get
            {
                return this.trackingId;
            }

            set
            {
                this.trackingId = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to send to debug server.
        /// </summary>
        public bool SendToDebugServer
        {
            get
            {
                return this.sendToDebugServer;
            }

            set
            {
                this.sendToDebugServer = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        public IAnalyticsSession Session
        {
            get
            {
                return this.session;
            }

            private set
            {
                this.session = value;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged(nameof(this.SessionCreated));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "PropertyName")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCreateSession(object sender, RoutedEventArgs e)
        {
            if (!this.SessionCreated && !string.IsNullOrWhiteSpace(this.TrackingId))
            {
                this.Session = new AnalyticsSession(this.TrackingId, this.SendToDebugServer);
                this.Session.Parameters.App.ApplicationName = "Allium.Example";
                this.Session.Parameters.App.ApplicationId = "com.kolky.Allium";

                this.Session.Parameters.CustomDimensions.Add(Environment.MachineName);
            }
        }

        private async void OnStart(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.Start();
            }
        }

        private async void OnEvent(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated && this.Session != null)
            {
                var hit = this.Session.TrackEventHit(this.EventCategory.Text, this.EventAction.Text);
                hit.Parameters.EventLabel = this.EventLabel.Text;
                if (int.TryParse(this.EventValue.Text, out int eventValue))
                {
                    hit.Parameters.EventValue = eventValue;
                }

                await hit.Send();
            }
        }

        private async void OnException(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.TrackExceptionHit(new NotImplementedException(), false);
            }
        }

        private async void OnPageView(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.TrackPageViewHit("HostName", "Path");
            }
        }

        private async void OnScreenView(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.TrackScreenViewHit("Screen");
            }
        }

        private async void OnSocial(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.TrackSocialHit("Network", "Action", "Target");
            }
        }

        private async void OnTimer(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.TrackTimerHit("Category", "Name")?.FinishAndSend();
            }
        }

        private async void OnFinish(object sender, RoutedEventArgs e)
        {
            if (this.SessionCreated)
            {
                await this.Session?.Finish();

                this.Session = null;
            }
        }
    }
}
