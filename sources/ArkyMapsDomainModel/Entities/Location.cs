using System.Runtime.Serialization;

namespace ArkyMapsDomainModel
{
    /// <summary>
    /// The class represent a location entity.
    /// </summary>
    [DataContract(Namespace = "ArkyMaps.com/PhoneService")]
    public class Location
    {
        #region properties
        /// <summary>
        /// Gets or set the ID of location.
        /// </summary>
        [DataMember]
        public long ID { get; set; }


        /// <summary>
        /// Gets or set the ID of <see cref="PhoneUser"/>.
        /// </summary>
        [DataMember]
        public long PhoneUserId { get; set; }


        /// <summary>
        /// Gets or set the location value of entity.
        /// </summary>
        [DataMember]
        public LonLat Value { get; set; }
        #endregion
    }
}
