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

namespace Gestures
{
    /// <summary>
    /// Logika interakcji dla klasy MainSettings.xaml
    /// </summary>
    public partial class MainSettings : Window
    {
        public event EventHandler settingsWindowSavedChanges;

        public MainSettings()
        {
            InitializeComponent();
        }
        
        private void HideFieldButton(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.IsVisible)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Gray);
                Application.Current.MainWindow.Hide();
            }
            else
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Beige);
                Application.Current.MainWindow.Show();
            }
        }

        private void ShowSettingsButton(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            GestureSettingsWindow settingsWindow = new GestureSettingsWindow();
            settingsWindow.Closed += SettingsWindow_Closed;
            settingsWindow.SaveChanges += SettingsWindow_SaveChanges;
            settingsWindow.ShowDialog();
        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
        }

        private void SettingsWindow_SaveChanges(object sender, EventArgs e)
        {
            settingsWindowSavedChanges?.Invoke(this, EventArgs.Empty);
        }
    }
}
