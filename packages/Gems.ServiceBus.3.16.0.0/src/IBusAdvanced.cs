namespace Gems.ServiceBus
{
    using System.Collections.Generic;

    using Gems.ServiceBus.Configuration;
    using Gems.ServiceBus.Receiving;
    using Gems.ServiceBus.Sending;

    /// <summary>
    ///   Расширенный интерфейс шины для целей отладки.
    /// </summary>
    public interface IBusAdvanced
    {
        #region Public Properties

        /// <summary>
        ///   Трекер компонентов шины, зависящих от фактического подключения к брокеру.
        /// </summary>
        IBusComponentTracker ComponentTracker { get; }

        /// <summary>
        ///   Список получателей сообщений (по одному на каждую объявленную подписку).
        /// </summary>
        IEnumerable<IReceiver> Receivers { get; }

        /// <summary>
        ///   Список отправителей сообщений (по одному на каждое объявленное сообщение).
        /// </summary>
        IEnumerable<ISender> Senders { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Открытие/создание нового канала, используя текущее подключение.
        /// </summary>
        /// <returns>
        /// The <see cref="IChannel"/>.
        /// </returns>
        IChannel OpenChannel();

        /// <summary>
        ///   Принудительный перезапуск клиента шины.
        /// </summary>
        void Panic();

        #endregion
    }
}