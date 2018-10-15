﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using WindowsInput;
using WindowsInput.Native;

namespace Gestures
{
    class gestureField
    {
        string gestureCodeBufor = "";
        List<Gesture> recordedGestures;
        Canvas paintField;
        List<List<gesturePoint>> pointField;

        public gestureField(Canvas gestureCanv)
        {
            recordedGestures = new List<Gesture>();
            pointField = new List<List<gesturePoint>>();
            this.paintField = gestureCanv;
            fillField(3);
            //TODO make loading gestures from some kind of file with size decided by user
            //loadGesturesFromFile();
            addGesture("[1,1][2,2]", 1, "Hello");
            addGesture("[2,2][2,1]", 2, @"C:\Program Files (x86)\Google\Chrome\Application\Chrome.exe");
            addGesture("[0,0][1,0][2,0]", 3, "Q");
        }

        private void fillField(int amountOfDots)
        {
            this.pointField.Clear();
            for(int i = 0; i < amountOfDots; i++)
            {
                List<gesturePoint> tmp = new List<gesturePoint>();
                for (int j = 0; j < amountOfDots; j++)
                {
                    gesturePoint pointToAdd = new gesturePoint(50, "[" + i + "," + j + "]");
                    pointToAdd.IsLookedAt += PointToAdd_IsLookedAt;
                    tmp.Add(pointToAdd);
                }
                this.pointField.Add(tmp);
            }
            drawField();
        }

        private void PointToAdd_IsLookedAt(object sender, EventArgs e)
        {
            if(gestureCodeBufor.Length >= 100)
            {
                gestureCodeBufor = gestureCodeBufor.Remove(0, gestureCodeBufor.IndexOf(']')+1);
            }
            gestureCodeBufor += ((gesturePoint)sender).id;
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
            fillField(amountOfDots);
        }

        public void addGesture(string gesture, int type, string command)
        {
            Gesture gest = new Gesture(gesture, type, command);
            recordedGestures.Add(gest);
        }

        public void loadSetting(string settingsFileName = "Settings.xml")
        {
            XmlDocument settings = new XmlDocument();
            settings.Load(settingsFileName);
            foreach (XmlNode node in settings.DocumentElement)
            {
                addGesture(node.Attributes["gestureCode"].Value, Int32.Parse( node.Attributes["gestureType"].Value), node.Attributes["gestureCommand"].Value);
            }   
        }
    }
}
