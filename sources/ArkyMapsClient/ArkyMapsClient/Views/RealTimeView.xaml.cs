using ArkyMapsClient.ArkyMapServiceReference;
using ArkyMapsDomainModel;
using OpenLayersMapControl;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private QueueWorker<Location> m_locationQueueWorker;
        private Dictionary<PhoneUser, MapUser> m_phoneUserMapObjectMap;
        #endregion


        #region properties
        /// <summary>
        /// Gets or sets the <see cref="MainWindow"/> instance.
        /// </summary>
        public MainWindow MainWindow { get; set; }


        /// <summary>
        /// Indicates whether the <see cref="RealTimeView"/> is loaded or not.
        /// </summary>
        public bool IsViewLoaded { get; private set; }
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
            m_callbackHandler = callbackHandler;

            m_mapControl.MapLoaded += MapControl_MapLoaded;

            m_mapControl.LoadMap();
        }


        /// <summary>
        /// Register for location sent event of the map service and start the location queue worker.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MapControl_MapLoaded(object sender, EventArgs e)
        {
            m_mapControl.MapLoaded -= MapControl_MapLoaded;

            m_phoneUserMapObjectMap = new Dictionary<PhoneUser, MapUser>();

            m_locationQueueWorker = new QueueWorker<Location>(1000, HandleQueue);
            m_locationQueueWorker.Start();

            m_callbackHandler.LocationSent += CallbackHandler_LocationSent;

            IsViewLoaded = true;
        }


        /// <summary>
        /// Deregister and stop the services.
        /// </summary>
        public void Unload()
        {
            m_callbackHandler.LocationSent -= CallbackHandler_LocationSent;
            m_locationQueueWorker.Stop();

            IsViewLoaded = false;
        }
        #endregion


        #region client operations
        /// <summary>
        /// Stores incoming locations into the location queue.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void CallbackHandler_LocationSent(object sender, LocationSentEventArgs e)
        {
            m_locationQueueWorker.Enqueue(e.Location);
        }


        /// <summary>
        /// Handles location dequeued from location queue.
        /// Register new phone user if it was not registered before and move registered ones into the new location.
        /// </summary>
        /// <param name="location">Location value pick from the queue.</param>
        void HandleQueue(Location location)
        {
            PhoneUser phoneUser = m_phoneUserMapObjectMap.Keys.SingleOrDefault(user => user.ID == location.PhoneUserId);
            MapUser mapObject = null;

            if (phoneUser == null)
            {
                phoneUser = MainWindow.QueryPhoneUserById(location.PhoneUserId);

                Dispatcher.Invoke(new Action(() =>
                {
                    mapObject = m_mapControl.AddMapUserToMap(phoneUser.ID, phoneUser.Name, location.Value);
                }));

                m_phoneUserMapObjectMap.Add(phoneUser, mapObject);
            }
            else
            {
                mapObject = m_phoneUserMapObjectMap[phoneUser];
            }

            Dispatcher.Invoke(new Action(() =>
            {
                mapObject.Location = location.Value;
            }));
        }
        #endregion
    }
}
