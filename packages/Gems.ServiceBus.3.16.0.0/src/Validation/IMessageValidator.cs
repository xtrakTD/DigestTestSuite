﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageValidator.cs" company="">
//   
// </copyright>
// <summary>
//   Валидатор сообщения.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Validation
{
    /// <summary>
    ///   Валидатор сообщения.
    /// </summary>
    public interface IMessageValidator
    {
        #region Public Methods and Operators

        /// <summary>
        /// Проверить валидность сообщения.
        /// </summary>
        /// <param name="message">
        /// Сообщение для проверки.
        /// </param>
        /// <returns>
        /// Результат валидации.
        /// </returns>
        ValidationResult Validate(IMessage message);

        #endregion
    }
}