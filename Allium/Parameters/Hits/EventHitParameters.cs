// <copyright file="EventHitParameters.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

namespace Allium.Parameters.Hits
{
    using Attributes;
    using Enums;
    using Interfaces.Parameters;
    using Interfaces.Parameters.Hits;
    using Validation;

    /// <summary>
    /// Parameters for an event hit.
    /// </summary>
    internal class EventHitParameters : HitParameters, IEventParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="category">category</param>
        /// <param name="action">action</param>
        public EventHitParameters(IGeneralParameters copy, string category, string action)
            : base(copy)
        {
            Requires.NotNullOrWhiteSpace(category, nameof(category));
            Requires.NotNullOrWhiteSpace(action, nameof(action));

            this.EventCategory = category;
            this.EventAction = action;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Event; }
        }

        /// <summary>
        /// Gets or sets the event category.
        /// </summary>
        [Parameter("ec", Required = true, MaxLength = 150)]
        public string EventCategory { get; set; }

        /// <summary>
        /// Gets or sets the event action.
        /// </summary>
        [Parameter("ea", Required = true, MaxLength = 500)]
        public string EventAction { get; set; }

        /// <summary>
        /// Gets or sets the event label.
        /// </summary>
        [Parameter("el", MaxLength = 500)]
        public string EventLabel { get; set; }

        /// <summary>
        /// Gets or sets the event value. Values must be non-negative.
        /// </summary>
        [Parameter("ev")]
        public int EventValue { get; set; }
    }
}
