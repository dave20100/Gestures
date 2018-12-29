using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Timers;
using Tobii.Interaction.Wpf;

namespace Gestures
{
    class GesturePoint  : UIElement
    {
        public event EventHandler IsLookedAt;
        SolidColorBrush hovered = new SolidColorBrush(Colors.Red);
        SolidColorBrush idle = new SolidColorBrush(Colors.Black);
        Ellipse outer;
        int size;
        public string id;
        
        public GesturePoint(int size, string id)
        {
            this.size = size;
            this.id = id;
            
            outer = new Ellipse()
            {
                Width = size,
                Height = size,
                Fill = idle,
                Opacity = 0.3
            };
            outer.IsMouseDirectlyOverChanged += MouseOverChanged;
            outer.SetIsGazeAware(true);
            outer.AddHasGazeChangedHandler(Button_HasGazeChanged);
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
