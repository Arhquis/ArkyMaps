using ArkyMapsClient.ArkyMapServiceReference;
using ArkyMapsDomainModel;
using System;

namespace ArkyMapsClient
{
    /// <summary>
    /// The class handles map service callbacks.
    /// </summary>
    public class MapServiceCallbackHandler : IMapServiceCallback
    {
        #region events
        /// <summary>
        /// Fires if a new location has been sent by map service.
        /// </summary>
        public event EventHandler<LocationSentEventArgs> LocationSent;
        #endregion


        #region IMapServiceCallback members
        /// <summary>
        /// Send new <see cref="Location"/> entity to clients.
        /// </summary>
        /// <param name="location">The new <see cref="Location"/>.</param>
        public void NewLocation(Location location)
        {
            if (LocationSent != null)
            {
                LocationSent(this, new LocationSentEventArgs { Location = location });
            }
        }
        #endregion
    }
}
