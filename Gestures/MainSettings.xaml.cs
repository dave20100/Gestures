using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tobii.Interaction.Wpf;
namespace Gestures
{
    /// <summary>
    /// Logika interakcji dla klasy MainSettings.xaml
    /// </summary>
    public partial class MainSettings : Window
    {


        Timer timeForGesture = new Timer();
        Timer loader = new Timer();
        public event EventHandler settingsWindowSavedChanges;

        public MainSettings()
        {
            InitializeComponent();
            Application.Current.MainWindow.Visibility = Visibility.Hidden;
            loader.Interval = 2000;
            loader.Elapsed += Loader_Elapsed;
            loader.AutoReset = false;
            timeForGesture.Interval = 4000;
            timeForGesture.Elapsed += TimeForGesture_Elapsed;
            timeForGesture.AutoReset = false;
        }

        private void ShowSettingsButton(object sender, RoutedEventArgs e)
        {
            GestureSettingsWindow settingsWindow = new GestureSettingsWindow();
            settingsWindow.SaveChanges += SettingsWindow_SaveChanges;
            settingsWindow.Closed += SettingsWindow_Closed;
            loader.Stop();
            this.Topmost = false;
            mainButton.SetIsGazeAware(false);
            settingsWindow.ShowDialog();
            this.Topmost = true;
        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            mainButton.SetIsGazeAware(true);
        }

        private void SettingsWindow_SaveChanges(object sender, EventArgs e)
        {
            settingsWindowSavedChanges?.Invoke(this, EventArgs.Empty);
        }
        
        private void Button_HasGazeChanged(object sender, Tobii.Interaction.Wpf.HasGazeChangedRoutedEventArgs e)
        {
            if (e.HasGaze == true && loader.Enabled == false)
            {
                loader.Start();
                ((Button)sender).Background = new SolidColorBrush(Colors.Gray);
            }
            if (e.HasGaze == false && loader.Enabled == true)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Beige);
                loader.Stop();
            }
        }

        private void Loader_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Application.Current.MainWindow.Visibility = Visibility.Visible;
                this.Hide();
            });    
            timeForGesture.Start();
        }

        private void TimeForGesture_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Application.Current.MainWindow.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Visible;
                mainButton.Background = new SolidColorBrush(Colors.Beige);
            });
        }
    }
}
