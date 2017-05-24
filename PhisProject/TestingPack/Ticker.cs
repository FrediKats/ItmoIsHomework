using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using PhysProject.Source;

namespace PhysProject.TestingPack
{
    public class Ticker : PhysicalBaseObject
    {
        private TextBlock _debug;
        private TwoDimesional _startPosition;
        private Line _line;
        private double L = 40;

        public Ticker(PhysicalField field, double size, TwoDimesional position, TwoDimesional speedVector) : base(field, size, position, speedVector)
        {
            _startPosition = position;
            double newX = L * Math.Sin(Math.PI / 6) * -1;   
            double newY = L * Math.Cos(Math.PI / 6) * -1;
            TwoDimesional objectPosition = new TwoDimesional(newX, newY);
            Position = objectPosition + Position;
            _line = Tool.GenerateLine(_startPosition, Position);
            Field.FieldCanvas.Children.Add(_line);
        }

        public void SetDebug(TextBlock debugText)
        {
            _debug = debugText;
        }

        protected override void CustomConduct()
        {
            double angleCos = (Position.X - _startPosition.X)/ L;
            double angleSin = Math.Sqrt(1 - angleCos * angleCos);
            AccelerationDirection = new TwoDimesional(-9.81 * angleCos, 9.81 * (angleSin - 1) );

            if (_debug != null)
            {
                 _debug.Text = $"X:{Position.X}\nY:{Position.Y}\n" +
                               $"angle:{angleCos}\n" +
                               $"V:({SpeedVector.X};{SpeedVector.Y})\n" +
                               $"A:({AccelerationDirection.X};{AccelerationDirection.Y}";
            }

            _line.X2 = Position.X;
           _line.Y2 = Config.WindowHeight - Position.Y;
            if (GraphicList.Count >= 1)
            {
                GraphicList[0].AddPoint(0, Position.X - _startPosition.X);
            }
        }
    }
}