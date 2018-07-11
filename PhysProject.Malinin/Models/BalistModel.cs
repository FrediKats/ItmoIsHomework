﻿using System;
using System.Windows.Controls;
using PhysicsSource.Core.Models;

namespace PhysProject.Malinin.Models
{
    public class BalistModel : PhysicalBaseObject
    {
        //TODO: refactoring
        private readonly double a1;
        private TwoDimensional _stPos;
        private TextBlock _view;
        private double _Vprev, _At, _An;
        private bool flag = true;
        private int i;

        public BalistModel(double size, TwoDimensional position, TwoDimensional speedVector,
            double a, int i1) : base(position, speedVector, size)
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
            var sv = SpeedVector;
            var position = CurrentPosition;

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
                    if (_An < 0.000000000001) _An = 0;
                    _V = _Vprev;
                }
                else
                {
                    // _Vprev = _V;
                    _V = Math.Sqrt(_X1 * _X1 + _Y1 * _Y1);
                    _An = _X1 / _V * 9.81;
                    if (_An < 0.000000000001) _An = 0;
                    _At = Math.Sqrt(9.81 * 9.81 - _An * _An);
                }

                if (flag)
                    _view.Text = $"X:{Math.Round(position.X - 100, 2)} м\nY:{Math.Round(position.Y, 2)} м\n" +
                                 $"A(tan):{Math.Round(_At, 2)} м/с2\n" +
                                 $"A(norm):{Math.Round(_An, 2)} м/с2\n" +
                                 $"V:{Math.Round(_V, 2)} м/с\n";
            }

            AccelerationDirection = new TwoDimensional(0, -9.81);

            if (position.Y < ObjectSize / 2)
            {
                flag = false;
                StopMoving();
            }
        }
    }
}