using ArkyMapsDomainModel;
using System.Linq;

using DM = ArkyMapsDomainModel;
using EDM = ArkyMapsDal;

namespace ArkyMapsDal
{
    /// <summary>
    /// The type implements client user operations.
    /// </summary>
    public class PhoneUserService
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="PhoneUserService"/> class.
        /// </summary>
        internal PhoneUserService()
        {
            // NOTE intentionally left blank
        }
        #endregion


        #region user queries
        /// <summary>
        /// Query <see cref="DM.PhoneUser"/> entity by username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="DM.PhoneUser"/> entity.</param>
        /// <param name="password">Password of <see cref="DM.PhoneUser"/> entity.</param>
        /// <returns>The <see cref="DM.PhoneUser"/> entity with the specified username and password.</returns>
        public DM.PhoneUser QueryUserByUsernameAndPassword(string username, string password)
        {
            DM.PhoneUser rvPhoneUser = null;

            EDM.PhoneUser edmPhoneUser = null;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                edmPhoneUser =
                    (from phoneUser in context.PhoneUser
                    where phoneUser.Name == username && phoneUser.Password == password
                    select phoneUser).SingleOrDefault();
            }

            if (edmPhoneUser != null)
            {
                rvPhoneUser = EdmToDmMapper.MapEdmToDmPhoneUser(edmPhoneUser);
            }

            return rvPhoneUser;
        }


        /// <summary>
        /// Query <see cref="DM.PhoneUser"/> entity by ID.
        /// </summary>
        /// <param name="userId">ID of a <see cref="DM.PhoneUser"/> entity.</param>
        /// <returns>The <see cref="DM.PhoneUser"/> entity with the specififed ID.</returns>
        public DM.PhoneUser QueryPhoneUserById(long userId)
        {
            DM.PhoneUser rvPhoneUser = null;

            EDM.PhoneUser edmPhoneUser = null;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                edmPhoneUser =
                    (from phoneUser in context.PhoneUser
                     where phoneUser.ID == userId
                     select phoneUser).SingleOrDefault();
            }

            if (edmPhoneUser != null)
            {
                rvPhoneUser = EdmToDmMapper.MapEdmToDmPhoneUser(edmPhoneUser);
            }

            return rvPhoneUser;
        }
        #endregion
    }
}
