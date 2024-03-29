﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Xml;

namespace Gestures
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class GestureSettingsWindow : Window
    {
        bool changesMade;
        public event EventHandler SaveChanges;
        private ObservableCollection<Gesture> listOfGestures = new ObservableCollection<Gesture>();

        public GestureSettingsWindow()
        {
            InitializeComponent();
            changesMade = false;
            listOfGesturesToShow.ItemsSource = listOfGestures;
            loadSettings();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public void loadSettings(string settingsFileName = "Settings.xml")
        {
            listOfGestures.Clear();
            XmlDocument settings = new XmlDocument();
            settings.Load(settingsFileName);
            foreach (XmlNode node in settings.DocumentElement)
            {
                listOfGestures.Add(new Gesture(node.Attributes["gestureCode"].Value, Int32.Parse(node.Attributes["gestureType"].Value), node.Attributes["gestureCommand"].Value));   
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (changesMade)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to close without saving", "You have unsaved changes", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnClosing(e);
        }
        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            changesMade = true;
            if(listOfGesturesToShow.SelectedItem != null)
            {
                listOfGestures.Remove(listOfGesturesToShow.SelectedItem as Gesture);
            }
        }


        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            changesMade = true;
            if (listOfGesturesToShow.SelectedItem != null)
            {
                Gesture gesture;
                Gesture gestureToEdit = listOfGesturesToShow.SelectedItem as Gesture;
                GestureAdderAndEditor wind = new GestureAdderAndEditor(gestureToEdit.type, gestureToEdit.command, gestureToEdit.code);
                if (wind.ShowDialog() == true)
                {
                    listOfGestures.Remove(listOfGesturesToShow.SelectedItem as Gesture);
                    gesture = wind.createdGesture;
                    foreach (Gesture tmpG in listOfGestures)
                    {
                        if (tmpG == null)
                        {
                            return;
                        }
                        if (tmpG.code.Contains(gesture.code) || gesture.code.Contains(tmpG.code))
                        {
                            MessageBox.Show($"Gesture with desired code or part of it already exists changes were not saved\n {gesture.code} and {tmpG.code}", "Can't change gesture");
                            listOfGestures.Add(gestureToEdit);
                            return;
                        }
                    }
                    listOfGestures.Add(gesture);
                }
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            changesMade = true;
            Gesture gesture;
            GestureAdderAndEditor wind = new GestureAdderAndEditor();
            if (wind.ShowDialog() == true)
            {
                gesture = wind.createdGesture;
                foreach (Gesture tmpG in listOfGestures)
                {
                    if(tmpG == null)
                    {
                        return;
                    }
                    if (tmpG.code.Contains(gesture.code) || gesture.code.Contains(tmpG.code))
                    {
                        MessageBox.Show($"Gesture with desired code or part of it already exists\n {gesture.code} and {tmpG.code}", "Can't add gesture");
                        return;
                    }
                }
                listOfGestures.Add(gesture);
            }
        }

        private void AcceptChangesButton(object sender, RoutedEventArgs e)
        {
            XmlDocument settings = new XmlDocument();
            XmlNode root = settings.CreateElement("gestures");
            settings.AppendChild(root);
            foreach (var gestureParameters in listOfGestures)
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
            SaveChanges?.Invoke(this, EventArgs.Empty);
            loadSettings();
            MessageBox.Show("Changes Saved");
            changesMade = false;
            this.DialogResult = true;
        }
    }
}
