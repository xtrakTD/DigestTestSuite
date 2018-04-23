﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReceiver.cs" company="">
//   
// </copyright>
// <summary>
//   The Receiver interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Receiving
{
    using Gems.ServiceBus.Configuration;
    using Gems.ServiceBus.Receiving.Consumers;

    /// <summary>
    /// The Receiver interface.
    /// </summary>
    public interface IReceiver : IBusComponent
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register consumer.
        /// </summary>
        /// <param name="label">
        /// The label.
        /// </param>
        /// <param name="consumer">
        /// The consumer.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        void RegisterConsumer<T>(MessageLabel label, IConsumerOf<T> consumer) where T : class;

        #endregion
    }
}