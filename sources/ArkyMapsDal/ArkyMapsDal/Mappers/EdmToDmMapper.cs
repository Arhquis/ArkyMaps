using DM = ArkyMapsDomainModel;
using EDM = ArkyMapsDal;

namespace ArkyMapsDal
{
    /// <summary>
    /// This class holds mappers from domain model to entity domain model.
    /// </summary>
    internal static class EdmToDmMapper
    {
        #region phone user
        /// <summary>
        /// Maps <see cref="EDM.PhoneUser"/> object to <see cref="DM.PhoneUser"/>.
        /// </summary>
        /// <param name="edmPhoneUser">The <see cref="EDM.PhoneUser"/> object to map.</param>
        /// <returns>The mapped <see cref="DM.PhoneUser"/> object.</returns>
        internal static DM.PhoneUser MapEdmToDmPhoneUser(EDM.PhoneUser edmPhoneUser)
        {
            DM.PhoneUser rvDmPhoneUser = new DM.PhoneUser
            {
                ID = edmPhoneUser.ID,
                UserName = edmPhoneUser.UserName,
                Password = edmPhoneUser.Password,
                Name = edmPhoneUser.Name,
                Male = edmPhoneUser.Male,
                Email = edmPhoneUser.Email
            };

            return rvDmPhoneUser;
        }
        #endregion


        #region client user
        /// <summary>
        /// Maps <see cref="EDM.ClientUser"/> object to <see cref="DM.ClientUser"/>.
        /// </summary>
        /// <param name="edmClinetUser">The <see cref="EDM.ClientUser"/> object to map.</param>
        /// <returns>The mapped <see cref="DM.ClientUser"/> object.</returns>
        internal static DM.ClientUser MapEdmToDmClientUser(EDM.ClientUser edmClinetUser)
        {
            DM.ClientUser rvDmPhoneUser = new DM.ClientUser
            {
                ID = edmClinetUser.ID,
                Name = edmClinetUser.Name,
                Password = edmClinetUser.Password
            };

            return rvDmPhoneUser;
        }
        #endregion
    }
}
