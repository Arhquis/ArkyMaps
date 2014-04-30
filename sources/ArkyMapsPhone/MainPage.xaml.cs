using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ArkyMapsPhone.Resources;
using ArkyMapsPhone.ArkyPhoneServiceReference;
using System.Threading;

namespace ArkyMapsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PhoneServiceClient m_client;
        private long m_userId;
        private Thread m_thread;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_client = new PhoneServiceClient();

            m_client.OpenCompleted += m_client_OpenCompleted;
            m_client.OpenAsync();
        }

        void m_client_OpenCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            m_client.LoginCompleted += m_client_LoginCompleted;
            m_client.LoginAsync("Test1", "Test1");
        }

        void m_client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            m_userId = e.Result;

            m_thread = new Thread(DoWork);
            m_thread.IsBackground = true;

            m_thread.Start();
        }


        private void DoWork()
        {
            while (true)
            {
                m_client.NewLocationAsync(m_userId, new LonLat {Longitude = 10.3, Latitude = 3.12});

                Thread.Sleep(3000);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            m_thread.Abort();

            m_client.CloseAsync();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}