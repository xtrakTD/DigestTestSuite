namespace Gems.ServiceBus.Configuration
{
    using System.Collections.Generic;

    using Gems.ServiceBus.Filters;
    using Gems.ServiceBus.Serialization;

    /// <summary>
    ///   Конфигурация клиента шины.
    /// </summary>
    public interface IBusConfiguration
    {
        #region Public Properties

        /// <summary>
        ///   Строка подключения к транспорту (брокеру).
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        IEnumerable<IMessageExchangeFilter> Filters { get; }

        /// <summary>
        ///   Обработчик метки сообщений.
        /// </summary>
        IMessageLabelHandler MessageLabelHandler { get; }

        /// <summary>
        ///   Сериализатор сообщений.
        /// </summary>
        IPayloadConverter Serializer { get; }

        #endregion
    }
}