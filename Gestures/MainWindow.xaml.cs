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
            if (!File.Exists("Settings.xml"))
            {
                generateDefaultSettings();
            }
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            MainSettings okno = new MainSettings();
            okno.Show();
            okno.Closed += Okno_Closed;
        }

        private void Okno_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainGestureField = new gestureField(gestureCanvas);
            mainGestureField.changeAmountOfDots(3);
            mainGestureField.loadSetting();
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
            gestureCode.Value = "[0,0][1,1]";
            gestureType.Value = "3";
            gestureCommand.Value = "Enter";
            gesture.Attributes.Append(gestureCode);
            gesture.Attributes.Append(gestureType);
            gesture.Attributes.Append(gestureCommand);
            root.AppendChild(gesture);
            settings.Save("Settings.xml");
        }
        
    }
}
