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
        /// Fires if a new position has been sent by map service.
        /// </summary>
        public event EventHandler<PositionSentEventArgs> PositionSent;
        #endregion


        #region IMapServiceCallback members
        /// <summary>
        /// Send new <see cref="Location"/> entity to clients.
        /// </summary>
        /// <param name="position">The new <see cref="Location"/>.</param>
        public void NewPosition(Position position)
        {
            if (PositionSent != null)
            {
                PositionSent(this, new PositionSentEventArgs { Position = position });
            }
        }
        #endregion
    }
}
