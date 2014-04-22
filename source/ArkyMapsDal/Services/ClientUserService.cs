using ArkyMapsDomainModel;

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
        /// Query <see cref="ClientUser"/> entity by username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="ClientUser"/> entity.</param>
        /// <param name="password">Password of <see cref="ClientUser"/> entity.</param>
        /// <returns>The queried <see cref="ClientUser"/> entity.</returns>
        public ClientUser QueryUserByUsernameAndPassword(string username, string password)
        {
            return new ClientUser { ID = 1, Name = "test", Password = "test" };
        }
        #endregion
    }
}
