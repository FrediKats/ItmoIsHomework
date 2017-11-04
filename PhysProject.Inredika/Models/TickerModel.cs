using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using PhysProject.Core;

namespace PhysProject.Inredika.Models
{
	public class TickerModel : PhysicalBaseObject
	{
		private readonly TwoDimesional _startPosition;

		private Line _line;
		private double _length, _angle;
		private double _a;

		public TickerModel(PhysicalField field, TwoDimesional position, double length, double angle) : base(field, 15, position, new TwoDimesional(0, 0))
        {
			_length = length;
			_startPosition = position;
			_angle = angle / 180 * Math.PI;
			_a = _length * Math.Sin(_angle);

			double dX = _a * Math.Cos(0);
			double dY = Math.Sqrt(_length * _length - Math.Pow(dX, 2));

			TwoDimesional objectPosition = new TwoDimesional(dX, dY);
			Position = objectPosition + Position;
			PrevPosition = Position;

			_line = Tool.GenerateLine(_startPosition, Position);
			_line.Stroke = new SolidColorBrush() { Color = Colors.Magenta };
			Field.FieldCanvas.Children.Add(_line);

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

			double newA = SelfTime * Math.Sqrt(9.81 / _length * 100) / 1000;
			double dX = _a * Math.Cos(newA);
			dX *= Math.Pow(Math.E, SelfTime * -0.0001);
			Position.X = _startPosition.X + dX;
			Position.Y = Math.Sqrt(_length * _length - Math.Pow(dX, 2));

			UpdateData();
		}

		public void UpdateData()
		{
			if (InfoBlock != null)
			{
				InfoBlock.Text = $"X:{Position.X}\nY:{Position.Y}\n" +
							  $"V:({SpeedVector.X};{SpeedVector.Y})\n" +
							  $"A:({AccelerationDirection.X};{AccelerationDirection.Y}";
			}
			SeriesList[0]?.AddPoint(SelfTime, Position.X - 150);
			SeriesList[1]?.AddPoint(SelfTime, (Position.X - PrevPosition.X) / 40 * 1000);
		}

		protected override void CustomPositionUpdate()
		{
			if (_line != null)
			{
				_line.X2 = Position.X;
				_line.Y2 = Position.Y;
			}
		}

		public void DeleteTicker()
		{
			Field.FieldCanvas.Children.Remove(MaterialObject);
			Field.FieldCanvas.Children.Remove(_line);
			Field.PhysicalObjects.Remove(this);
			foreach (CustomSeries series in SeriesList)
			{
				series.DeleteChart();
			}
			SeriesList = new List<CustomSeries>();
		}
	}
}