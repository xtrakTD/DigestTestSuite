﻿namespace Gems.ServiceBus.Transport.RabbitMQ.Internal
{
    using Gems.ServiceBus.Receiving;
    using Gems.ServiceBus.Sending;
    using Gems.ServiceBus.Validation;

    /// <summary>
    /// The listener configuration.
    /// </summary>
    internal class ListenerConfiguration
    {
        #region Public Properties

        /// <summary>
        /// Gets the callback route resolver.
        /// </summary>
        public IRouteResolver CallbackRouteResolver
        {
            get
            {
                return this.SubscriptionEndpoint.CallbackRouteResolver;
            }
        }

        /// <summary>
        /// Gets or sets the failed delivery strategy.
        /// </summary>
        public IFailedDeliveryStrategy FailedDeliveryStrategy { get; set; }

        /// <summary>
        /// Gets the listening source.
        /// </summary>
        public IListeningSource ListeningSource
        {
            get
            {
                return this.SubscriptionEndpoint.ListeningSource;
            }
        }

        /// <summary>
        /// Gets or sets the parallelism level.
        /// </summary>
        public uint ParallelismLevel { get; set; }

        /// <summary>
        /// Gets or sets the qos.
        /// </summary>
        public QoSParams Qos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether requires accept.
        /// </summary>
        public bool RequiresAccept { get; set; }

        /// <summary>
        /// Gets or sets the subscription endpoint.
        /// </summary>
        public ISubscriptionEndpoint SubscriptionEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the unhandled delivery strategy.
        /// </summary>
        public IUnhandledDeliveryStrategy UnhandledDeliveryStrategy { get; set; }

        /// <summary>
        /// Gets or sets the validator registry.
        /// </summary>
        public MessageValidatorRegistry ValidatorRegistry { get; set; }

        #endregion
    }
}