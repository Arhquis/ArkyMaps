using ArkyMapsClient.ArkyMapServiceReference;
using System.ServiceModel;
using System.Windows;

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

            //client.Open();

            //client.Login("test", "test");

            //m_mapControl.LoadMapControl(handler);
            m_realTimeView.Load(handler);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.Logout(1);
            client.Close();
        }
    }
}
