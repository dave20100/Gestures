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
            List<string> l = new List<string>() { "Simulate text", "Start program", "Keyboard shortcut" };
            InitializeComponent();
            typeBox.ItemsSource = l;
            typeBox.SelectionChanged += TypeBox_SelectionChanged;
            paramChoseButton.IsEnabled = false;
        }

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            paramChoseButton.Click -= chooseFile;
            paramChoseButton.Click -= inputShortcut;
            if(typeBox.SelectedIndex == 0)
            {
                paramChoseButton.IsEnabled = false;
                return;
            }
            paramChoseButton.IsEnabled = true;
            if(typeBox.SelectedIndex == 1)
            {
                paramChoseButton.Click += chooseFile; ;
            }
            if (typeBox.SelectedIndex == 2)
            {
                paramChoseButton.Click += inputShortcut; 
            }
        }

        private void chooseFile(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AAA");
        }
        private void inputShortcut(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BBB");

        }



        private void TypeChanged(object sender, SelectionChangedEventArgs e)
        {
            codeBox.Text = "";
            parameterBox.Text = "";
        }

        

    }
}
