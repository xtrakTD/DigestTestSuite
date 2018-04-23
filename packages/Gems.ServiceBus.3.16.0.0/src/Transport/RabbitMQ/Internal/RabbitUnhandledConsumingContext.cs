﻿namespace Gems.ServiceBus.Transport.RabbitMQ.Internal
{
    using Gems.ServiceBus.Receiving;

    /// <summary>
    /// The rabbit unhandled consuming context.
    /// </summary>
    internal class RabbitUnhandledConsumingContext : FaultedConsumingContext, IUnhandledConsumingContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RabbitUnhandledConsumingContext"/>.
        /// </summary>
        /// <param name="delivery">
        /// The delivery.
        /// </param>
        public RabbitUnhandledConsumingContext(RabbitDelivery delivery)
            : base(delivery)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build fault message.
        /// </summary>
        /// <returns>
        /// The <see cref="FaultMessage"/>.
        /// </returns>
        public override FaultMessage BuildFaultMessage()
        {
            return new RabbitFaultMessage((RabbitDelivery)this.Delivery);
        }

        #endregion
    }
}