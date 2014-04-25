using ArkyMapsClient.ArkyMapServiceReference;
using ArkyMapsClient.Controls;
using ArkyMapsDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkyMapsClient
{
    public class MapServiceCallbackHandler : IMapServiceCallback
    {
        public event EventHandler<LocationSentEventArgs> LocationSent;


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
    }
}
