using System;
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
            //TODO make loading gestures from some kind of file with size decided by user
            //loadGesturesFromFile();
            addGesture("[1,0][2,0]", 1, "Hello");
            addGesture("[2,2][2,1]", 2, @"C:\Program Files (x86)\Google\Chrome\Application\Chrome.exe");
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

        public void changeAmountOfDots(int amountOfDots)
        {
            fillField(amountOfDots);
        }

        public void addGesture(string gesture, int type, string command)
        {
            InputSimulator simulator = new InputSimulator();
            switch (type)
            {
                case 1:
                    recordedGestures.Add(gesture, () =>
                    {
                        simulator.Keyboard.TextEntry(command);
                    });
                    break;
                case 2:
                    recordedGestures.Add(gesture, () =>
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(command);
                        }
                        catch
                        {
                            MessageBox.Show("The file does not exist", "Error");
                        }
                    });
                    break;
                case 3:
                    simulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
                    break;
            }
        }
    }
}
