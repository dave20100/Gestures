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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestures
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        gestureField f;
        public MainWindow()
        {
            
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
            f = new gestureField(gestureCanvas);
            f.changeAmountOfDots(3);
        }
        
        
    }
}
