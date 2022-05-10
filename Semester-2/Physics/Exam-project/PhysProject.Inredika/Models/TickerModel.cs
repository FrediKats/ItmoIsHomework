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

            //TODO: ??
            _line = Generator.GenerateLine(_startPosition, CurrentPosition);
            _line.Stroke = new SolidColorBrush {Color = Colors.Magenta};
            _field.FieldCanvas.Children.Add(_line);
        }

        public void DeleteTicker()
        {
            _field.FieldCanvas.Children.Remove(MaterialObject);
            _field.FieldCanvas.Children.Remove(_line);
            _field.PhysicalObjects.Remove(this);
        }

        protected override void CustomConduct()
        {
            var newA = TotalExecutingTime * Math.Sqrt(9.81 / _length * 100) / 1000;
            var dX = _a * Math.Cos(newA);
            dX *= Math.Pow(Math.E, TotalExecutingTime * -0.0001);
            CurrentPosition.X = _startPosition.X + dX;
            CurrentPosition.Y = _startPosition.Y - Math.Sqrt(_length * _length - Math.Pow(dX, 2));
        }
    }
}