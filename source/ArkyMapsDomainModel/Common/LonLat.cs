using System.Runtime.Serialization;

namespace ArkyMapsDomainModel
{
    [DataContract(Namespace = "ArkyMaps.com/PhoneService")]
    public class LonLat
    {
        #region properties
        /// <summary>
        /// Gets or sets the longitude value of the object.
        /// </summary>
        [DataMember]
        public double Longitude { get; set; }


        /// <summary>
        /// Gets or sets the latitude value of the object.
        /// </summary>
        [DataMember]
        public double Latitude { get; set; }
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LonLat"/> class.
        /// </summary>
        /// <param name="longitude">Longitude value.</param>
        /// <param name="latitude">Latitiude value.</param>
        public LonLat(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        #endregion
    }
}
