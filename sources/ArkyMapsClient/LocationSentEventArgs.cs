using ArkyMapsDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkyMapsClient
{
    public class LocationSentEventArgs : EventArgs
    {
        public Location Location { get; set; }
    }
}
