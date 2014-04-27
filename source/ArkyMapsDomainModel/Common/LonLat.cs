using System.Runtime.Serialization;

namespace ArkyMapsDomainModel
{
    [DataContract(Namespace = "ArkyMaps.com/PhoneService")]
    public class LonLat
    {
        #region constructors
        private const string TO_STRING_FORMAT_STRING = "Longitude: {0}, Latitude: {1}";
        #endregion


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
        public LonLat()
        {
        }


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


        #region object override
        /// <summary>
        /// Returns string representation of object.
        /// </summary>
        /// <returns>String representation of object.</returns>
        public override string ToString()
        {
            return string.Format(TO_STRING_FORMAT_STRING, Longitude, Latitude);
        }
        #endregion
    }
}
