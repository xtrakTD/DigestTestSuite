﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   Исключение вызванное неудачной валидацией сообщения.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Validation
{
    using System;

    /// <summary>
    ///   Исключение вызванное неудачной валидацией сообщения.
    /// </summary>
    public class MessageValidationException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MessageValidationException"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        internal MessageValidationException(string message)
            : base(message)
        {
        }

        #endregion
    }
}