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
using System.Collections.ObjectModel;

namespace Gestures
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GestureAdderAndEditor : Window
    {
        public GestureAdderAndEditor(int type, string param, string code)
        {
            InitializeComponent();
            List<string> typeList = new List<string>() { "Simulate text", "Start program", "Keyboard shortcut" };
            Titlebox.Text = "Gesture Editor";
            Title = "Edit Gesture";
            typeBox.ItemsSource = typeList;
            typeBox.SelectionChanged += TypeBox_SelectionChanged;
            typeBox.SelectedIndex = type;
            
            parameterBox.Text = param;
            codeBox.Text = code;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public Gesture createdGesture
        {
            get
            {
                try
                {
                    return new Gesture(codeBox.Text, typeBox.SelectedIndex, parameterBox.Text);
                }
                catch
                {
                    return null;
                }
            }
        }
        public GestureAdderAndEditor()
        {
            List<string> typeList = new List<string>() { "Simulate text", "Start program", "Keyboard shortcut" };
            InitializeComponent();
            typeBox.ItemsSource = typeList;
            typeBox.SelectionChanged += TypeBox_SelectionChanged;
            paramChoseButton.IsEnabled = false;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            paramChoseButton.Click -= chooseFile;
            paramChoseButton.Click -= ParamChoseButton_KeyDown;
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
                paramChoseButton.Click += ParamChoseButton_KeyDown;
            }
        }

        private void ParamChoseButton_KeyDown(object sender, RoutedEventArgs e)
        {
            
            KeyBoardKeysCombining keyw = new KeyBoardKeysCombining();
            
            if (keyw.ShowDialog() == true)
            {
                parameterBox.Text = "";
                List<string> resultingKeynames = keyw.returnList;
                foreach (string keyname in resultingKeynames)
                {
                    parameterBox.Text += keyname + "+";
                }
                if (parameterBox.Text.Length > 0)
                {
                    parameterBox.Text = parameterBox.Text.Remove(parameterBox.Text.LastIndexOf('+'));
                }
            }
        }

        private void chooseFile(object sender, RoutedEventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.FileName != "")
                {
                    parameterBox.Text = dialog.FileName;
                }
            }
        }
        

        
        
         

        private void TypeChanged(object sender, SelectionChangedEventArgs e)
        {
            parameterBox.Text = "";
        }

        private void AcceptGestureButton_Click(object sender, RoutedEventArgs e)
        {
            if (createdGesture == null)
            {
                System.Windows.MessageBox.Show("Invalid code or empty parameter for gesture");
            }
            else
            {
                DialogResult = true;
            }
        }
    }
}
