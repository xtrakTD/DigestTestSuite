﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaybeExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The maybe extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Helpers
{
    using System.ComponentModel;

    /// <summary>
    /// The maybe extensions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class MaybeExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The maybe.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Maybe"/>.
        /// </returns>
        public static Maybe<T> Maybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        #endregion
    }
}