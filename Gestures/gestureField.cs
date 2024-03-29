﻿using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Gestures
{
    class GestureField
    {
        /// <summary>
        /// Timer resetujacy kod gestu po ustalonym czasie
        /// </summary>
        Timer gestureTime = new Timer()
        {
            Interval = 2000
        };
        string gestureCodeBufor = "";
        List<Gesture> recordedGestures;
        Canvas paintField;
        List<List<GesturePoint>> pointField;

        public GestureField(Canvas gestureCanv)
        {
            gestureTime.Elapsed += GestureTime_Elapsed;
            recordedGestures = new List<Gesture>();
            pointField = new List<List<GesturePoint>>();
            this.paintField = gestureCanv;
            CreateField(3);
        }

        private void GestureTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            gestureCodeBufor = "";
        }

        private void CreateField(int amountOfDots)
        {
            this.pointField.Clear();
            for(int i = 0; i < amountOfDots; i++)
            {
                List<GesturePoint> tmp = new List<GesturePoint>();
                for (int j = 0; j < amountOfDots; j++)
                {
                    GesturePoint pointToAdd = new GesturePoint(70, "[" + i + "," + j + "]");
                    pointToAdd.IsLookedAt += PointToAdd_IsLookedAt;
                    pointToAdd.IsLookedAt += PointToAdd_LookedAtResetTimer;
                    tmp.Add(pointToAdd);
                }
                this.pointField.Add(tmp);
            }
            drawField();
        }

        private void PointToAdd_LookedAtResetTimer(object sender, EventArgs e)
        {
            gestureTime.Stop();
            gestureTime.Start();
        }

        private void PointToAdd_IsLookedAt(object sender, EventArgs e)
        {
            if(gestureCodeBufor.Length >= 100)
            {
                gestureCodeBufor = gestureCodeBufor.Remove(0, gestureCodeBufor.IndexOf(']')+1);
            }
            if (!gestureCodeBufor.EndsWith(((GesturePoint)sender).id)){
                gestureCodeBufor += ((GesturePoint)sender).id;
            }
            foreach (var record in recordedGestures)
            {
                if (gestureCodeBufor.Contains(record.code))
                {
                    gestureCodeBufor = "";
                    record.invoke();
                }
            }
        }

        private void drawField()
        {
            this.paintField.Children.Clear();
            for (int i = 0; i < pointField.Count; i++)
            {
                for (int j = 0; j < pointField[0].Count; j++)
                {
                    double distX = paintField.ActualWidth / pointField.Count;
                    double distY = paintField.ActualHeight / pointField[0].Count;
                    pointField[i][j].Draw(distX / 2 + distX * i, distY / 2 + distY * j , this.paintField);
                }
            }
        }

        public void changeAmountOfDots(int amountOfDots)
        {
            CreateField(amountOfDots);
        }

        public void addGesture(string gesture, int type, string command)
        {
            foreach(Gesture tmpG in recordedGestures)
            {
                if(tmpG == null)
                {
                    return;
                }
                if (tmpG.code.Contains(gesture) || gesture.Contains(tmpG.code))
                {
                    MessageBox.Show($"Gesture with desired code or part of it already exists\n {gesture} and {tmpG.code}", "Can't add gesture");
                    return;
                }
            }
            Gesture gest = new Gesture(gesture, type, command);
            recordedGestures.Add(gest);
        }

        /// <summary>
        /// Load gestures from a settings file
        /// </summary>
        /// <param name="settingsFileName"></param>
        public void loadSettings(string settingsFileName = "Settings.xml")
        {
            recordedGestures.Clear();
            XmlDocument settings = new XmlDocument();
            try
            {
                settings.Load(settingsFileName);
                foreach (XmlNode node in settings.DocumentElement)
                {
                        addGesture(node.Attributes["gestureCode"].Value, Int32.Parse(node.Attributes["gestureType"].Value), node.Attributes["gestureCommand"].Value);
                }
            }
            catch
            {
                MessageBox.Show("There was something wrong with file default settings restored");
                MainWindow.generateDefaultSettings();
                loadSettings();
            }
        }
    }
}
