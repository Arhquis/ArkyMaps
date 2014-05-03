using System.Runtime.Serialization;

namespace ArkyMapsDomainModel
{
    /// <summary>
    /// The class represents a phone user.
    /// </summary>
    [DataContract(Namespace = "ArkyMaps.com/PhoneService")]
    public class PhoneUser
    {
        #region properties
        /// <summary>
        /// Gets or sets the ID of user.
        /// </summary>
        [DataMember]
        public long ID { get; set; }


        /// <summary>
        /// Gets or sets the username of user.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }


        /// <summary>
        /// Gets or sets the password of user.
        /// </summary>
        [DataMember]
        public string Password { get; set; }


        /// <summary>
        /// Gets or sets the name of user.
        /// </summary>
        [DataMember]
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets whether the user male or not.
        /// </summary>
        [DataMember]
        public bool Male { get; set; }


        /// <summary>
        /// Gets or sets the email of user.
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        #endregion
    }
}
