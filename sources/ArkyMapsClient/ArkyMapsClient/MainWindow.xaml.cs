using ArkyMapsClient.ArkyMapServiceReference;
using ArkyMapsDomainModel;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;

namespace ArkyMapsClient
{
    /// <summary>
    /// The main view and controller of the client.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region attributes
        private MapServiceClient m_serviceClient = null;
        private MapServiceCallbackHandler m_callbackHandler = null;
        private ClientUser m_client = null;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            m_loginView.MainWindow = this;
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Perform client user login.
        /// </summary>
        /// <param name="username">Username of user try to login.</param>
        /// <param name="password">Password of user try to login.</param>
        /// <returns>The <see cref="ClientUser"/> entity of login person, or null, if login was not successfull.</returns>
        internal bool Login(string username, string password)
        {
            bool rvSucceeded = false;

            try
            {
                m_callbackHandler = new MapServiceCallbackHandler();
                m_serviceClient = new MapServiceClient(new InstanceContext(m_callbackHandler));

                m_serviceClient.Open();

                m_client = m_serviceClient.Login(username, password);

                if (m_client != null)
                {
                    m_realTimeView.Load(m_serviceClient, m_callbackHandler);

                    m_realTimeViewTabItem.IsSelected = true;
                    m_realTimeViewTabItem.IsEnabled = true;
                    m_administrationViewTabItem.IsEnabled = true;

                    rvSucceeded = true;
                }
                else
                {
                    m_serviceClient.Close();
                    m_serviceClient = null;
                }
            }
            catch (Exception)
            {
                Logout();
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Perform logout with currently logged in user.
        /// </summary>
        internal void Logout()
        {
            if (m_realTimeView.IsLoaded)
            {
                m_realTimeView.Unload();
            }

            if (m_serviceClient != null && m_serviceClient.State != CommunicationState.Closed)
            {
                if (m_client != null)
                {
                    m_serviceClient.Logout(m_client.ID);
                }

                m_serviceClient.Close();
            }

            m_realTimeViewTabItem.IsEnabled = false;
            m_administrationViewTabItem.IsEnabled = false;

            m_client = null;
            m_serviceClient = null;
        }


        /// <summary>
        /// On client closing log out the currently logged in user.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Logout();
        }
        #endregion
    }
}
