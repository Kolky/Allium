// <copyright file="CloneableHitParameters.cs" company="Kolky">
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
    using System;
    using Allium.Interfaces.Parameters.Hits;
    using Attributes;
    using Enums;
    using Interfaces.Parameters;

    /// <summary>
    /// Base for hit parameters.
    /// </summary>
    /// <typeparam name="T">Type of cloneable parameters.</typeparam>
    internal abstract class CloneableHitParameters<T> : GeneralParameters, ICloneableHitParameters<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloneableHitParameters{T}"/> class.
        /// Copy constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected CloneableHitParameters(IGeneralParameters copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloneableHitParameters{T}"/> class.
        /// Clone constructor!
        /// </summary>
        /// <param name="copy">copy</param>
        protected CloneableHitParameters(ICloneableHitParameters<T> copy)
            : base(copy)
        {
            this.NonInteractionHit = copy.NonInteractionHit;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        [Parameter("t", Required = true)]
        public abstract HitType HitType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether a hit must be considered non-interactive.
        /// </summary>
        [Parameter("ni")]
        public bool? NonInteractionHit { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public abstract T Clone();

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
