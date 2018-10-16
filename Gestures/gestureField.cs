using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        Timer gestureTime = new Timer()
        {
            Interval = 2000
        };
        string gestureCodeBufor = "";
        List<Gesture> recordedGestures;
        Canvas paintField;
        List<List<gesturePoint>> pointField;

        public gestureField(Canvas gestureCanv)
        {
            gestureTime.Elapsed += GestureTime_Elapsed;
            recordedGestures = new List<Gesture>();
            pointField = new List<List<gesturePoint>>();
            this.paintField = gestureCanv;
            fillField(3);
        }

        private void GestureTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            gestureCodeBufor = "";
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
            if (!gestureCodeBufor.EndsWith(((gesturePoint)sender).id)){
                gestureCodeBufor += ((gesturePoint)sender).id;
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
            fillField(amountOfDots);
        }

        public void addGesture(string gesture, int type, string command)
        {
            foreach(Gesture tmpG in recordedGestures)
            {
                if (tmpG.code.Contains(gesture) || gesture.Contains(tmpG.code))
                {
                    MessageBox.Show($"Gesture with desired code or part of it already exists\n {gesture} and {tmpG.code}", "Can't add gesture");
                    return;
                }
            }
            Gesture gest = new Gesture(gesture, type, command);
            recordedGestures.Add(gest);
        }

        public void loadSettings(string settingsFileName = "Settings.xml")
        {
            XmlDocument settings = new XmlDocument();
            settings.Load(settingsFileName);
            int countBad = 0;
            foreach (XmlNode node in settings.DocumentElement)
            {
                try
                {
                    addGesture(node.Attributes["gestureCode"].Value, Int32.Parse(node.Attributes["gestureType"].Value), node.Attributes["gestureCommand"].Value);
                }
                catch
                {
                    countBad++;
                }
            }
            MessageBox.Show(countBad.ToString(), "Amount of bad gestures");
            
        }
    }
}
