﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRouteResolverBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The RouteResolverBuilder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Sending
{
    using Gems.ServiceBus.Topology;

    /// <summary>
    /// The RouteResolverBuilder interface.
    /// </summary>
    public interface IRouteResolverBuilder
    {
        #region Public Properties

        /// <summary>
        /// Gets the endpoint.
        /// </summary>
        IEndpoint Endpoint { get; }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        ISenderConfiguration Sender { get; }

        /// <summary>
        /// Gets the topology.
        /// </summary>
        ITopologyBuilder Topology { get; }

        #endregion
    }
}