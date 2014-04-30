using ArkyMapsDomainModel;

namespace OpenLayersMapControl
{
    /// <summary>
    /// The class represents a map user.
    /// </summary>
    public class MapUser
    {
        #region attributes
        private ScriptObject m_scriptObject;
        private LonLat m_location;
        #endregion


        #region properties
        /// <summary>
        /// Gets or private sets the ID of the map user.
        /// </summary>
        public long ID { get; private set; }


        /// <summary>
        /// Gets or private sets the name of the map user.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// Gets or sets the location of the map user.
        /// </summary>
        public LonLat Location
        {
            get
            {
                return m_location;
            }

            set
            {
                m_location = value;

                m_scriptObject.MoveMapObject(this);
            }
        }


        /// <summary>
        /// Internal gets or sets the browser object.
        /// </summary>
        internal object BrowserObject { get; set; }
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapUser"/> class.
        /// </summary>
        /// <param name="id">The id of the new <see cref="MapUser"/> entity.</param>
        /// <param name="name">The name of the new <see cref="MapUser"/> entity.</param>
        /// <param name="location">The name of the new <see cref="MapUser"/> entity where it will be placed.</param>
        /// <param name="scriptObject">The <see cref="ScriptObject"/> provides JavaScript interoperability.</param>
        internal MapUser(long id, string name, LonLat location, ScriptObject scriptObject)
        {
            ID = id;
            Name = name;
            m_location = location;
            m_scriptObject = scriptObject;
        }
        #endregion
    }
}
