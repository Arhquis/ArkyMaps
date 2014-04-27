using ArkyMapsClient.ArkyMapServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArkyMapsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //[CallbackBehavior(
    //ConcurrencyMode = ConcurrencyMode.Single,
    //UseSynchronizationContext = false)]
    public partial class MainWindow : Window
    {
        MapServiceClient client = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MapServiceCallbackHandler handler = new MapServiceCallbackHandler();
            client = new MapServiceClient(new InstanceContext(handler));

            client.Open();

            client.Login("test", "test");

            m_mapControl.LoadMapControl(handler);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.Logout(1);
            client.Close();
        }
    }
}
