using System.Windows.Media;
using System.Windows.Shapes;
using PhisSource.Core.Models;

namespace PhisSource.Core.Tools
{
    public static class Generator
    {
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

        public static Line GenerateLine(double x1, double y1, double x2, double y2)
        {
            return new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = 2,
                Stroke = Brushes.Navy
            };
        }

        public static Line GenerateLine(TwoDimensional f, TwoDimensional s)
        {
            return GenerateLine(f.X, f.Y, s.X, s.Y);
        }
    }
}