using System;
using System.ServiceModel;

namespace ArkyMapService
{
    /// <summary>
    /// The class implements phone service methods.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PhoneService : IPhoneService
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="PhoneService"/> class.
        /// </summary>
        public PhoneService()
        {
            // NOTE: intentionally left blank.
        }
        #endregion


        #region IPhoneService members
        /// <summary>
        /// Try to log in the user.
        /// </summary>
        /// <param name="username">Username of user.</param>
        /// <param name="password">Password of user.</param>
        /// <returns>The ID of user logged in.</returns>
        public long Login(string username, string password)
        {
            return ServiceController.Instance.LoginPhoneUser(username, password);
        }


        /// <summary>
        /// Register a new location for user belongs to user ID.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        /// <param name="longitude">Longitude value of location.</param>
        /// <param name="latitude">Latitiude value of location.</param>
        public void NewLocation(long userId, long lon, long lat)
        {
            ServiceController.Instance.NewLocation(userId, lon, lat);
        }
        #endregion
    }
}
