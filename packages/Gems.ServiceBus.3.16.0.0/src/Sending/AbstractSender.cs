﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Common.Logging;

using Gems.ServiceBus.Configuration;
using Gems.ServiceBus.Filters;
using Gems.ServiceBus.Helpers;

namespace Gems.ServiceBus.Sending
{
    /// <summary>
    /// Отправитель, который не знает о транспортном уровне.
    /// </summary>
    internal abstract class AbstractSender : ISender
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Фильтры обработки сообщений.
        /// </summary>
        private readonly IList<IMessageExchangeFilter> filters;

        /// <summary>
        /// Конечная точка, от имени которой работает отправитель.
        /// </summary>
        private readonly IEndpoint endpoint;

        /// <summary>
        /// Последняя точка в пути сообщения.
        /// </summary>
        private readonly string breadCrumbsTail;

        // TODO: refactor, don't copy filters

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AbstractSender"/>.
        /// </summary>
        /// <param name="endpoint">Конечная точка, от имени которой работает отправитель.</param>
        /// <param name="configuration">Конфигурация отправителя.</param>
        /// <param name="filters">Список фильтров обработки сообщения.</param>
        protected AbstractSender(IEndpoint endpoint, ISenderConfiguration configuration, IEnumerable<IMessageExchangeFilter> filters)
        {
            this.endpoint = endpoint;
            this.breadCrumbsTail = ";" + endpoint.Address;

            this.filters = new SendingExchangeFilter(this.InternalSend)
                .ToEnumerable()
                .Union(filters)
                .ToList();

            this.Configuration = configuration;
        }

        /// <summary>
        /// Конфигурация отправителя.
        /// </summary>
        public ISenderConfiguration Configuration { get; private set; }

        /// <summary>
        /// Есть ли сбои в работе отправителя.
        /// </summary>
        public abstract bool IsHealthy { get; }

        /// <summary>
        /// Проверяет возможность создать маршрут для метки сообщения.
        /// </summary>
        /// <param name="label">Метка сообщения, для которой нужно создать маршрут.</param>
        /// <returns><c>true</c> - если можно создать маршрут.</returns>
        public virtual bool CanRoute(MessageLabel label)
        {
            return label.IsAlias ? label.Name.Equals(this.Configuration.Alias) : label.Equals(this.Configuration.Label);
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Отправляет сообщение в формате запрос-ответ.
        /// </summary>
        /// <param name="payload">Сообщение запроса.</param>
        /// <param name="headers">Заголовки запроса.</param>
        /// <typeparam name="T">Тип сообщения ответа.</typeparam>
        /// <returns>Задача выполнения запроса.</returns>
        public Task<T> Request<T>(object payload, IDictionary<string, object> headers) where T : class
        {
            var message = new Message(this.Configuration.Label, headers, payload);

            var exchange = new MessageExchange(message, typeof(T));
            var invoker = new MessageExchangeFilterInvoker(this.filters);

            return invoker.Process(exchange)
                .ContinueWith(
                    t =>
                        {
                            t.Result.ThrowIfFailed();
                            return (T)t.Result.In.Payload;
                        });
        }

        /// <summary>
        /// Отправляет сообщение в формате запрос-ответ.
        /// </summary>
        /// <param name="payload">Сообщение запроса.</param>
        /// <param name="options">Параметры запроса.</param>
        /// <typeparam name="T">Тип сообщения ответа.</typeparam>
        /// <returns>Задача выполнения запроса.</returns>
        public Task<T> Request<T>(object payload, RequestOptions options) where T : class
        {
            var headers = this.ApplyOptions(options);
            if (!headers.ContainsKey(Headers.CorrelationId))
            {
                headers[Headers.CorrelationId] = Guid.NewGuid().ToString("n");
            }

            return this.Request<T>(payload, headers);
        }

            /// <summary>
        /// Отправляет одностороннее сообщение.
        /// </summary>
        /// <param name="payload">Тело сообщения.</param>
        /// <param name="headers">Заголовки сообщения.</param>
        /// <returns>Задача выполнения отправки сообщения.</returns>
        [Obsolete("Необходимо использовать метод Send с указанием метки сообщения.")]
        public Task Send(object payload, IDictionary<string, object> headers)
        {
            var message = new Message(this.Configuration.Label, headers, payload);

            return this.ProcessFilter(message);
        }

        /// <summary>
        /// Отправляет одностороннее сообщение.
        /// </summary>
        /// <param name="payload">Тело сообщения.</param>
        /// <param name="options">Заголовки сообщения.</param>
        /// <returns>Задача выполнения отправки сообщения.</returns>
        [Obsolete("Необходимо использовать метод Send с указанием метки сообщения.")]
        public Task Send(object payload, PublishingOptions options)
        {
            return this.Send(payload, this.ApplyOptions(options));
        }

        /// <summary>
        /// Отправляет сообщение в формате запрос-ответ.
        /// </summary>
        /// <param name="label">Метка отправляемого запроса.</param>
        /// <param name="payload">Сообщение запроса.</param>
        /// <param name="options">Параметры запроса.</param>
        /// <typeparam name="T">Тип сообщения ответа.</typeparam>
        /// <returns>Задача выполнения запроса.</returns>
        public Task<T> Request<T>(MessageLabel label, object payload, RequestOptions options) where T : class
        {
            var headers = this.ApplyOptions(options);

            return this.Request<T>(label, payload, headers);
        }

        /// <summary>
        /// Отправляет сообщение в формате запрос-ответ.
        /// </summary>
        /// <param name="label">Метка отправляемого запроса.</param>
        /// <param name="payload">Сообщение запроса.</param>
        /// <param name="headers">Заголовки запроса.</param>
        /// <typeparam name="T">Тип сообщения ответа.</typeparam>
        /// <returns>Задача выполнения запроса.</returns>
        public Task<T> Request<T>(MessageLabel label, object payload, IDictionary<string, object> headers) where T : class
        {
            if (!headers.ContainsKey(Headers.CorrelationId))
            {
                headers[Headers.CorrelationId] = Guid.NewGuid().ToString("n");
            }

            var message = new Message(this.Configuration.Label.Equals(MessageLabel.Any) ? label : this.Configuration.Label, headers, payload);

            var exchange = new MessageExchange(message, typeof(T));
            var invoker = new MessageExchangeFilterInvoker(this.filters);

            return invoker.Process(exchange)
                .ContinueWith(
                    t =>
                    {
                        t.Result.ThrowIfFailed();
                        return (T)t.Result.In.Payload;
                    });
        }

        /// <summary>
        /// Отправляет одностороннее сообщение.
        /// </summary>
        /// <param name="label">Метка отправляемого сообщения.</param>
        /// <param name="payload">Тело сообщения.</param>
        /// <param name="headers">Заголовки сообщения.</param>
        /// <returns>Задача выполнения отправки сообщения.</returns>
        public Task Send(MessageLabel label, object payload, IDictionary<string, object> headers)
        {
            var message = new Message(this.Configuration.Label.Equals(MessageLabel.Any) ? label : this.Configuration.Label, headers, payload);

            return this.ProcessFilter(message);
        }

        /// <summary>
        /// Отправляет одностороннее сообщение.
        /// </summary>
        /// <param name="label">Метка отправляемого сообщения.</param>
        /// <param name="payload">Тело сообщения.</param>
        /// <param name="options">Заголовки сообщения.</param>
        /// <returns>Задача выполнения отправки сообщения.</returns>
        public Task Send(MessageLabel label, object payload, PublishingOptions options)
        {
            return this.Send(label, payload, this.ApplyOptions(options));
        }

        /// <summary>
        /// Запускает отправитель.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Останавливает отправитель.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Фильтр обработки сообщения, который отсылает сообщение.
        /// </summary>
        /// <param name="exchange">Отсылаемое сообщение.</param>
        /// <returns>Задача выполнения фильтра.</returns>
        protected abstract Task<MessageExchange> InternalSend(MessageExchange exchange);

        /// <summary>
        /// Обрабатывает сообщение с помощью зарегистрированных фильтров.
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение.</param>
        /// <returns>Задача обработки сообщения с помощью фильтров.</returns>
        private Task ProcessFilter(IMessage message)
        {
            var exchange = new MessageExchange(message, null);
            var invoker = new MessageExchangeFilterInvoker(this.filters);

            return invoker.Process(exchange);
        }

        /// <summary>
        /// Конвертирует настройки публикации сообщения в заголовки сообщения.
        /// </summary>
        /// <param name="options">Настройки публикации сообщения.</param>
        /// <returns>Заголовки сообщения.</returns>
        private IDictionary<string, object> ApplyOptions(PublishingOptions options)
        {
            var storage = this.Configuration.Options.GetIncomingMessageHeaderStorage().Value;
            var headers = storage.Load() ?? new Dictionary<string, object>();
            if (!headers.ContainsKey(Headers.Breadcrumbs))
            {
                headers[Headers.Breadcrumbs] = this.endpoint.Address;
            }
            else
            {
                headers[Headers.Breadcrumbs] = Headers.GetString(headers, Headers.Breadcrumbs) + this.breadCrumbsTail;
            }

            if (!headers.ContainsKey(Headers.OriginalMessageId))
            {
                headers[Headers.OriginalMessageId] = Guid.NewGuid().ToString("n");
            }

            Logger.Trace(m => m("Идентификатор первого сообщения [{0}].", Headers.GetString(headers, Headers.OriginalMessageId)));

            Maybe<bool> persist = BusOptions.Pick(options.Persistently, this.Configuration.Options.IsPersistently());
            if (persist != null && persist.HasValue)
            {
                headers[Headers.Persist] = persist.Value;
            }

            Maybe<TimeSpan?> ttl = BusOptions.Pick(options.Ttl, this.Configuration.Options.GetTtl());
            if (ttl != null && ttl.HasValue)
            {
                headers[Headers.Ttl] = ttl.Value;
            }

            return headers;
        }

        /// <summary>
        /// Конвертирует настройки публикации сообщения в заголовки сообщения.
        /// </summary>
        /// <param name="requestOptions">Настройки публикации сообщения.</param>
        /// <returns>Заголовки сообщения.</returns>
        private IDictionary<string, object> ApplyOptions(RequestOptions requestOptions)
        {
            IDictionary<string, object> headers = this.ApplyOptions(requestOptions as PublishingOptions);

            Maybe<TimeSpan?> timeout = BusOptions.Pick(requestOptions.Timeout, this.Configuration.Options.GetRequestTimeout());
            if (timeout != null && timeout.HasValue)
            {
                headers[Headers.Timeout] = timeout.Value;
            }

            return headers;
        }
    }
}