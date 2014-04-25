using ArkyPhoneTest.ArkyPhoneServiceReference;
using System.Threading;
using System.Windows;

namespace ArkyPhoneTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region attributes
        PhoneServiceClient client = null;
        Thread thread = null;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new PhoneServiceClient();

            client.Open();

            long valami = client.Login("test2", "test2");

            thread = new Thread(Run);

            thread.Start(valami);
        }


        public void Run(object obj)
        {
            while (true)
            {
                client.NewLocation((long)obj, 10, 10);

                Thread.Sleep(1000);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            thread.Abort();
        }
    }
}
