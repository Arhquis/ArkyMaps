using ArkyPhoneTest.ArkyPhoneServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            long valami = client.Login("a", "b");

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
