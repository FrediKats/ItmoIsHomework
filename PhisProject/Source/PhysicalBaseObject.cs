using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace PhysProject.Source
{
    public abstract class PhysicalBaseObject
    {
        #region Public
        public readonly PhysicalField Field;
        public readonly Ellipse MaterialObject;
        public TwoDimesional Position;
        public TwoDimesional PrevPosition;
        public readonly List<Graphic> GraphicList = new List<Graphic>();
        public int TimeAdded = -1;

        public bool IsWayDraw = false;

        public void StopMoving()
        {
            _speedVector = new TwoDimesional(0, 0);
            _accelerationVector = new TwoDimesional(0, 0);

            Field.StaticObjects.Add(this);
            Field.PhysicalObjects.Remove(this);
        }

        public void UpdateMoveDirection(int timePassed)
        {
            _newSpeedVector = null;

            CustomConduct();

            if (_newSpeedVector != null)
            {
                _speedVector = _newSpeedVector;
            }
            if (_accelerationVector != null)
            {
                _speedVector += (_accelerationVector * timePassed);
            }
        }

        public void AddGraphic(Graphic graphic)
        {
            GraphicList.Add(graphic);
        }

        public double Size
        {
            get { return MaterialObject.Width; }
            set
            {
                MaterialObject.Width = value;
                MaterialObject.Height = value;
            }
        }

        public TwoDimesional AccelerationDirection
        {
            get { return _accelerationVector; }
            set { _accelerationVector = value; }
        }

        public TwoDimesional SpeedVector
        {
            get { return _speedVector;}
            set { _newSpeedVector = value; }
        }
        #endregion

        #region Private
        private TwoDimesional _accelerationVector;
        private TwoDimesional _speedVector, _newSpeedVector;
        #endregion

        #region Protected
        protected PhysicalBaseObject(PhysicalField field, double size, TwoDimesional position, TwoDimesional speedVector)
        {
            _speedVector = speedVector;
            Field = field;
            Position = position;
            PrevPosition = position;
            MaterialObject = Tool.GenerateEllipse(size);
        }

        protected abstract void CustomConduct();
        #endregion

    }
}