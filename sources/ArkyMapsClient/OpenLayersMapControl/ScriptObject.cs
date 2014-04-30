using ArkyMapsDomainModel;
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
        private const string OL_LOAD_MAP = "LoadMap";
        private const string OL_GET_OPEN_LAYERS_LON_LAT = "GetOpenLayersLonLat";
        private const string OL_ADD_MAP_USER = "AddMapUser";
        private const string OL_MOVE_MAP_USER = "MoveMapUser";
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
        /// <param name="webBrowser">Webbrowser shows the map.</param>
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
        /// Calls from script if map loaded.
        /// </summary>
        public void MapLoadedCallback()
        {
            m_mapControl.OnMapLoaded();
        }
        #endregion


        #region map user
        /// <summary>
        /// Adds a <see cref="MapUser"/> to the map.
        /// </summary>
        /// <param name="mapUser">A <see cref="MapUser"/> to add.</param>
        public void AddMapUser(MapUser mapUser)
        {
            mapUser.BrowserObject = InvokeScript(
                OL_ADD_MAP_USER,
                mapUser.ID,
                mapUser.Name,
                GetOpenLayersLonLat(mapUser.Location));
        }


        /// <summary>
        /// Moves the <see cref="MapUser"/> to its location on the map.
        /// </summary>
        /// <param name="mapObject"></param>
        public void MoveMapObject(MapUser mapObject)
        {
            InvokeScript(
                OL_MOVE_MAP_USER,
                mapObject.BrowserObject,
                GetOpenLayersLonLat(mapObject.Location));
        }
        #endregion


        #region utility
        /// <summary>
        /// Transforms a <see cref="LonLat"/> instance to the OpenLayers representation.
        /// </summary>
        /// <param name="location">The <see cref="LonLat"/> instance to transform.</param>
        /// <returns>The transformed OpenLayers location.</returns>
        private object GetOpenLayersLonLat(LonLat location)
        {
            object rvOpenLayersLocation;

            rvOpenLayersLocation = InvokeScript(
                OL_GET_OPEN_LAYERS_LON_LAT,
                location.Longitude, location.Latitude);

            return rvOpenLayersLocation;
        }


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
