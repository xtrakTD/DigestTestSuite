// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITopologyEntity.cs" company="">
//   
// </copyright>
// <summary>
//   The TopologyEntity interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Topology
{
    /// <summary>
    /// The TopologyEntity interface.
    /// </summary>
    public interface ITopologyEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        #endregion
    }
}