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
    }
}
