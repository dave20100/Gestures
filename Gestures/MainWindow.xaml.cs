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
using Tobii.Interaction.Framework;
using Tobii.Interaction;
using WindowsInput;
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
            this.Show();
            MainSettings settingsWindow = new MainSettings();
            settingsWindow.settingsWindowSavedChanges += SettingsWindow_settingsWindowSavedChanges;
            settingsWindow.Show();
            settingsWindow.Closed += Okno_Closed;
        }
        

        private void SettingsWindow_settingsWindowSavedChanges(object sender, EventArgs e)
        {
            mainGestureField = new gestureField(gestureCanvas);
            mainGestureField.loadSettings();
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

       public static void generateDefaultSettings()
        {
            List<Gesture> listOfGesturesDefault = new List<Gesture>();
            listOfGesturesDefault.Add(new Gesture("[0,0][1,1]", 1, @"C:\Program Files (x86)\Google\Chrome\Application\Chrome.exe"));
            listOfGesturesDefault.Add(new Gesture("[0,0][1,0]", 0, @"!@#$%^&*()_+{}<>?))"));
            listOfGesturesDefault.Add(new Gesture("[0,0][0,1]", 2, "LeftCtrl+C"));
            listOfGesturesDefault.Add(new Gesture("[1,1][1,2]", 2, "LeftCtrl+V"));
            listOfGesturesDefault.Add(new Gesture("[0,1][1,1][0,1]", 2, "LeftCtrl+Z"));
            XmlDocument settings = new XmlDocument();
            XmlNode root = settings.CreateElement("gestures");
            settings.AppendChild(root);
            foreach (var gestureParameters in listOfGesturesDefault)
            {
                XmlNode gesture = settings.CreateElement("gesture");
                XmlAttribute gestureCode = settings.CreateAttribute("gestureCode");
                XmlAttribute gestureType = settings.CreateAttribute("gestureType");
                XmlAttribute gestureCommand = settings.CreateAttribute("gestureCommand");

                gestureCode.Value = gestureParameters.code;
                gestureType.Value = gestureParameters.type.ToString();
                gestureCommand.Value = gestureParameters.command;
                gesture.Attributes.Append(gestureCode);
                gesture.Attributes.Append(gestureType);
                gesture.Attributes.Append(gestureCommand);
                gestureCode = settings.CreateAttribute("gestureCode");

                root.AppendChild(gesture);
            }
            settings.Save("Settings.xml");
        }
    }
}