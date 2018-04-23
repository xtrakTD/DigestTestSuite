﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageValidatorGroup.cs" company="">
//   
// </copyright>
// <summary>
//   The message validator group.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    using Gems.ServiceBus.Helpers.CodeContracts;

    /// <summary>
    /// The message validator group.
    /// </summary>
    public sealed class MessageValidatorGroup
    {
        #region Fields

        /// <summary>
        /// The _validators.
        /// </summary>
        private readonly IList<IMessageValidator> _validators;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MessageValidatorGroup"/>.
        /// </summary>
        /// <param name="validators">
        /// The validators.
        /// </param>
        public MessageValidatorGroup(IEnumerable<IMessageValidator> validators)
        {
            Requires.NotNull(validators, "validators");

            this._validators = validators.ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the validators.
        /// </summary>
        public IEnumerable<IMessageValidator> Validators
        {
            get
            {
                return this._validators;
            }
        }

        #endregion
    }
}