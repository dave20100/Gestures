using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Gestures
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class GestureSettingsWindow : Window
    {
        public GestureSettingsWindow()
        {
            InitializeComponent();
            listOfActions.Items.Add("Number Code Type Parameter");
            loadSettings();
            //MessageBox.Show(listOfActions.Items[1].ToString());
        }
        public void loadSettings(string settingsFileName = "Settings.xml")
        {
            XmlDocument settings = new XmlDocument();
            settings.Load(settingsFileName);
            int count = 1;
            foreach (XmlNode node in settings.DocumentElement)
            {
                try
                {
                    listOfActions.Items.Add(count + ": " + node.Attributes["gestureCode"].Value + " " + Int32.Parse(node.Attributes["gestureType"].Value) + " " + node.Attributes["gestureCommand"].Value);
                }
                catch
                {
                }
                count++;
            }
            //MessageBox.Show(countBad.ToString(), "Amount of bad gestures");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
