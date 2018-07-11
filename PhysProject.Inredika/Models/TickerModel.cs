using System;
using System.Windows.Media;
using System.Windows.Shapes;
using PhysicsSource.Core.Models;
using PhysicsSource.Core.Tools;

namespace PhysProject.Inredika.Models
{
    public class TickerModel : PhysicalBaseObject
    {
        //TODO: refactoring
        private readonly TwoDimensional _startPosition;
        private readonly double _a;
        private readonly ExecuteField _field;
        private readonly double _length;

        private readonly Line _line;

        public TickerModel(ExecuteField field, TwoDimensional position, double length, double angle) : base(position,
            new TwoDimensional(0, 0), 15)
        {
            _field = field;
            _length = length;
            _startPosition = position;
            var angleWithPi = angle / 180 * Math.PI;
            _a = _length * Math.Sin(angleWithPi);

            var dX = _a * Math.Cos(0);
            var dY = Math.Sqrt(_length * _length - Math.Pow(dX, 2));

            var objectPosition = new TwoDimensional(dX, dY);
            CurrentPosition = objectPosition + CurrentPosition;
            //PrevPosition = CurrentPosition;

            //TODO: ??
            _line = Generator.GenerateLine(_startPosition, CurrentPosition);
            _line.Stroke = new SolidColorBrush {Color = Colors.Magenta};
            _field.FieldCanvas.Children.Add(_line);
        }

        public void UpdateData()
        {
            //if (InfoBlock != null)
            //{
            //	InfoBlock.Text = $"X:{Position.X}\nY:{Position.Y}\n" +
            //				  $"V:({SpeedVector.X};{SpeedVector.Y})\n" +
            //				  $"A:({AccelerationDirection.X};{AccelerationDirection.Y}";
            //}
            //SeriesList[0]?.AddPoint(SelfTime, Position.X - 150);
            //SeriesList[1]?.AddPoint(SelfTime, (Position.X - PrevPosition.X) / 40 * 1000);
        }

        //protected override void CustomPositionUpdate()
        //{
        //	if (_line != null)
        //	{
        //		_line.X2 = Position.X;
        //		_line.Y2 = Position.Y;
        //	}
        //}

        public void DeleteTicker()
        {
            _field.FieldCanvas.Children.Remove(MaterialObject);
            _field.FieldCanvas.Children.Remove(_line);
            _field.PhysicalObjects.Remove(this);
            //foreach (CustomSeries series in SeriesList)
            //{
            //	series.DeleteChart();
            //}
            //SeriesList = new List<CustomSeries>();
        }

        protected override void CustomConduct()
        {
            //double angleCos = (Position.X - _startPosition.X) / _length;
            //double angleSin = Math.Sqrt(1 - angleCos * angleCos);
            //AccelerationDirection = new TwoDimesional( -981 * angleCos, -981 * (angleSin - 1));
            //double cv = Math.Pow(SpeedVector.X, 2) + Math.Pow(SpeedVector.Y, 2);
            //cv /= _length;
            //AccelerationDirection.X += cv / _length * (-Position.X + _startPosition.X);
            //AccelerationDirection.Y += cv / _length * (-Position.Y + _startPosition.Y);

            var newA = TotalExecutingTime * Math.Sqrt(9.81 / _length * 100) / 1000;
            var dX = _a * Math.Cos(newA);
            dX *= Math.Pow(Math.E, TotalExecutingTime * -0.0001);
            CurrentPosition.X = _startPosition.X + dX;
            CurrentPosition.Y = Math.Sqrt(_length * _length - Math.Pow(dX, 2));

            UpdateData();
        }
    }
}