using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigestTester
{
    /// <summary>
    /// Объект отправляемого письма
    /// </summary>
    public class SendLetterRequest
    {
        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        public long FromId { get; set; }

        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        public long ToId { get; set; }

        /// <summary>
        /// Тип письма.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Тело письма.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Идентификатор письма, на которое выполяется ответ.
        /// </summary>
        public long? ReplyToId { get; set; }

        ///// <summary>
        ///// Список прикреплений.
        ///// </summary>
        //public Attachment[] Attachments { get; set; }

        /// <summary>
        /// Шаблон ссылки прикреплений.
        /// </summary> 
        public string AttachmentsUri { get; set; }

        public override string ToString()
        {
            return string.Format($"FromId: [{this.FromId.ToString()}], ToId: [{this.ToId.ToString()}], Type: [{this.Type}]");
        }
    }
}
