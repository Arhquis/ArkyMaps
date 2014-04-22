using ArkyMapsDomainModel;

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
        /// Query <see cref="PhoneUser"/> entity by username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="PhoneUser"/> entity.</param>
        /// <param name="password">Password of <see cref="PhoneUser"/> entity.</param>
        /// <returns>The queried <see cref="PhoneUser"/> entity.</returns>
        public PhoneUser QueryUserByUsernameAndPassword(string username, string password)
        {
            return new PhoneUser { ID = 1, Name = "test", Password = "test" };
        }
        #endregion
    }
}
