using ArkyMapsDomainModel;

namespace ArkyMapsDal
{
    /// <summary>
    /// The type implements user operations.
    /// </summary>
    public class UserService
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="UserService"/> class.
        /// </summary>
        internal UserService()
        {
            // NOTE intentionally left blank
        }
        #endregion


        #region user queries
        /// <summary>
        /// Query <see cref="User"/> entity by username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="User"/> entity.</param>
        /// <param name="password">Password of <see cref="User"/> entity.</param>
        /// <returns>The queried <see cref="User"/> entity.</returns>
        public User QueryUserByUsernameAndPassword(string username, string password)
        {
            return new User { ID = 1, Name = "test", Password = "test" };
        }
        #endregion
    }
}
