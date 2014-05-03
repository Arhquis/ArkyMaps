using System.Collections.Generic;
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
        /// Query all <see cref="DM.PhoneUser"/> entities.
        /// </summary>
        /// <returns>Collection of <see cref="DM.PhoneUser"/> entities.</returns>
        public IEnumerable<DM.PhoneUser> QueryPhoneUsers()
        {
            IEnumerable<DM.PhoneUser> rvPhoneUsers = null;

            IEnumerable<EDM.PhoneUser> edmPhoneUsers = null;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                edmPhoneUsers =
                    from phoneUser in context.PhoneUser
                    where phoneUser.Deleted == false
                    select phoneUser;

                rvPhoneUsers = edmPhoneUsers.Select(phoneUser => EdmToDmMapper.MapEdmToDmPhoneUser(phoneUser)).ToList();
            }

            return rvPhoneUsers;
        }


        /// <summary>
        /// Creates a new <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="dmPhoneUser">A <see cref="DM.PhoneUser"/> entity to save in database.</param>
        /// <returns>True if saving was successfull, false otherwise.</returns>
        public bool CreatePhoneUser(DM.PhoneUser dmPhoneUser)
        {
            bool rvSucceeded = false;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                EDM.PhoneUser edmPhoneUser = context.PhoneUser.Create();
                edmPhoneUser.UserName = dmPhoneUser.UserName;
                edmPhoneUser.Password = dmPhoneUser.Password;
                edmPhoneUser.Name = dmPhoneUser.Name;
                edmPhoneUser.Male = dmPhoneUser.Male;
                edmPhoneUser.Email = dmPhoneUser.Email;

                context.PhoneUser.Add(edmPhoneUser);

                int affectedRows = context.SaveChanges();

                if (affectedRows == 1)
                {
                    rvSucceeded = true;
                }
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Modify the specified <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="dmPhoneUser">A <see cref="DM.PhoneUser"/> entity to modify in database.</param>
        /// <returns>True if modification was successfull, false otherwise.</returns>
        public bool ModifyPhoneUser(DM.PhoneUser dmPhoneUser)
        {
            bool rvSucceeded = false;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                EDM.PhoneUser edmPhoneUser =
                    (from phoneUser in context.PhoneUser
                     where phoneUser.ID == dmPhoneUser.ID && phoneUser.Deleted == false
                     select phoneUser).SingleOrDefault();

                if (edmPhoneUser != null)
                {
                    edmPhoneUser.UserName = dmPhoneUser.UserName;
                    edmPhoneUser.Password = dmPhoneUser.Password;
                    edmPhoneUser.Name = dmPhoneUser.Name;
                    edmPhoneUser.Male = dmPhoneUser.Male;
                    edmPhoneUser.Email = dmPhoneUser.Email;

                    int affectedRows = context.SaveChanges();

                    if (affectedRows == 1)
                    {
                        rvSucceeded = true;
                    }
                }
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Delete the specified <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="dmPhoneUser">A <see cref="DM.PhoneUser"/> entity to delete.</param>
        /// <returns>True if delete was successfull, false otherwise.</returns>
        public bool DeletePhoneUser(DM.PhoneUser dmPhoneUser)
        {
            bool rvSucceeded = false;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                EDM.PhoneUser edmPhoneUser =
                    (from phoneUser in context.PhoneUser
                     where phoneUser.ID == dmPhoneUser.ID && phoneUser.Deleted == false
                     select phoneUser).SingleOrDefault();

                if (edmPhoneUser != null)
                {
                    edmPhoneUser.Deleted = true;

                    int affectedRows = context.SaveChanges();

                    if (affectedRows == 1)
                    {
                        rvSucceeded = true;
                    }
                }
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Query <see cref="DM.PhoneUser"/> entity by username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="DM.PhoneUser"/> entity.</param>
        /// <param name="password">Password of <see cref="DM.PhoneUser"/> entity.</param>
        /// <returns>The <see cref="DM.PhoneUser"/> entity with the specified username and password.</returns>
        public DM.PhoneUser QueryPhoneUserByUsernameAndPassword(string username, string password)
        {
            DM.PhoneUser rvPhoneUser = null;

            EDM.PhoneUser edmPhoneUser = null;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                edmPhoneUser =
                    (from phoneUser in context.PhoneUser
                    where phoneUser.UserName == username && phoneUser.Password == password && phoneUser.Deleted == false
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
                     where phoneUser.ID == userId && phoneUser.Deleted == false
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
