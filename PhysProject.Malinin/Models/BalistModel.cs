using System;
using System.Windows.Controls;
using PhysProject.Core;

namespace PhysProject.Malinin.Models
{
	public class BalistModel : PhysicalBaseObject
	{
		private TwoDimesional _stPos;
		private TextBlock _view;
		private double _Vprev = 0, _At, _An, a1;
		private bool flag = true;
		int i;

		public BalistModel(PhysicalField field, double size, TwoDimesional position, TwoDimesional speedVector, double a, int i1) : base(field, size, position, speedVector)
        {
			_stPos = position;
			a1 = a;
			i = i1;
		}
		public void SetCoord(TextBlock Coord)
		{
			_view = Coord;
		}

		protected override void CustomConduct()
		{
			TwoDimesional sv = SpeedVector;

			if (_view != null)
			{
				double _V, _X1, _Y1;
				_X1 = SpeedVector.X;
				_Y1 = SpeedVector.Y;

				if (_Vprev == 0)
				{
					_Vprev = Math.Sqrt(SpeedVector.X * SpeedVector.X + SpeedVector.Y * SpeedVector.Y);
					_At = 9.81 * Math.Sin(a1);
					_An = Math.Sqrt(9.81 * 9.81 - _At * _At);
					if (_An < 0.000000000001)
					{
						_An = 0;
					}
					_V = _Vprev;
				}
				else
				{
					// _Vprev = _V;
					_V = Math.Sqrt(_X1 * _X1 + _Y1 * _Y1);
					_An = _X1 / _V * 9.81;
					if (_An < 0.000000000001)
					{
						_An = 0;
					}
					_At = Math.Sqrt(9.81 * 9.81 - _An * _An);

				}
				if (flag == true)
				{
					_view.Text = $"X:{Math.Round((Position.X - 100), 2)} м\nY:{Math.Round((Position.Y), 2)} м\n" +
							   $"A(tan):{Math.Round(_At, 2)} м/с2\n" +
							   $"A(norm):{Math.Round(_An, 2)} м/с2\n" +
							   $"V:{Math.Round(_V, 2)} м/с\n";
					// $"i:{i}";
					// $"A:({AccelerationDirection.X};{AccelerationDirection.Y}";
				}
			}

			AccelerationDirection = new TwoDimesional(0, -9.81);


			//TODO Unavaliable now
			//if ((Position.Y >= Size / 2) && (flag == true))
			//{
			//	if (GraphicList.Count >= 1)
			//	{
			//		GraphicList[0].AddPoint(0, Field.CurrentTime - TimeAdded, _At);
			//		GraphicList[1].AddPoint(0, Field.CurrentTime - TimeAdded, _An);
			//	}
			//}
			if (Position.Y < Size / 2)
			{
				flag = false;
				StopMoving();
			}
		}
	}
}