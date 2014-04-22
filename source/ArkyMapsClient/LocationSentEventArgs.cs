using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkyMapsClient
{
    public class LocationSentEventArgs : EventArgs
    {
        public long UserId { get; set; }

        public long Lon { get; set; }

        public long Lat { get; set; }
    }
}
