﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListeningSource.cs" company="">
//   
// </copyright>
// <summary>
//   The ListeningSource interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Receiving
{
    /// <summary>
    /// The ListeningSource interface.
    /// </summary>
    public interface IListeningSource
    {
        #region Public Properties

        /// <summary>
        /// Gets the address.
        /// </summary>
        string Address { get; }

        #endregion
    }
}