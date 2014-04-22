namespace ArkyMapsDal
{
    /// <summary>
    /// The type represents the entry point of data access layer.
    /// </summary>
    public class DalServices
    {
        #region attributes
        private UserService m_userService;
        #endregion


        #region properties
        /// <summary>
        /// Gets the <see cref="UserService"/> service entity.
        /// </summary>
        public UserService UserService
        {
            get { return m_userService; }
        }
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="DalService"/> class.
        /// </summary>
        public DalServices()
        {
            m_userService = new UserService();
        }
        #endregion
    }
    
}
