// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeliveryContext.cs" company="">
//   
// </copyright>
// <summary>
//   The DeliveryContext interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Receiving
{
    /// <summary>
    /// The DeliveryContext interface.
    /// </summary>
    public interface IDeliveryContext
    {
        #region Public Properties

        /// <summary>
        /// Gets the delivery.
        /// </summary>
        IDelivery Delivery { get; }

        #endregion
    }
}