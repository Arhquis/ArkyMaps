using ArkyMapsDomainModel;
using System;

namespace ArkyMapsClient
{
    /// <summary>
    /// Location event data container class..
    /// </summary>
    public class LocationSentEventArgs : EventArgs
    {
        #region properties
        /// <summary>
        /// Gets or set the location value of the event.
        /// </summary>
        public Location Location { get; set; }
        #endregion
    }
}
