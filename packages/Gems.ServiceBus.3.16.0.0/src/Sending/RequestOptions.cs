// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestOptions.cs" company="">
//   
// </copyright>
// <summary>
//   The request options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Sending
{
    using System;

    using Gems.ServiceBus.Helpers;

    /// <summary>
    /// The request options.
    /// </summary>
    public class RequestOptions : PublishingOptions
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the timeout.
        /// </summary>
        public Maybe<TimeSpan?> Timeout { get; set; }

        #endregion
    }
}