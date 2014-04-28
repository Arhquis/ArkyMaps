using System.Windows.Controls;

namespace ArkyMapsClient.Views
{
    /// <summary>
    /// Interaction logic for RealTimeView.xaml
    /// </summary>
    public partial class RealTimeView : UserControl
    {
        #region attributes
        private MapServiceCallbackHandler m_callbackHandler;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RealTimeView"/> class.
        /// </summary>
        public RealTimeView()
        {
            InitializeComponent();
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Loads the real time view and its components.
        /// </summary>
        /// <param name="callbackHandler">Instance of a <see cref="MapServiceCallbackHandler"/> class.</param>
        public void Load(MapServiceCallbackHandler callbackHandler)
        {
            m_callbackHandler = new MapServiceCallbackHandler();

            m_mapControl.MapLoaded += MapControl_MapLoaded;

            m_mapControl.LoadMap();
        }


        /// <summary>
        /// Register for locatzion sent event of the map service.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MapControl_MapLoaded(object sender, System.EventArgs e)
        {
            m_callbackHandler.LocationSent += m_callbackHandler_LocationSent;
        }
        #endregion


        #region client operations
        private void m_callbackHandler_LocationSent(object sender, LocationSentEventArgs e)
        {
        }
        #endregion
    }
}
