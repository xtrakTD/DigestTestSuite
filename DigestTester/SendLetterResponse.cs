using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigestTester
{
    /// <summary>
    /// Результат отправки письма.
    /// </summary>
    public class SendLetterResponse
    {
        /// <summary>
        /// Статус отправки.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Количество доступных отправок.
        /// </summary>
        public long AvailableSendings { get; set; }

        /// <summary>
        /// Количество отправленных писем.
        /// </summary>
        public long SentLetters { get; set; }

        /// <summary>
        /// Время истечения ограничения.
        /// </summary>
        public TimeSpan? LimitExpiration { get; set; }

        /// <summary>
        /// Сообщение о ошибке отправки
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("AvailableSendings: {0}, LimitExpiration: {1}, SentLetters: {2}, Status: {3}", this.AvailableSendings, this.LimitExpiration, this.SentLetters, this.Status);
        }
    }
}
