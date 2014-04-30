using ArkyMapsDomainModel;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenLayersMapControl
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        #region constants
        private const string MAP_HTML_RELATIVE_PATH = "Resources/Map.html";
        #endregion


        #region attributes
        ScriptObject m_scriptObject;
        #endregion


        #region events
        /// <summary>
        /// Fires if the map loaded and ready for use.
        /// </summary>
        public event EventHandler MapLoaded;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapControl"/> class.
        /// </summary>
        public MapControl()
        {
            InitializeComponent();
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Loads the map.
        /// </summary>
        public void LoadMap()
        {
            string mapHtmlAbsolutePath = String.Concat(AppDomain.CurrentDomain.BaseDirectory, MAP_HTML_RELATIVE_PATH);

            m_webBrowser.LoadCompleted += WebBrowser_LoadCompleted;

            m_webBrowser.Navigate(new Uri(mapHtmlAbsolutePath, UriKind.Absolute));
        }


        /// <summary>
        /// Creates the scripting object and loads the map objects on webbrowser loaded its contents.
        /// </summary>
        /// <param name="sender">Sender ov the event.</param>
        /// <param name="e">Arguments of the events.</param>
        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            m_webBrowser.LoadCompleted -= WebBrowser_LoadCompleted;

            m_scriptObject = new ScriptObject(this, m_webBrowser);

            m_webBrowser.ObjectForScripting = m_scriptObject;

            m_scriptObject.LoadMap();
        }


        /// <summary>
        /// Fires the <see cref="MapLoaded"/> event.
        /// </summary>
        internal void OnMapLoaded()
        {
            if (MapLoaded != null)
            {
                MapLoaded(this, new EventArgs());
            }
        }
        #endregion


        #region map user
        /// <summary>
        /// Creates a new <see cref="MapUser"/> and adds it to the map.
        /// </summary>
        /// <param name="id">The id of the new <see cref="MapUser"/> entity.</param>
        /// <param name="name">The name of the new <see cref="MapUser"/> entity.</param>
        /// <param name="location">The name of the new <see cref="MapUser"/> entity where it will be placed.</param>
        /// <returns>The newly created <see cref="MapUser"/> object.</returns>
        public MapUser AddMapUserToMap(long id, string name, LonLat location)
        {
            MapUser rvMapUser;

            rvMapUser = new MapUser(id, name, location, m_scriptObject);

            m_scriptObject.AddMapUser(rvMapUser);

            return rvMapUser;
        }
        #endregion
    }
}
