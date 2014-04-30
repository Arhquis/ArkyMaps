using ArkyMapsClient.ArkyMapServiceReference;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;

namespace ArkyMapsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

            m_realTimeView.Load(client, handler);

            m_realTimeViewTabItem.IsSelected = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.Logout(1);
            client.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            m_realTimeView.Unload();

            if (client != null && client.State != CommunicationState.Closed)
            {
                client.Logout(1);
                client.Close();
            }
        }
    }
}
