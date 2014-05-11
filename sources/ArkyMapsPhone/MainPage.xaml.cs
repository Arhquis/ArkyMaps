using ArkyMapsPhone.ArkyPhoneServiceReference;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.ServiceModel;
using System.Windows;
using System.Windows.Navigation;

namespace ArkyMapsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region constants
        private const string ERROR_MESSAGE = "Failed to login!";
        private const string SERVICE_ENDPOINT_CONFIGURATION_NAME = "BasicHttpBinding_IPhoneService";
        private const string SERVICE_REMOTE_ADDRESS = "http://10.6.11.16:8081/PhoneService/service";
        private const string PAGE_SENDER_PAGE = "/SenderPage.xaml";
        private readonly Uri SENDER_PAGE_URI = new Uri("/SenderPage.xaml", UriKind.Relative);
        #endregion


        #region attributes
        private PhoneServiceClient m_serviceClient;
        #endregion


        #region constructors
        public MainPage()
        {
            InitializeComponent();

#if DEBUG
            m_tbServiceAddress.Text = "http://10.6.11.16:8081/PhoneService/service";
            m_tbUsername.Text = "test4";
            m_tbPassword.Text = "test4";
#endif
        }
        #endregion


        #region lifetime
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SetUIState(false);

            try
            {
                m_serviceClient = new PhoneServiceClient("BasicHttpBinding_IPhoneService", "http://10.6.11.16:8081/PhoneService/service");
                m_serviceClient.OpenCompleted += Client_OpenCompleted;
                m_serviceClient.OpenAsync();
            }
            catch (Exception)
            {
                RollbackOnError();
            }
        }


        private void Client_OpenCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                m_serviceClient.LoginCompleted += Client_LoginCompleted;

                m_serviceClient.LoginAsync(m_tbUsername.Text, m_tbPassword.Text);
            }
            catch (Exception)
            {
                RollbackOnError();
            }
        }


        private void Client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            SetUIState(true);

            PhoneApplicationService.Current.State["userId"] =  e.Result;
            PhoneApplicationService.Current.State["serviceClient"] = m_serviceClient;
            
            NavigationService.Navigate(SENDER_PAGE_URI);
        }


        private void SetUIState(bool enabled)
        {
            m_tbServiceAddress.IsEnabled = enabled;
            m_tbUsername.IsEnabled = enabled;
            m_tbPassword.IsEnabled = enabled;
            m_btnLogin.IsEnabled = enabled;
        }


        private void RollbackOnError()
        {
            CleanUp();
            m_serviceClient = null;

            MessageBox.Show("Failed to login!");

            SetUIState(true);
        }


        private void CleanUp()
        {
            if (m_serviceClient != null && m_serviceClient.State != CommunicationState.Closed)
            {
                m_serviceClient.CloseAsync();
            }

            m_serviceClient = null;

            PhoneApplicationService.Current.State["userId"] = null;
            PhoneApplicationService.Current.State["serviceClient"] =  null;
        }
        #endregion


        #region navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.CleanUp();
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (!IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                MessageBoxResult result = MessageBox.Show("This app accesses your phone's location. Is that ok?", "Location", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }
                
                IsolatedStorageSettings.ApplicationSettings.Save();
                
                if (!e.Uri.Equals(SENDER_PAGE_URI))
                {
                    this.CleanUp();
                }
            }
        }
        #endregion
    }
}