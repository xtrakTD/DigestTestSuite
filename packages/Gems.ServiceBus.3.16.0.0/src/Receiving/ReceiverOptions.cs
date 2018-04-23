﻿using System;

using Gems.ServiceBus.Configuration;
using Gems.ServiceBus.Helpers;

namespace Gems.ServiceBus.Receiving
{
    /// <summary>
    /// Настройки получателя.
    /// </summary>
    public class ReceiverOptions : BusOptions
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReceiverOptions"/>.
        /// </summary>
        public ReceiverOptions()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReceiverOptions"/>.
        /// </summary>
        /// <param name="parent">Базовые настройки.</param>
        public ReceiverOptions(BusOptions parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Устанавливает необходимость явного подтверждения успешной обработки.
        /// </summary>
        public Maybe<bool> AcceptIsRequired { protected get; set; }

        /// <summary>
        /// Построитель порта получения входящих сообщений.
        /// </summary>
        public Maybe<Func<ISubscriptionEndpointBuilder, ISubscriptionEndpoint>> EndpointBuilder { protected get; set; }

        /// <summary>
        /// Обработчик сообщений, чья доставка завершилась провалом.
        /// </summary>
        public Maybe<IFailedDeliveryStrategy> FailedDeliveryStrategy { protected get; set; }

        /// <summary>
        /// Количество одновременных обработчиков сообщений.
        /// </summary>
        public Maybe<uint> ParallelismLevel { protected get; set; }

        /// <summary>
        /// Обработчик сообщений, для которых не найден потребитель.
        /// </summary>
        public Maybe<IUnhandledDeliveryStrategy> UnhandledDeliveryStrategy { protected get; set; }

        /// <summary>
        /// Хранилище заголовков входящего сообщения.
        /// </summary>
        public Maybe<IIncomingMessageHeaderStorage> IncomingMessageHeaderStorage { protected get; set; }

        /// <summary>
        /// Создает новый экземпляр настроек как копию существующего.
        /// </summary>
        /// <returns>
        /// Новый экземпляр настроек.
        /// </returns>
        public override BusOptions Derive()
        {
            return new ReceiverOptions(this);
        }

        /// <summary>
        /// Возвращает построитель порта получаемых сообщений.
        /// </summary>
        /// <returns>
        /// Построитель порта получаемых сообщений.
        /// </returns>
        public Maybe<Func<ISubscriptionEndpointBuilder, ISubscriptionEndpoint>> GetEndpointBuilder()
        {
            return this.Pick(o => ((ReceiverOptions)o).EndpointBuilder);
        }

        /// <summary>
        /// Возвращает обработчик сообщений, для которых доставка завершилась провалом.
        /// </summary>
        /// <returns>
        /// Обработчик сообщений, для которых доставка завершилась провалом.
        /// </returns>
        public Maybe<IFailedDeliveryStrategy> GetFailedDeliveryStrategy()
        {
            return this.Pick(o => ((ReceiverOptions)o).FailedDeliveryStrategy);
        }

        /// <summary>
        /// Возвращает количество одновременных обработчиков сообщений.
        /// </summary>
        /// <returns>
        /// Количество одновременных обработчиков сообщений.
        /// </returns>
        public Maybe<uint> GetParallelismLevel()
        {
            return this.Pick(o => ((ReceiverOptions)o).ParallelismLevel);
        }

        /// <summary>
        /// Возвращает обработчик сообщений, для которых не удалось найти потребителя.
        /// </summary>
        /// <returns>
        /// Обработчик сообщений, для которых не удалось найти потребителя.
        /// </returns>
        public Maybe<IUnhandledDeliveryStrategy> GetUnhandledDeliveryStrategy()
        {
            return this.Pick(o => ((ReceiverOptions)o).UnhandledDeliveryStrategy);
        }

        /// <summary>
        /// Возвращает признак необходимости явно подтверждать успешно обработанные сообщения.
        /// </summary>
        /// <returns>
        /// Если <c>true</c> - тогда необходимо подтверждать успешно обработанные сообщения, иначе - <c>false</c>.
        /// </returns>
        public Maybe<bool> IsAcceptRequired()
        {
            return this.Pick(o => ((ReceiverOptions)o).AcceptIsRequired);
        }

        /// <summary>
        /// Возвращает хранилище заголовков входящего сообщения.
        /// </summary>
        /// <returns>Хранилище заголовков входящего сообщения.</returns>
        public Maybe<IIncomingMessageHeaderStorage> GetIncomingMessageHeaderStorage()
        {
            return this.Pick(o => ((ReceiverOptions)o).IncomingMessageHeaderStorage);
        }
    }
}