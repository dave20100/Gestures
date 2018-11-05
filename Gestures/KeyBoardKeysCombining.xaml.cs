using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy KeyBoardKeysCombining.xaml
    /// </summary>
    public partial class KeyBoardKeysCombining : Window
    {

        private ObservableCollection<String> listOfKeynames = new ObservableCollection<String>();
        public ObservableCollection<String> listOfSelectedKeynames = new ObservableCollection<String>();
        public List<string> returnList = new List<string>();
        public KeyBoardKeysCombining()
        {
            foreach (var keyname in Enum.GetNames(typeof(Key)))
            {
                listOfKeynames.Add(keyname);
            };
            InitializeComponent();
            keyNamesList.MouseDoubleClick += KeyNamesList_MouseDoubleClick;
            keyNamesList.ItemsSource = listOfKeynames;
            selectedKeyNamesList.ItemsSource = listOfSelectedKeynames;
            selectedKeyNamesList.MouseDoubleClick += SelectedKeyNamesList_MouseDoubleClick;
        }

        private void KeyNamesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            listOfSelectedKeynames.Add(keyNamesList.SelectedItem as string);
            if (keyNamesList.SelectedItem != null)
            {
                listOfKeynames.Remove(keyNamesList.SelectedItem as string);
            }
        }
        private void SelectedKeyNamesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            listOfKeynames.Add(selectedKeyNamesList.SelectedItem as string);
            if (selectedKeyNamesList.SelectedItem != null)
            {
                listOfSelectedKeynames.Remove(selectedKeyNamesList.SelectedItem as string);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(var keyName in selectedKeyNamesList.Items)
            {
                returnList.Add(keyName.ToString());
            }
            DialogResult = true;
        }
    }
}
