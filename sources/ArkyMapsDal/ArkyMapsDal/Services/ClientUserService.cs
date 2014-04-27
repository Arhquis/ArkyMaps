using ArkyMapsDomainModel;
using System.Linq;

using DM = ArkyMapsDomainModel;
using EDM = ArkyMapsDal;

namespace ArkyMapsDal
{
    /// <summary>
    /// The type implements client user operations.
    /// </summary>
    public class ClientUserService
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ClientUserService"/> class.
        /// </summary>
        internal ClientUserService()
        {
            // NOTE intentionally left blank
        }
        #endregion


        #region user queries
        /// <summary>
        /// Query <see cref="DM.ClientUser"/> entity by username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="DM.ClientUser"/> entity.</param>
        /// <param name="password">Password of <see cref="DM.ClientUser"/> entity.</param>
        /// <returns>The queried <see cref="DM.ClientUser"/> entity.</returns>
        public DM.ClientUser QueryUserByUsernameAndPassword(string username, string password)
        {
            DM.ClientUser rvClientUser = null;

            EDM.ClientUser edmClientUser = null;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                edmClientUser =
                    (from clientUser in context.ClientUser
                     where clientUser.Name == username && clientUser.Password == password
                     select clientUser).SingleOrDefault();
            }

            if (edmClientUser != null)
            {
                rvClientUser = EdmToDmMapper.MapEdmToDmClientUser(edmClientUser);
            }

            return rvClientUser;
        }
        #endregion
    }
}
