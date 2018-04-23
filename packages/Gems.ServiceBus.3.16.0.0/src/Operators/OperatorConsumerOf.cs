namespace Gems.ServiceBus.Operators
{
    using Gems.ServiceBus.Helpers;
    using Gems.ServiceBus.Receiving;
    using Gems.ServiceBus.Receiving.Consumers;

    /// <summary>
    /// Operator based consumer.
    /// </summary>
    /// <typeparam name="T">Payload type.</typeparam>
    public class OperatorConsumerOf<T> : IConsumerOf<T>
        where T : class
    {
        private readonly IMessageOperator @operator;

        /// <summary>
        /// Builds consumer.
        /// </summary>
        /// <param name="operator">Оператор.</param>
        public OperatorConsumerOf(IMessageOperator @operator)
        {
            this.@operator = @operator;
        }

        /// <summary>
        /// Handles incoming message.
        /// </summary>
        /// <param name="context">Consuming context.</param>
        public void Handle(IConsumingContext<T> context)
        {
            BusProcessingContext.Current = new BusProcessingContext(((IDeliveryContext)context).Delivery);

            this.@operator
                .Apply(context.Message)
                .ForEach(m => context.Bus.Emit(m.Label, m.Payload, m.Headers));

            context.Accept();
        }
    }
}