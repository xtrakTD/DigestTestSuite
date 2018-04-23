﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITicketTimer.cs" company="">
//   
// </copyright>
// <summary>
//   The TicketTimer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Helpers.Timing
{
    using System;

    /// <summary>
    /// The TicketTimer interface.
    /// </summary>
    internal interface ITicketTimer : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The acquire.
        /// </summary>
        /// <param name="span">
        /// The span.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        long Acquire(TimeSpan span, Action callback);

        /// <summary>
        /// The cancel.
        /// </summary>
        /// <param name="ticket">
        /// The ticket.
        /// </param>
        void Cancel(long ticket);

        #endregion
    }
}