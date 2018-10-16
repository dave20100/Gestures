using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
namespace Gestures
{
    class gesturePoint  : UIElement
    {
        public event EventHandler IsLookedAt;
        SolidColorBrush hovered = new SolidColorBrush(Colors.Red);
        SolidColorBrush idle = new SolidColorBrush(Colors.Black);
        //Ellipse center;
        Ellipse outer;
        int size;
        public string id;
        
        public gesturePoint(int size, string id)
        {
            this.size = size;
            this.id = id;

            //center = new Ellipse()
            //{
            //    Width = size / 10,
            //    Height = 10,
            //    Fill = idle,
            //};
            outer = new Ellipse()
            {
                Width = size,
                Height = size,
                Fill = idle,
                Opacity = 0.5
            };
            //center.IsMouseDirectlyOverChanged += MouseOverChanged;
            outer.IsMouseDirectlyOverChanged += MouseOverChanged;
        }

        

        private void MouseOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if((bool)e.NewValue == true)
            {
                //center.Fill = hovered;
                //outer.Opacity = 0;
                outer.Fill = hovered;
                IsLookedAt?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                outer.Fill = idle;
                //outer.Opacity = 0.3;
                //center.Fill = idle;
            }
        }

        public void Draw(double x, double y, Canvas toDrawOn)
        {
            //Canvas.SetLeft(center , x - (10/2));
            //Canvas.SetTop(center, y - (10/2));
            //toDrawOn.Children.Add(center);
            Canvas.SetLeft(outer, x - this.size/2);
            Canvas.SetTop(outer, y - this.size/2);
            toDrawOn.Children.Add(outer);
        }
        
    }
}
