using ArkyMapsDomainModel;
using System.ServiceModel;

namespace ArkyMapService
{
    /// <summary>
    /// The class implements map service methods.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MapService : IMapService
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="MapService"/> class.
        /// </summary>
        public MapService()
        {
            // NOTE: intentionally left blank.
        }
        #endregion


        #region IMapService members
        /// <summary>
        /// Try to log in the user.
        /// </summary>
        /// <param name="username">Username of user.</param>
        /// <param name="password">Password of user.</param>
        /// <returns>The <see cref="ClientUser"/> logged in.</returns>
        public ClientUser Login(string username, string password)
        {
            ClientUser rvClientUser = null;

            IMapServiceCallback callback = OperationContext.Current.GetCallbackChannel<IMapServiceCallback>();

            rvClientUser = ServiceController.Instance.LoginClientUser(username, password, callback);

            return rvClientUser;
        }


        /// <summary>
        /// Log out the user.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        public void Logout(long userId)
        {
            ServiceController.Instance.LogoutClientUser(userId);
        }


        /// <summary>
        /// Query <see cref="PhoneUser"/> entity by ID.
        /// </summary>
        /// <param name="userId">ID of a <see cref="PhoneUser"/> entity.</param>
        /// <returns>The <see cref="PhoneUser"/> entity with the specififed ID.</returns>
        public PhoneUser QueryPhoneUserById(long userId)
        {
            return ServiceController.Instance.QueryPhoneUserById(userId);
        }
        #endregion
    }
}
