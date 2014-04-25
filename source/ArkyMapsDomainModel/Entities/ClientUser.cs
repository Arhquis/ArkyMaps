using System.Runtime.Serialization;

namespace ArkyMapsDomainModel
{
    /// <summary>
    /// The class represents a client user.
    /// </summary>
    [DataContract(Namespace = "ArkyMaps.com/PhoneService")]
    public class ClientUser
    {
        #region properties
        /// <summary>
        /// Gets or sets the ID of user.
        /// </summary>
        [DataMember]
        public long ID { get; set; }


        /// <summary>
        /// Gets or sets the name of user.
        /// </summary>
        [DataMember]
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets the password of user.
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        #endregion
    }
}
