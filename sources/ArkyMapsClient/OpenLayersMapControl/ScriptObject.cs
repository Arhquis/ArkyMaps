using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace OpenLayersMapControl
{
    /// <summary>
    /// The class hold operations to interact with JavaScript code of map.
    /// </summary>
    [ComVisible(true)]
    public class ScriptObject
    {
        #region constants
        private const string OL_LOAD_MAP = "Load";
        #endregion


        #region atrributes
        private MapControl m_mapControl;
        private WebBrowser m_webBrowser;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instence of the <see cref="ScriptObject"/> class.
        /// </summary>
        /// <param name="mapControl"><see cref="MapControl"/> instance controls the map.</param>
        /// <param name="webBrowser">Webbroser shows the map.</param>
        public ScriptObject(MapControl mapControl, WebBrowser webBrowser)
        {
            m_mapControl = mapControl;
            m_webBrowser = webBrowser;
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Loads the map.
        /// </summary>
        public void LoadMap()
        {
            InvokeScript(OL_LOAD_MAP);
        }


        /// <summary>
        /// Calls from script if if map loaded.
        /// </summary>
        public void MapLoadedCallback()
        {
            m_mapControl.OnMapLoaded();
        }
        #endregion


        #region utility
        /// <summary>
        /// Invokes the specified JavaScript method with the passed arguments.
        /// </summary>
        /// <param name="name">Name of the method to invoke.</param>
        /// <param name="args">Arguments of the method invoke.</param>
        /// <returns>The value returned by the JavaScript method.</returns>
        private object InvokeScript(string name, params object[] args)
        {
            object returnValue = null;

            returnValue = args.Length == 0
                ? m_webBrowser.InvokeScript(name)
                : m_webBrowser.InvokeScript(name, args);

            return returnValue;
        }
        #endregion
    }
}
