using ArkyMapsClient.ArkyMapServiceReference;
using System.Windows;
using System.Windows.Controls;

namespace ArkyMapsClient.Views
{
    /// <summary>
    /// The class represents a login view.
    /// </summary>
    public partial class LoginView : UserControl
    {
        #region constants
        private const string ERROR_MESSAGE = "Failed to login!";
        #endregion


        #region attributes
        private MapServiceClient m_serviceClient = null;
        private MapServiceCallbackHandler m_callbackHandler = null;
        #endregion


        #region properties
        /// <summary>
        /// Gets or sets the <see cref="MainWindow"/> instance.
        /// </summary>
        public MainWindow MainWindow { get; set; }
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        public LoginView()
        {
            InitializeComponent();

            SetUIState(true);

#if (DEBUG)
            m_tbUsername.Text = "test";
            m_pbPassword.Password = "test";
#endif
        }
        #endregion


        #region button events
        /// <summary>
        /// Perform client login.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            m_lcError.Content = string.Empty;

            bool succeeded = MainWindow.Login(m_tbUsername.Text, m_pbPassword.Password);

            if (!succeeded)
            {
                m_lcError.Content = ERROR_MESSAGE;
            }
            else
            {
                SetUIState(false);
            }
        }


        /// <summary>
        /// Perform client logout.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Logout();

            SetUIState(true);
        }
        #endregion


        #region UI state
        /// <summary>
        /// Sets the state of UI elements according to the login state.
        /// </summary>
        /// <param name="isLogin">Login state of client.</param>
        private void SetUIState(bool isLogin)
        {
            m_tbUsername.IsEnabled = isLogin;
            m_pbPassword.IsEnabled = isLogin;
            m_btnLogin.IsEnabled = isLogin;
            m_btnLogout.IsEnabled = !isLogin;
        }
        #endregion
    }
}
