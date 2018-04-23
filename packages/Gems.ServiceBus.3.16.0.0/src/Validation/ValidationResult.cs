﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResult.cs" company="">
//   
// </copyright>
// <summary>
//   Результат валидации сообщения.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///   Результат валидации сообщения.
    /// </summary>
    public sealed class ValidationResult
    {
        #region Fields

        /// <summary>
        /// The _broken rules.
        /// </summary>
        private readonly IList<BrokenRule> _brokenRules;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidationResult"/>. 
        /// Создание результата валидации.
        /// </summary>
        /// <param name="brokenRules">
        /// Список нарушенных правил.
        /// </param>
        public ValidationResult(IEnumerable<BrokenRule> brokenRules)
        {
            this._brokenRules = brokenRules.ToList();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidationResult"/>. 
        /// Создание результата валидации.
        /// </summary>
        /// <param name="brokenRules">
        /// Список нарушенных правил.
        /// </param>
        public ValidationResult(params BrokenRule[] brokenRules)
        {
            this._brokenRules = brokenRules.ToList();
        }

        /// <summary>
        /// Предотвращает вызов конструктора по умолчанию для класса <see cref="ValidationResult"/>.
        /// </summary>
        private ValidationResult()
        {
            this._brokenRules = new BrokenRule[0];
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Возвращает успешный результат валидации.
        /// </summary>
        public static ValidationResult Valid
        {
            get
            {
                return new ValidationResult();
            }
        }

        /// <summary>
        ///   Список нарушенных правил.
        /// </summary>
        public IEnumerable<BrokenRule> BrokenRules
        {
            get
            {
                return this._brokenRules;
            }
        }

        /// <summary>
        ///   Выполнены ли все правила валидации.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this._brokenRules.Count == 0;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Вызывает исключение в случае нарушения хотя бы одного правила валидации.
        /// </summary>
        public void ThrowIfBroken()
        {
            if (this.IsValid)
            {
                return;
            }

            throw new MessageValidationException(string.Join(", ", this.BrokenRules.Select(r => r.Description)));
        }

        #endregion
    }
}