using ArkyMapsClient.ArkyMapServiceReference;
using ArkyMapsClient.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkyMapsClient
{
    public class MapServiceCallbackHandler : IMapServiceCallback
    {
        public event EventHandler<LocationSentEventArgs> LocationSent;
        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void NewLocation(long userId, long lon, long lat)
        {
            if (LocationSent != null)
            {
                LocationSent(this, new LocationSentEventArgs { UserId = userId, Lon = lon, Lat = lat });
            }
        }
    }
}
