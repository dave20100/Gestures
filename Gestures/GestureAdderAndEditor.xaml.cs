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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GestureAdderAndEditor : Window
    {
        public GestureAdderAndEditor()
        {
            List<int> l = new List<int>() { 0, 1, 2 };
            InitializeComponent();
            typeBox.ItemsSource = l;
        }

        private void TypeChanged(object sender, SelectionChangedEventArgs e)
        {
            codeBox.Text = "";
            parameterBox.Text = "";
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
