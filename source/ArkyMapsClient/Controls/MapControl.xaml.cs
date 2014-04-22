using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ArkyMapsClient.Controls
{
    /// <summary>
    /// Interaction logic for MapCOntrol.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
        }


        public bool LoadMapControl(MapServiceCallbackHandler callbackHandler)
        {
            Uri gmapsUri = new Uri(String.Concat(System.AppDomain.CurrentDomain.BaseDirectory, "Resources/Gmaps.html"),
                UriKind.Absolute);
            m_webBrowser.Navigate(gmapsUri);

            callbackHandler.LocationSent += CallbackHandler_LocationSent;

            return true;
        }

        private  void CallbackHandler_LocationSent(object sender, LocationSentEventArgs e)
        {
            string formatString = "{0} - {1} - {2}";

            Console.WriteLine(string.Format(formatString, e.UserId, e.Lon, e.Lat));
        }
    }
}
