using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhisProject.PhisAbstract
{
    public class Ball
    {
        public readonly Ellipse Ellipse;
        public readonly Line Line;
        private double _sizeX, _sizeY;
        private double _centerX, _centerY;

        public Ball(double sizeX, double sizeY, double centerX, double centerY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _centerX = centerX;
            _centerY = centerY;
            Line = new Line()
            {
                Stroke = System.Windows.Media.Brushes.LightSteelBlue,
                StrokeThickness = 2,
                X1 = centerX,
                Y1 = centerY
            };
            Ellipse = new Ellipse()
            {
                Height = 20,
                Width = 20,
                StrokeThickness = 2,
                Stroke = new SolidColorBrush() { Color = Colors.Black },
                Fill = new SolidColorBrush() { Color = Colors.Red }
            };
        }

        public void UpdateBallPosition(double asoluteX, double asoluteY)
        {
            Canvas.SetLeft(Ellipse, asoluteX - Ellipse.Width / 2);
            Canvas.SetTop(Ellipse, asoluteY - Ellipse.Height / 2);
        }

        public void UpdateLinePosition(double positionX, double positionY)
        {
            Line.X2 = positionX;
            Line.Y2 = positionY;
        }

        public double CenterX()
        {
            return _centerX;
        }

        public double CenterY()
        {
            return _centerY;
        }
    }
}