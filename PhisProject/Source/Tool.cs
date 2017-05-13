using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhysProject.Source
{
    public class Tool
    {
        public static double Distance(TwoDimesional f, TwoDimesional s)
        {
            return  Math.Sqrt(Math.Pow(f.X - s.X, 2) + Math.Pow(f.Y - s.Y, 2));
        }

        public static Ellipse GenerateEllipse(double size)
        {
            return new Ellipse()
            {
                Height = size,
                Width = size,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush() { Color = Colors.Navy },
                Fill = new SolidColorBrush() { Color = Colors.Yellow }
            };
        }
    }
}