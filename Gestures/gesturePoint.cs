using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using WindowsInput;
using System.Threading;
using System.Timers;
using Tobii.Interaction.Framework;
using Tobii.Interaction.Wpf;

namespace Gestures
{
    class gesturePoint  : UIElement
    {
        public event EventHandler IsLookedAt;
        SolidColorBrush hovered = new SolidColorBrush(Colors.Red);
        SolidColorBrush idle = new SolidColorBrush(Colors.Black);
        Ellipse outer;
        int size;
        public string id;
        
        public gesturePoint(int size, string id)
        {
            this.size = size;
            this.id = id;
            
            outer = new Ellipse()
            {
                Width = size,
                Height = size,
                Fill = idle,
                Opacity = 0.5
            };
            outer.IsMouseDirectlyOverChanged += MouseOverChanged;
            outer.MouseDown += Outer_MouseDown;
            outer.SetIsGazeAware(true);
            outer.AddHasGazeChangedHandler(Button_HasGazeChanged);
        }

        private void Outer_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            outer.Visibility = Visibility.Hidden;
            System.Timers.Timer hidingTimer = new System.Timers.Timer();
            hidingTimer.Interval = 1000;
            hidingTimer.AutoReset = false;
            hidingTimer.Elapsed += HidingTimerElapsed;
            hidingTimer.Start();
        }

        private void HidingTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => outer.Visibility = Visibility.Visible);
        }

        private void MouseOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        public void Draw(double x, double y, Canvas toDrawOn)
        {
            Canvas.SetLeft(outer, x - this.size/2);
            Canvas.SetTop(outer, y - this.size/2);
            toDrawOn.Children.Add(outer);
        }


        private void Button_HasGazeChanged(object sender, HasGazeChangedRoutedEventArgs e)
        {
            if ((bool)e.HasGaze == true)
            {
                outer.Fill = hovered;
                IsLookedAt?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                outer.Fill = idle;
            }
        }
    }
}
