namespace ArkyMapsDal
{
    /// <summary>
    /// The type represents the entry point of data access layer.
    /// </summary>
    public class DalServices
    {
        #region attributes
        private ClientUserService m_clientUserService;
        private PhoneUserService m_phoneUserService;
        private PositionService m_positionService;
        #endregion


        #region properties
        /// <summary>
        /// Gets the <see cref="ClientUserService"/> service entity.
        /// </summary>
        public ClientUserService ClientUserService
        {
            get { return m_clientUserService; }
        }


        /// <summary>
        /// Gets the <see cref="PhoneUserService"/> service entity.
        /// </summary>
        public PhoneUserService PhoneUserService
        {
            get { return m_phoneUserService; }
        }


        /// <summary>
        /// Gets the <see cref="PositionService"/> service entity;
        /// </summary>
        public PositionService PositionService
        {
            get { return m_positionService; }
        }
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="DalService"/> class.
        /// </summary>
        public DalServices()
        {
            m_clientUserService = new ClientUserService();
            m_phoneUserService = new PhoneUserService();
            m_positionService = new PositionService();
        }
        #endregion
    }
}
