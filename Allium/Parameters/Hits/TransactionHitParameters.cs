// <copyright file="TransactionHitParameters.cs" company="Kolky">
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
    using Allium.Enums;
    using Allium.Interfaces.Parameters;
    using Allium.Interfaces.Parameters.Hits;
    using Allium.Parameters.Attributes;

    /// <summary>
    /// Parameters for an E-Commerce transaction hit.
    /// </summary>
    internal class TransactionHitParameters : HitParameters, IEcommerceTransactionParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionHitParameters"/> class.
        /// </summary>
        /// <param name="copy">copy</param>
        /// <param name="transactionId">transactionId</param>
        public TransactionHitParameters(IGeneralParameters copy, string transactionId)
            : base(copy)
        {
            this.TransactionId = transactionId;
        }

        private TransactionHitParameters(TransactionHitParameters copy)
            : base(copy)
        {
            this.TransactionId = copy.TransactionId;
            this.TransactionAffiliation = copy.TransactionAffiliation;
            this.TransactionRevenue = copy.TransactionRevenue;
            this.TransactionShipping = copy.TransactionShipping;
            this.TransactionTax = copy.TransactionTax;
        }

        /// <summary>
        /// Gets the type of hit.
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Transaction; }
        }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        [Parameter("ti", MaxLength = 500)]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction affiliation.
        /// </summary>
        [Parameter("ta", MaxLength = 500)]
        public string TransactionAffiliation { get; set; }

        /// <summary>
        /// Gets or sets the transaction revenue.
        /// </summary>
        [Parameter("tr")]
        public double TransactionRevenue { get; set; }

        /// <summary>
        /// Gets or sets the transaction shipping costs.
        /// </summary>
        [Parameter("ts")]
        public double TransactionShipping { get; set; }

        /// <summary>
        /// Gets or sets the transaction tax.
        /// </summary>
        [Parameter("tt")]
        public double TransactionTax { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>Clone</returns>
        public override IGeneralParameters Clone()
        {
            return new TransactionHitParameters(this);
        }
    }
}
