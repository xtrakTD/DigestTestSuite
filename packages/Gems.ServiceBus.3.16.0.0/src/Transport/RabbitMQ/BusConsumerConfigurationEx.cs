﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusConsumerConfigurationEx.cs" company="">
//   
// </copyright>
// <summary>
//   The bus consumer configuration ex.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Transport.RabbitMQ
{
    using Gems.ServiceBus.Receiving;

    /// <summary>
    /// The bus consumer configuration ex.
    /// </summary>
    public static class BusConsumerConfigurationEx
    {
        #region Public Methods and Operators

        /// <summary>
        /// The with qo s.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="qosParams">
        /// The qos params.
        /// </param>
        /// <returns>
        /// The <see cref="IReceiverConfigurator"/>.
        /// </returns>
        public static IReceiverConfigurator WithQoS(this IReceiverConfigurator builder, QoSParams qosParams)
        {
            ((RabbitReceiverOptions)((ReceiverConfiguration)builder).Options).QoS = qosParams;

            return builder;
        }

        /// <summary>
        /// The with qo s.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="qosParams">
        /// The qos params.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IReceiverConfigurator"/>.
        /// </returns>
        public static IReceiverConfigurator<T> WithQoS<T>(this IReceiverConfigurator<T> builder, QoSParams qosParams) where T : class
        {
            ReceiverConfiguration configuration = ((TypedReceiverConfigurationDecorator<T>)builder).Configuration;

            WithQoS(configuration, qosParams);

            return builder;
        }

        /// <summary>
        /// The with qo s.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="prefetchCount">
        /// The prefetch count.
        /// </param>
        /// <param name="prefetchSize">
        /// The prefetch size.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IReceiverConfigurator"/>.
        /// </returns>
        public static IReceiverConfigurator<T> WithQoS<T>(this IReceiverConfigurator<T> builder, ushort prefetchCount, uint prefetchSize) where T : class
        {
            return builder.WithQoS(new QoSParams(prefetchCount, prefetchSize));
        }

        #endregion
    }
}