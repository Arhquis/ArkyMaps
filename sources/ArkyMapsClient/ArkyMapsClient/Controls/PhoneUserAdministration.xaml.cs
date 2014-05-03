using ArkyMapsClient.Views;
using ArkyMapsDomainModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ArkyMapsClient.Controls
{
    /// <summary>
    /// The class administer the <see cref="PhoneUser"/> class.
    /// </summary>
    public partial class PhoneUserAdministration : UserControl
    {
        #region constants
        private const string ERROR_MESSAGE_INPUT_NOT_VALID = "Input not valid!";
        private const string ERROR_MESSAGE_SAVE_FAILED = "Save failed!";
        #endregion


        #region properties
        /// <summary>
        /// Gets or sets the parent <see cref="AdministrationView"/>.
        /// </summary>
        internal AdministrationView AdministrationView { get; set; }


        /// <summary>
        /// Gets or set the phone user to administer.
        /// </summary>
        internal PhoneUser PhoneUser { get; set; }
        #endregion


        #region events
        /// <summary>
        /// Fires if the modification process finished.
        /// </summary>
        internal event EventHandler ModificationFinished;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneUserAdministration"/> class.
        /// </summary>
        public PhoneUserAdministration()
        {
            InitializeComponent();
        }
        #endregion


        #region lifetime
        /// <summary>
        /// Loads the UI elements according to the <see cref="PhoneUser"/> set for administer.
        /// </summary>
        internal void Load()
        {
            if (PhoneUser != null)
            {
                m_tbUsername.Text = PhoneUser.UserName;
                m_pbPassword.Password = PhoneUser.Password;
                m_pbRetypePassword.Password = PhoneUser.Password;
                m_tbName.Text = PhoneUser.Name;
                m_rbMale.IsChecked = PhoneUser.Male;
                m_rbFemale.IsChecked = !PhoneUser.Male;
                m_tbEmail.Text = PhoneUser.Email;
            }
            else
            {
                m_tbUsername.Text = string.Empty;
                m_pbPassword.Password = string.Empty;
                m_pbRetypePassword.Password = string.Empty;
                m_tbName.Text = string.Empty;
                m_rbMale.IsChecked = true;
                m_rbFemale.IsChecked = false;
                m_tbEmail.Text = string.Empty;
            }
        }
        #endregion


        #region button events
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                PhoneUser phoneUser = new PhoneUser
                {
                    UserName = m_tbUsername.Text,
                    Password = m_pbPassword.Password,
                    Name = m_tbName.Text,
                    Male = (bool)m_rbMale.IsChecked,
                    Email = m_tbEmail.Text
                };

                bool isSucceeded = false;

                if (PhoneUser == null)
                {
                    isSucceeded = AdministrationView.CreatePhoneUser(phoneUser);
                }
                else
                {
                    phoneUser.ID = PhoneUser.ID;

                    isSucceeded = AdministrationView.ModifyPhoneUser(phoneUser);
                }

                if (isSucceeded)
                {
                    Finished();
                }
                else
                {
                    MessageBox.Show(ERROR_MESSAGE_SAVE_FAILED);
                }
            }
            else
            {
                MessageBox.Show(ERROR_MESSAGE_INPUT_NOT_VALID);
            }
        }


        /// <summary>
        /// Validates the form data.
        /// </summary>
        /// <returns>True, if validation succeeded, false otherwise.</returns>
        private bool Validate()
        {
            bool rvValid = false;

            rvValid =
                !string.IsNullOrWhiteSpace(m_tbUsername.Text) &&
                !string.IsNullOrWhiteSpace(m_pbPassword.Password) &&
                !string.IsNullOrWhiteSpace(m_tbName.Text) &&
                !string.IsNullOrWhiteSpace(m_tbEmail.Text) &&
                m_pbPassword.Password.Equals(m_pbRetypePassword.Password);

            return rvValid;
        }


        /// <summary>
        /// Cancel changes.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Finished();
        }


        private void Finished()
        {
            if (ModificationFinished != null)
            {
                ModificationFinished(this, new EventArgs());
            }
        }
        #endregion
    }
}
