using ArkyMapsPhone.ArkyPhoneServiceReference;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;

namespace ArkyMapsPhone
{
    /// <summary>
    /// This class implements the location data sender page.
    /// </summary>
    public partial class SenderPage : PhoneApplicationPage
    {
        #region inner classes
        /// <summary>
        /// Inner class represent a location.
        /// </summary>
        public class LonLat
        {
            /// <summary>
            /// Gets or sets the longitude value.
            /// </summary>
            public double Longitude { get; set; }


            /// <summary>
            /// Gets or set the latitude value.
            /// </summary>
            public double Latitude { get; set; }
        }
        #endregion


        #region attributes
        private PhoneServiceClient m_serviceClient;
        private long m_userId;

        private Thread m_workerThread;
        private Geolocator m_geolocator;
        private List<LonLat> m_simulatorDataList;
        private int m_simulatorIndex;
        private bool m_isRealdataMode;

        private bool m_endWork;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SenderPage"/> class.
        /// </summary>
        public SenderPage()
        {
            InitializeComponent();
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Initializes the page.
        /// </summary>
        /// <param name="e">Sender of the event.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SetUIState(false);

            LoadSimulatorData();

            m_geolocator = new Geolocator();
            m_geolocator.DesiredAccuracyInMeters = 50;

            m_serviceClient =
                (PhoneServiceClient)PhoneApplicationService.Current.State[Constants.APPLICATION_STATE_NAME_SERVICE_CLIENT];
            m_userId =
                (long)PhoneApplicationService.Current.State[Constants.APPLICATION_STATE_NAME_USER_ID];
        }


        /// <summary>
        /// Loads the simulator data from resource file.
        /// </summary>
        private void LoadSimulatorData()
        {
            m_simulatorDataList = new List<LonLat>();

            Stream src = Application.GetResourceStream(
                new Uri("Resources/LocationList.txt", UriKind.Relative)).Stream;

            using (StreamReader reader = new StreamReader(src))
            {
                IFormatProvider numberFormat = CultureInfo.InvariantCulture.NumberFormat;

                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(';');

                    LonLat location = new LonLat
                    {
                        Longitude = Convert.ToDouble(values[0], numberFormat),
                        Latitude = Convert.ToDouble(values[1], numberFormat)
                    };

                    m_simulatorDataList.Add(location);
                }
            }
        }
        #endregion


        #region button events
        /// <summary>
        /// Starts the location sending.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            SetUIState(true);
            m_simulatorIndex = 0;
            m_isRealdataMode = (bool)m_rbRealData.IsChecked;

            m_endWork = false;

            m_workerThread = new Thread(DoWork);
            m_workerThread.IsBackground = true;

            m_workerThread.Start();
        }


        /// <summary>
        /// Periodically send location data.
        /// </summary>
        private async void DoWork()
        {
            while (!m_endWork)
            {
                LonLat location = await GetNextLocation();

                try
                {
                    m_serviceClient.NewPositionAsync(m_userId, location.Longitude, location.Latitude);
                }
                catch (Exception ex)
                {

                }

                Dispatcher.BeginInvoke(
                    () =>
                    {
                        m_tbLastSent.Text = DateTime.Now.ToLongTimeString();
                    });

                Thread.Sleep(3000);
            }
        }


        /// <summary>
        /// Gets the next location data depends on the working state of the sender page.
        /// </summary>
        /// <returns>A real location value or a simulated one.</returns>
        private async Task<LonLat> GetNextLocation()
        {
            LonLat rvLocation = null;

            if (m_isRealdataMode)
            {
                Geoposition geoposition = await m_geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                rvLocation = new LonLat
                {
                    Longitude = geoposition.Coordinate.Longitude,
                    Latitude = geoposition.Coordinate.Latitude
                };
            }
            else
            {
                if (m_simulatorIndex >= m_simulatorDataList.Count)
                {
                    m_simulatorIndex = 0;
                }

                rvLocation = m_simulatorDataList[m_simulatorIndex];

                m_simulatorIndex++;
            }

            return rvLocation;
        }


        /// <summary>
        /// Stops the location sending.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            StopSending();
        }


        /// <summary>
        /// Stops location sending and navigate to the login page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            StopSending();

            NavigationService.GoBack();
        }


        /// <summary>
        /// Stops location sending.
        /// </summary>
        private void StopSending()
        {
            //if (m_workerThread != null)
            //{
            //    m_workerThread.Abort();
            //}

            m_endWork = true;

            while (m_workerThread.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(500);
            }

            SetUIState(false);
        }


        /// <summary>
        /// Enable or disable UI elements.
        /// </summary>
        /// <param name="isStarted">The running state of the sender service.</param>
        private void SetUIState(bool isStarted)
        {
            m_rbRealData.IsEnabled = !isStarted;
            m_rbSimulatorData.IsEnabled = !isStarted;
            m_btnStart.IsEnabled = !isStarted;
            m_btnStop.IsEnabled = isStarted;
            m_btnLogout.IsEnabled = !isStarted;
        }
        #endregion
    }
}