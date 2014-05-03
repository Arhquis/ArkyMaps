using System.Runtime.Serialization;

namespace ArkyMapsDomainModel
{
    /// <summary>
    /// The class represent a position entity.
    /// </summary>
    [DataContract(Namespace = "ArkyMaps.com/PhoneService")]
    public class Position
    {
        #region constants
        private const string TO_STRING_FORMAT_STRING = "ID: {0}, PhoneUserID: {1}, Location: {2}";
        #endregion


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
        public LonLat Location { get; set; }
        #endregion


        #region object override
        /// <summary>
        /// Returns string representation of object.
        /// </summary>
        /// <returns>String representation of object.</returns>
        public override string ToString()
        {
            return string.Format(TO_STRING_FORMAT_STRING, ID, PhoneUserId, Location);
        }
        #endregion

    }
}
