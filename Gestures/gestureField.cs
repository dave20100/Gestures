﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using WindowsInput;
using WindowsInput.Native;

namespace Gestures
{
    class gestureField
    {
        string gestureCodeBufor = "";
        Dictionary<string, Action> recordedGestures;
        Canvas paintField;
        List<List<gesturePoint>> pointField;

        public gestureField(Canvas gestureCanv)
        {
            recordedGestures = new Dictionary<string, Action>();
            pointField = new List<List<gesturePoint>>();
            this.paintField = gestureCanv;
            fillField(3);
            recordedGestures.Add("[0,0][0,1][1,1][0,1]", () => {
                try
                {
                    System.Diagnostics.Process.Start(@"C:\Users\Dawid\AppData\Local\atom\atom.exe");
                }
                catch
                {
                    MessageBox.Show("The file does not exist", "Error");
                }
            });
            recordedGestures.Add("[2,2][2,1]", () =>
            {
                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.TextEntry("Hello World");
            });
            recordedGestures.Add("[0,0][1,1][2,2]", () =>
            {
                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
            });
        }
        private void fillField(int size)
        {
            this.pointField.Clear();
            for(int i = 0; i < size; i++)
            {
                List<gesturePoint> tmp = new List<gesturePoint>();
                for (int j = 0; j < size; j++)
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
            if(gestureCodeBufor.Length >= pointField.Count * pointField[0].Count * 5)
            {
                gestureCodeBufor = gestureCodeBufor.Remove(0, 5);
            }
            gestureCodeBufor += ((gesturePoint)sender).id;
            foreach (var gesture in recordedGestures)
            {
                if (gestureCodeBufor.Contains(gesture.Key))
                {
                    gestureCodeBufor = "";
                    gesture.Value.Invoke();
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

        public void changeSize(int size)
        {
            fillField(size);
        }
    }
}