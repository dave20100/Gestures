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
        public MainSettings()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.IsVisible)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.BlanchedAlmond);
                Application.Current.MainWindow.Hide();
            }
            else
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Beige);
                Application.Current.MainWindow.Show();
            }
        }
    }
}
