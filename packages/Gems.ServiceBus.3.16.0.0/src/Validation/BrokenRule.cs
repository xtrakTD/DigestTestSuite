﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrokenRule.cs" company="">
//   
// </copyright>
// <summary>
//   Описание нарушенного правила валидации сообщения.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Validation
{
    /// <summary>
    ///   Описание нарушенного правила валидации сообщения.
    /// </summary>
    public class BrokenRule
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BrokenRule"/>. 
        /// Создание описания нарушенного правила валидации.
        /// </summary>
        /// <param name="description">
        /// Текстовое описание ошибки валидации.
        /// </param>
        public BrokenRule(string description)
        {
            this.Description = description;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Текстовое описание ошибки валидации.
        /// </summary>
        public string Description { get; private set; }

        #endregion
    }
}