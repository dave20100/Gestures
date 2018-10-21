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
        public event EventHandler SaveChanges;

        public GestureSettingsWindow()
        {
            InitializeComponent();
            listOfActions.Items.Add("Number Code Type Parameter");
            
            loadSettings();
            //MessageBox.Show(listOfActions.Items[1].ToString());
        }
        public void loadSettings(string settingsFileName = "Settings.xml")
        {
            listOfActions.Items.Clear();

            listOfActions.Items.Add("Number Code Type Parameter");
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

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditOrAddButton(object sender, RoutedEventArgs e)
        {

            if(listOfActions.SelectedItem != null && listOfActions.SelectedItem != listOfActions.Items[0])
            {
                listOfActions.Items.Remove(listOfActions.SelectedItem);
            }

        }

        private void AcceptChangesButton(object sender, RoutedEventArgs e)
        {
            XmlDocument settings = new XmlDocument();
            XmlNode root = settings.CreateElement("gestures");
            settings.AppendChild(root);
            foreach (var gestureParameters in listOfActions.Items)
            {
                if(gestureParameters == listOfActions.Items[0])
                {
                    continue;
                }
                XmlNode gesture = settings.CreateElement("gesture");
                XmlAttribute gestureCode = settings.CreateAttribute("gestureCode");
                XmlAttribute gestureType = settings.CreateAttribute("gestureType");
                XmlAttribute gestureCommand = settings.CreateAttribute("gestureCommand");

                List<string> listed = gestureParameters.ToString().Split().ToList<string>();

                gestureCode.Value = listed[1];
                gestureType.Value = listed[2];
                gestureCommand.Value = listed[3];
                gesture.Attributes.Append(gestureCode);
                gesture.Attributes.Append(gestureType);
                gesture.Attributes.Append(gestureCommand);
                gestureCode = settings.CreateAttribute("gestureCode");

                root.AppendChild(gesture);
            }
            settings.Save("Settings.xml");
            SaveChanges?.Invoke(this, EventArgs.Empty);
            loadSettings();
        }
    }
}
