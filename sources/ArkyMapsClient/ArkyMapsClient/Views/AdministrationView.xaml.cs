using ArkyMapsDomainModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ArkyMapsClient.Views
{
    /// <summary>
    /// Interaction logic for AdministrationView.xaml
    /// </summary>
    public partial class AdministrationView : UserControl
    {
        #region constants
        private const string MESSAGE_BOX_TEXT_DELETE_PHONE_USER = "Do you want to delete the selected phone user?";
        private const string MESSAGE_BOX_CAPTION_DELETE_PHONE_USER = "Delete phone user...";
        private const string ERROR_MESSAGE_DELETE_FAILED = "Delete failed!";
        #endregion


        #region attributes
        private PhoneUser m_selectedUser = null;
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
        /// Initializes a new instance of the <see cref="AdministrationView"/> class.
        /// </summary>
        public AdministrationView()
        {
            InitializeComponent();

            m_phoneUserAdministration.AdministrationView = this;
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Loads the view.
        /// </summary>
        internal void Load()
        {
            LoadPhoneUsers();
            IsViewLoaded = true;
        }


        /// <summary>
        /// Loads the view.
        /// </summary>
        internal void Unload()
        {
            m_phoneUserList.PhoneUserCollection = new List<PhoneUser>();
            SetUIState(true);
            IsViewLoaded = false;
        }
        #endregion


        #region phone user list
        /// <summary>
        /// Refresh user list.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPhoneUsers();
        }


        /// <summary>
        /// Loads <see cref="PhoneUser"/> entitites and refresh user list.
        /// </summary>
        private void LoadPhoneUsers()
        {
            m_phoneUserList.PhoneUserCollection = MainWindow.QueryPhoneUsers();
        }


        /// <summary>
        /// Sets the view state according to user selection.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        void PhoneUserList_PhoneUserSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasItemSelected = e.AddedItems.Count > 0;

            m_btnModify.IsEnabled = hasItemSelected;
            m_btnDelete.IsEnabled = hasItemSelected;

            if (hasItemSelected)
            {
                m_selectedUser = (PhoneUser)e.AddedItems[0];
            }
            else
            {
                m_selectedUser = null;
            }
        }
        #endregion


        #region phone user administration
        #region button events
        /// <summary>
        /// Creates a new <see cref="PhoneUser"/>.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            m_phoneUserAdministration.PhoneUser = null;
            m_phoneUserAdministration.Load();
            SetUIState(false);
            m_phoneUserAdministration.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Modify the selected <see cref="PhoneUser"/>.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            m_phoneUserAdministration.PhoneUser = m_selectedUser;
            m_phoneUserAdministration.Load();
            SetUIState(false);
            m_phoneUserAdministration.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Sets the state of the UI.
        /// </summary>
        /// <param name="enabled">Indicates whether the UI elements are enabled.</param>
        private void SetUIState(bool enabled)
        {
            m_gbUserList.IsEnabled = enabled;
            m_gbAdministration.IsEnabled = enabled;
            m_phoneUserList.IsEnabled = enabled;
        }


        /// <summary>
        /// Sets UI back to normal state and reload user list.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void PhoneUserAdministration_ModificationFinished(object sender, EventArgs e)
        {
            m_phoneUserAdministration.Visibility = Visibility.Hidden;
            SetUIState(true);
            LoadPhoneUsers();
        }


        /// <summary>
        /// Delete the selected <see cref="PhoneUser"/>.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(MESSAGE_BOX_TEXT_DELETE_PHONE_USER, MESSAGE_BOX_CAPTION_DELETE_PHONE_USER, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                bool succeeded = false;

                succeeded = MainWindow.DeletePhoneUser(m_selectedUser);

                if (succeeded)
                {
                    LoadPhoneUsers();
                }
                else
                {
                    MessageBox.Show(ERROR_MESSAGE_DELETE_FAILED);
                }
            }
        }
        #endregion


        #region user administration
        /// <summary>
        /// Creates a new <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">The <see cref="PhoneUser"/> to create.</param>
        /// <returns>True if creation was successful.</returns>
        internal bool CreatePhoneUser(PhoneUser phoneUser)
        {
            return MainWindow.CreatePhoneUser(phoneUser);
        }


        /// <summary>
        /// Modify the <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">The <see cref="PhoneUser"/> to modify.</param>
        /// <returns>True if modification was successful.</returns>
        internal bool ModifyPhoneUser(PhoneUser phoneUser)
        {
            return MainWindow.ModifyPhoneUser(phoneUser);
        }


        /// <summary>
        /// Delete the <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">The <see cref="PhoneUser"/> to delete.</param>
        /// <returns>True if delete was successful.</returns>
        internal bool DeletePhoneUser(PhoneUser phoneUser)
        {
            return MainWindow.DeletePhoneUser(phoneUser);
        }
        #endregion
        #endregion
    }
}
