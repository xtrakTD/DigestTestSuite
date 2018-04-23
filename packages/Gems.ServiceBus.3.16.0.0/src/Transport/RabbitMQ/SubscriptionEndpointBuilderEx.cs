﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionEndpointBuilderEx.cs" company="">
//   
// </copyright>
// <summary>
//   The subscription endpoint builder ex.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Transport.RabbitMQ
{
    using Gems.ServiceBus.Receiving;
    using Gems.ServiceBus.Transport.RabbitMQ.Topology;

    /// <summary>
    /// The subscription endpoint builder ex.
    /// </summary>
    public static class SubscriptionEndpointBuilderEx
    {
        #region Public Methods and Operators

        /// <summary>
        /// The listen to.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="queueName">
        /// The queue name.
        /// </param>
        /// <returns>
        /// The <see cref="ISubscriptionEndpoint"/>.
        /// </returns>
        public static ISubscriptionEndpoint ListenTo(this ISubscriptionEndpointBuilder builder, string queueName)
        {
            return builder.ListenTo(
                Queue.Named(queueName).
                    Instance);
        }

        #endregion
    }
}