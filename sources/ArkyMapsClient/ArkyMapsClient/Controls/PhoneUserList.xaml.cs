using ArkyMapsDomainModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ArkyMapsClient.Controls
{
    /// <summary>
    /// Interaction logic for PhoneUserList.xaml
    /// </summary>
    public partial class PhoneUserList : UserControl
    {
        #region attributes
        private List<PhoneUser> m_phoneUsers;
        #endregion


        #region events
        /// <summary>
        /// Fires if <see cref="PhoneUser"/> selection changed.
        /// </summary>
        public event SelectionChangedEventHandler PhoneUserSelectionChanged;
        #endregion


        #region properties
        /// <summary>
        /// Gets or set the <see cref="PhoneUser"/> collection.
        /// </summary>
        public IEnumerable<PhoneUser> PhoneUserCollection
        {
            get { return m_phoneUsers; }
            set {
                m_phoneUsers = new List<PhoneUser>(value);
                m_list.ItemsSource = m_phoneUsers;
            }
        }
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneUserList"/> class.
        /// </summary>
        public PhoneUserList()
        {
            InitializeComponent();
        }
        #endregion


        #region list events
        /// <summary>
        /// Fires the <see cref="PhoneUserSelectionChanged"/> event if selection changed in the list.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>
        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhoneUserSelectionChanged != null)
            {
                PhoneUserSelectionChanged(this, e);
            }
        }
        #endregion
    }
}
