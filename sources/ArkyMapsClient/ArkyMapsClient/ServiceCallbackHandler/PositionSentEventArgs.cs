using ArkyMapsDomainModel;
using System;

namespace ArkyMapsClient
{
    /// <summary>
    /// Location event data container class..
    /// </summary>
    public class PositionSentEventArgs : EventArgs
    {
        #region properties
        /// <summary>
        /// Gets or set the position value of the event.
        /// </summary>
        public Position Position { get; set; }
        #endregion
    }
}
