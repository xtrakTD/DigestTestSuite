namespace Gems.ServiceBus.Transport.RabbitMQ
{
    using Gems.ServiceBus.Configuration;
    using Gems.ServiceBus.Helpers;
    using Gems.ServiceBus.Receiving;

    /// <summary>
    /// The rabbit receiver options.
    /// </summary>
    public class RabbitReceiverOptions : ReceiverOptions
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RabbitReceiverOptions"/>.
        /// </summary>
        public RabbitReceiverOptions()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RabbitReceiverOptions"/>.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public RabbitReceiverOptions(BusOptions parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Gets or sets the qo s.
        /// </summary>
        public Maybe<QoSParams> QoS { protected get; set; }

        /// <summary>
        /// The derive.
        /// </summary>
        /// <returns>
        /// The <see cref="BusOptions"/>.
        /// </returns>
        public override BusOptions Derive()
        {
            return new RabbitReceiverOptions(this);
        }

        /// <summary>
        /// The get qo s.
        /// </summary>
        /// <returns>
        /// The <see cref="Maybe{T}"/>.
        /// </returns>
        public Maybe<QoSParams> GetQoS()
        {
            return this.Pick(o => ((RabbitReceiverOptions)o).QoS);
        }
    }
}