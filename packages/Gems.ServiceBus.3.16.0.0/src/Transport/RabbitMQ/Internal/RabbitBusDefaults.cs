﻿namespace Gems.ServiceBus.Transport.RabbitMQ.Internal
{
    using System;

    using Gems.ServiceBus.Receiving;
    using Gems.ServiceBus.Sending;
    using Gems.ServiceBus.Transport.RabbitMQ.Topology;

    /// <summary>
    /// The rabbit bus defaults.
    /// </summary>
    internal static class RabbitBusDefaults
    {
        #region Static Fields

        /// <summary>
        /// The route resolver builder.
        /// </summary>
        public static Func<IRouteResolverBuilder, IRouteResolver> RouteResolverBuilder = RouteResolverBuilderImpl;

        /// <summary>
        /// The subscription endpoint builder.
        /// </summary>
        public static Func<ISubscriptionEndpointBuilder, ISubscriptionEndpoint> SubscriptionEndpointBuilder = SubscriptionEndpointBuilderImpl;

        #endregion

        #region Methods

        /// <summary>
        /// The route resolver builder impl.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The <see cref="IRouteResolver"/>.
        /// </returns>
        private static IRouteResolver RouteResolverBuilderImpl(IRouteResolverBuilder builder)
        {
            string label = builder.Sender.Label.Name;

            Exchange exchange = builder.Topology.Declare(
                Exchange.Named(label).
                    Durable.Fanout);

            return new StaticRouteResolver(exchange);
        }

        /// <summary>
        /// The subscription endpoint builder impl.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The <see cref="ISubscriptionEndpoint"/>.
        /// </returns>
        private static ISubscriptionEndpoint SubscriptionEndpointBuilderImpl(ISubscriptionEndpointBuilder builder)
        {
            string label = builder.Receiver.Label.Name;

            string queueName = builder.Endpoint.Address + "." + label;

            Queue queue = builder.Topology.Declare(
                Queue.Named(queueName).
                    Durable);
            Exchange exchange = builder.Topology.Declare(
                Exchange.Named(label).
                    Durable.Fanout);

            builder.Topology.Bind(exchange, queue);

            return builder.ListenTo(queue, exchange);
        }

        #endregion
    }
}