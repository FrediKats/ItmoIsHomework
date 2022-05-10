using System;
using System.Windows.Controls;
using PhysicsSource.Core.Models;
using PhysicsSource.Core.Tools;

namespace PhysProject.Malinin.Models
{
    public class BalistModel : PhysicalBaseObject
    {
        private readonly double _angelDirection;
        private bool _isActive = true;
        private bool _isFirst = true;
        private TextBlock _textBlock;

        public BalistModel(double size, TwoDimensional position, TwoDimensional speedVector,
            double angelDirection) : base(position, speedVector, size)
        {
            _angelDirection = angelDirection;
        }

        public void AssignOutputBox(TextBlock outputTextBlock)
        {
            _textBlock = outputTextBlock;
        }

        protected override void CustomConduct()
        {
            if (_isActive == false) return;
            if (CurrentPosition.Y < ObjectSize / 2)
            {
                _isActive = false;
                StopMoving();
            }

            AccelerationDirection = new TwoDimensional(0, -9.81);

            if (_textBlock != null)
            {
                double accelerationX;
                double accelerationY;
                if (_isFirst)
                {
                    _isFirst = false;
                    accelerationY = 9.81 * Math.Sin(_angelDirection);
                    accelerationX = Math.Sqrt(9.81 * 9.81 - accelerationY * accelerationY);
                }
                else
                {
                    accelerationX = SpeedVector.X / SpeedVector.VectorLength() * 9.81;
                    accelerationY = Math.Sqrt(9.81 * 9.81 - accelerationX * accelerationX);
                }
                

                _textBlock.Text = $"X:{Math.Round(CurrentPosition.X - 100, 2)} м\n"
                                  + $"Y:{Math.Round(CurrentPosition.Y, 2)} м\n"
                                  + $"A(tan):{Math.Round(accelerationY, 2)} м/с2\n"
                                  + $"A(norm):{Math.Round(accelerationX, 2)} м/с2\n"
                                  + $"V:{Math.Round(SpeedVector.VectorLength(), 2)} м/с\n";
            }
        }
    }
}