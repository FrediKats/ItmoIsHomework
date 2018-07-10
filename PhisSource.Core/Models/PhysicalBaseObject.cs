using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using PhisSource.Core.Tools;

namespace PhisSource.Core.Models
{
    public abstract class PhysicalBaseObject
    {
        public event EventHandler<PositionChangedEventArgs> OnPositionChanged;
        public event EventHandler<PositionChangedEventArgs> OnSpeedChanged; 
        public event EventHandler<PositionChangedEventArgs> OnAccelerationChanged;

        public List<TwoDimensional> PositionList { get; } = new List<TwoDimensional>();

        public TwoDimensional CurrentPosition
        {
            get => _position;
            set
            {
                var last = PositionList.LastOrDefault();
                _position = value;
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs(last, value));
            }
        }

        public TwoDimensional AccelerationDirection
        {
            get => _accelerationDirection;
            set
            {
                var last = _accelerationDirection;
                _accelerationDirection = value;
                OnAccelerationChanged?.Invoke(this, new PositionChangedEventArgs(last, value));
            }
        }

        public TwoDimensional SpeedVector
        {
            get => _speedVector;
            set
            {
                var last = _speedVector;
                _speedVector = value;
                OnSpeedChanged?.Invoke(this, new PositionChangedEventArgs(last, value));
            }
        }

        public readonly Ellipse MaterialObject;

        public double ObjectSize { get; }
        public int TotalExecutingTime { get; private set; }
        public bool IsStopped = false;

        private TwoDimensional _newSpeedVector;
        private TwoDimensional _speedVector;
        private TwoDimensional _accelerationDirection;
        private TwoDimensional _position;

        public void StopMoving()
        {
            IsStopped = true;
        }

        protected PhysicalBaseObject(TwoDimensional position, TwoDimensional speedVector, double size)
        {
            ObjectSize = size;
            MaterialObject = Tools.Generator.GenerateEllipse(ObjectSize);
            SpeedVector = speedVector;

            CurrentPosition = position;
            OnPositionChanged?.Invoke(this, new PositionChangedEventArgs(null, PositionList.Last()));
        }

        public void UpdateMoveDirection(int timePassed)
        {
            if (IsStopped)
            {
                return;
            }

            TotalExecutingTime += timePassed;
            _newSpeedVector = null;
            CustomConduct();

            if (_newSpeedVector != null)
            {
                SpeedVector = _newSpeedVector;
            }
            if (AccelerationDirection != null)
            {
                SpeedVector += (AccelerationDirection * timePassed / 1000);
            }


            //TODO: /1000 ??
            CurrentPosition = CurrentPosition + (SpeedVector * timePassed / 1000);

            UpdatePosition();
        }

        public void UpdatePosition()
        {
            Canvas.SetLeft(MaterialObject, CurrentPosition.X - MaterialObject.Width / 2);
            Canvas.SetBottom(MaterialObject, CurrentPosition.Y - MaterialObject.Height / 2);
            //TODO:
            //CustomPositionUpdate();
        }

        protected abstract void CustomConduct();
    }
}