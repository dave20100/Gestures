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
using System.Windows.Shapes;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;
namespace Gestures
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GestureAdderAndEditor : Window
    {
        public Gesture createdGesture { get { return new Gesture(codeBox.Text, typeBox.SelectedIndex, parameterBox.Text); } }
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
            paramChoseButton.KeyDown -= ParamChoseButton_KeyDown;
            if(typeBox.SelectedIndex == 0)
            {
                parameterBox.Focusable = true;
                paramChoseButton.IsEnabled = false;
                return;
            }
            paramChoseButton.IsEnabled = true;
            if(typeBox.SelectedIndex == 1)
            {
                parameterBox.Focusable = false;
                paramChoseButton.Click += chooseFile; 
            }
            if (typeBox.SelectedIndex == 2)
            {
                parameterBox.Focusable = false;
                paramChoseButton.Click += inputShortcut;
                paramChoseButton.KeyDown += ParamChoseButton_KeyDown;
            }
        }

        private void ParamChoseButton_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            if (parameterBox.Text == "")
            {
                parameterBox.Text += Enum.GetName(typeof(Key), e.Key);
            }
            else
            {
                parameterBox.Text += "+" + Enum.GetName(typeof(Key), e.Key);
            }
        }

        private void chooseFile(object sender, RoutedEventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();
                parameterBox.Text = dialog.FileName;
            }
        }
        

        private void inputShortcut(object sender, RoutedEventArgs e)
        {
            parameterBox.Text = "";
        }
        
         

        private void TypeChanged(object sender, SelectionChangedEventArgs e)
        {
            codeBox.Text = "";
            parameterBox.Text = "";
        }

        private void AcceptGestureButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
