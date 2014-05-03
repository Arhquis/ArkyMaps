using ArkyMapsDomainModel;
using ArkyMapsPhoneSimulator.ArkyPhoneServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Threading;

namespace ArkyMapsPhoneSimulator
{
    /// <summary>
    /// The class simulate a location sender phone client.
    /// </summary>
    public class Simulator
    {
        #region constants
        private const string LOCATION_SENT_MESSAGE_FORMAT_STRING = "Location data sent. (User id: {0}, location: {1})";
        private const string LOGIN_FAILED = "Login failed.";
        #endregion


        #region attributes
        private string m_fileLocation;
        private string m_username;
        private string m_password;
        private int m_sendPeriod;
        private int m_offset;
        private bool m_isVerbose;

        private List<LonLat> m_locations;
        private Thread m_worker;
        private PhoneServiceClient m_client;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Simulator"/> class.
        /// </summary>
        /// <param name="fileLocation">File location of simulation data.</param>
        /// <param name="username">Username of simulated user.</param>
        /// <param name="password">Password of simulated user.</param>
        /// <param name="sendPeriod">Time period of sending location in millisec.</param>
        /// <param name="offset">Start offset in location list.</param>
        /// <param name="isVerbose">Whether report operations to console or not.</param>
        public Simulator(string fileLocation, string username, string password, int sendPeriod, int offset, bool isVerbose)
        {
            m_fileLocation = fileLocation;
            m_username = username;
            m_password = password;
            m_sendPeriod = sendPeriod;
            m_offset = offset;
            m_isVerbose = isVerbose;
        }
        #endregion


        #region methods
        /// <summary>
        /// Loads location data from the file.
        /// </summary>
        public void LoadData()
        {
            m_locations = new List<LonLat>();

            using (StreamReader reader = new StreamReader(m_fileLocation))
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

                    m_locations.Add(location);
                }
            }
        }


        /// <summary>
        /// Start simulation.
        /// </summary>
        public void Start()
        {
            m_worker = new Thread(DoWork);
            m_worker.IsBackground = true;
            m_worker.Start();
        }


        /// <summary>
        /// Open service connection and periodically send location data.
        /// </summary>
        private void DoWork()
        {
            try
            {
                m_client = new PhoneServiceClient();

                m_client.Open();

                long userId = m_client.Login(m_username, m_password);

                if (userId == -1)
                {
                    Console.WriteLine(LOGIN_FAILED);
                    return;
                }

                int index = m_offset;

                while (true)
                {
                    LonLat location = m_locations[index];

                    m_client.NewPosition(userId, location.Longitude, location.Latitude);

                    if (m_isVerbose)
                    {
                        Console.WriteLine(string.Format(LOCATION_SENT_MESSAGE_FORMAT_STRING, userId, location));
                    }

                    index++;

                    if (index >= m_locations.Count)
                    {
                        index = 0;
                    }

                    Thread.Sleep(m_sendPeriod);
                }
            }
            catch (Exception ex)
            {
                if (m_client.State != CommunicationState.Closed)
                {
                    m_client.Close();
                }

                throw ex;
            }
        }


        /// <summary>
        /// Stop simulation.
        /// </summary>
        public void Stop()
        {
            m_worker.Abort();

            m_client.Close();
        }
        #endregion
    }
}
