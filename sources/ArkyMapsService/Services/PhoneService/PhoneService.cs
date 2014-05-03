using ArkyMapsDomainModel;
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
        /// Register a new position for user belongs to user ID.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        /// <param name="location">Location value.</param>
        public void NewPosition(long userId, LonLat location)
        {
            ServiceController.Instance.NewPosition(userId, location);
        }
        #endregion
    }
}
