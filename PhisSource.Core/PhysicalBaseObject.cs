using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PhisSource.Core
{
    public abstract class PhysicalBaseObject
    {
        public List<TwoDimensional> PositionList { get; } = new List<TwoDimensional>();
        public TwoDimensional AccelerationDirection { get; set; }
        public TwoDimensional SpeedVector { get; set; }

        public readonly Ellipse MaterialObject;

        public double ObjectSize { get; }
        public bool IsStopped = false;
        public bool IsWayDraw = false;
        public int SelfTime = 0;

        private TwoDimensional _newSpeedVector;

        public void StopMoving()
        {
            SpeedVector = new TwoDimensional(0, 0);
            AccelerationDirection = new TwoDimensional(0, 0);
            IsStopped = true;
        }

        protected PhysicalBaseObject(TwoDimensional position, TwoDimensional speedVector, double size)
        {
            PositionList.Add(position);
            SpeedVector = speedVector;
            ObjectSize = size;
            MaterialObject = Tools.Generator.GenerateEllipse(ObjectSize);
        }

        public void UpdateMoveDirection(int timePassed)
        {
            if (IsStopped)
            {
                return;
            }

            SelfTime += timePassed;
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

            UpdatePosition();
        }

        public void UpdatePosition()
        {
            var lastPosition = PositionList.Last();
            Canvas.SetLeft(MaterialObject, lastPosition.X - MaterialObject.Width / 2);
            Canvas.SetTop(MaterialObject, lastPosition.Y - MaterialObject.Height / 2);
            //TODO:
            //CustomPositionUpdate();
        }

        protected abstract void CustomConduct();
    }
}