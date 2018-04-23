﻿using System.Collections.Generic;
using System.Text;

namespace Gems.ServiceBus
{
    /// <summary>
    /// Заголовки сообщения.
    /// </summary>
    public class Headers
    {
        /// <summary>
        /// Корреляционный идентификатор, нужен для объединения в группу набора сообщений.
        /// </summary>
        public static readonly string CorrelationId = "x-correlation-id";

        /// <summary>
        /// Заголовок, который содержит правила устаревания данных.
        /// Поддерживается следующий формат: <c>x-expires: at 2014-04-01T22:00:33Z</c> или <c>x-expires: in 100</c>.
        /// </summary>
        public static readonly string Expires = "x-expires";

        /// <summary>
        /// Метка сообщения, с которым она была отправлена.
        /// Использование этого заголовка не рекомендуется.
        /// </summary>
        public static readonly string MessageLabel = "x-message-type";

        /// <summary>
        /// Содержит информацию о том, что сообщение необходимо сохранить.
        /// </summary>
        public static readonly string Persist = "x-persist";

        /// <summary>
        /// Адрес ответного сообщения на запрос.
        /// </summary>
        public static readonly string ReplyRoute = "x-reply-route";

        /// <summary>
        /// Время ответа на запрос.
        /// </summary>
        public static readonly string Timeout = "x-timeout";

        /// <summary>
        /// Время жизни сообщения.
        /// </summary>
        public static readonly string Ttl = "x-ttl";

        /// <summary>
        /// Время жизни сообщений в очереди.
        /// </summary>
        public static readonly string QueueMessageTtl = "x-message-ttl";

        /// <summary>
        /// Путь сообщений по системе. Содержит список всех конечных точек, через которое прошло сообщение, разделенное символом ";".
        /// </summary>
        public static readonly string Breadcrumbs = "x-breadcrumbs";

        /// <summary>
        /// Идентификатор сообщения инициировавшего обмен сообщениями.
        /// </summary>
        public static readonly string OriginalMessageId = "x-original-message-id";

        /// <summary>
        /// Получает значение заголовка из сообщения и удаляет его из списка заголовков сообщения.
        /// </summary>
        /// <param name="headers">
        /// Список заголовков сообщения.
        /// </param>
        /// <param name="key">
        /// Заголовок сообщения, чье значение нужно получить.
        /// </param>
        /// <typeparam name="T">Тип получаемого значения.</typeparam>
        /// <returns>Если заголовок существует, тогда его значение, иначе <c>null</c> или 0.</returns>
        public static T Extract<T>(IDictionary<string, object> headers, string key)
        {
            object value;
            if (headers.TryGetValue(key, out value))
            {
                headers.Remove(key);
                return (T)value;
            }

            return default(T);
        }

        /// <summary>
        /// Получает строковое значение заголовка из набора заголовков.
        /// </summary>
        /// <param name="headers">Коллекция заголовков сообщений.</param>
        /// <param name="key">Имя заголовка.</param>
        /// <returns>Строковое значение заголовка или пустая строка, если заголовка не существует в наборе.</returns>
        public static string GetString(IDictionary<string, object> headers, string key)
        {
            object value;
            if (headers.TryGetValue(key, out value))
            {
                if (value is string)
                {
                    return (string)value;
                }

                if (value is byte[])
                {
                    return Encoding.UTF8.GetString((byte[])value);
                }
            }

            return string.Empty;
        }
    }
}