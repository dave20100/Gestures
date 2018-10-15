using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Gestures
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        gestureField mainGestureField;
        public MainWindow()
        {
            //if (!File.Exists("Settings.xml"))
            //{
            generateDefaultSettings();
            //}
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            MainSettings settingsWindow = new MainSettings();
            settingsWindow.Show();
            settingsWindow.Closed += Okno_Closed;
        }

        private void Okno_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainGestureField = new gestureField(gestureCanvas);
            mainGestureField.changeAmountOfDots(3);
            mainGestureField.loadSettings();
        }

        private void generateDefaultSettings()
        {
            XmlDocument settings = new XmlDocument();
            XmlNode root = settings.CreateElement("gestures");
            settings.AppendChild(root);

            XmlNode gesture = settings.CreateElement("gesture");
            XmlAttribute gestureCode = settings.CreateAttribute("gestureCode");
            XmlAttribute gestureType = settings.CreateAttribute("gestureType");
            XmlAttribute gestureCommand = settings.CreateAttribute("gestureCommand");

            gestureCode.Value = "[1,1][2,2]";
            gestureType.Value = "1";
            gestureCommand.Value = "Enter";
            gesture.Attributes.Append(gestureCode);
            gesture.Attributes.Append(gestureType);
            gesture.Attributes.Append(gestureCommand);
            gestureCode = settings.CreateAttribute("gestureCode");

            XmlNode gesture1 = settings.CreateElement("gesture");
            XmlAttribute gestureCode1 = settings.CreateAttribute("gestureCode");
            XmlAttribute gestureType1 = settings.CreateAttribute("gestureType");
            XmlAttribute gestureCommand1 = settings.CreateAttribute("gestureCommand");

            gestureCode1.Value = "[0,0][0,1][1,0]";
            gestureType1.Value = "2";
            gestureCommand1.Value = @"C:\Program Files (x86)\Google\Chrome\Application\Chrome.exe";
            gesture1.Attributes.Append(gestureCode1);
            gesture1.Attributes.Append(gestureType1);
            gesture1.Attributes.Append(gestureCommand1);

            XmlNode gesture2 = settings.CreateElement("gesture");
            XmlAttribute gestureCode2 = settings.CreateAttribute("gestureCode");
            XmlAttribute gestureType2 = settings.CreateAttribute("gestureType");
            XmlAttribute gestureCommand2 = settings.CreateAttribute("gestureCommand");

            gestureCode2.Value = "[1,1][1,2]";
            gestureType2.Value = "3";
            gestureCommand2.Value = "Return";
            gesture2.Attributes.Append(gestureCode2);
            gesture2.Attributes.Append(gestureType2);
            gesture2.Attributes.Append(gestureCommand2);
            root.AppendChild(gesture);
            root.AppendChild(gesture1);
            root.AppendChild(gesture2);
            settings.Save("Settings.xml");
        }
    }
}