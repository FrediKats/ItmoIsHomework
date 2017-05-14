using System.Windows.Shapes;

namespace PhysProject.Source
{
    public abstract class PhysicalBaseObject
    {
        #region Public
        public readonly PhysicalField Field;
        public readonly Ellipse MatherialObject;
        public TwoDimesional Position;

        public void StopMoving()
        {
            _speedVector = new TwoDimesional(0, 0);
            _accelerationVector = new TwoDimesional(0, 0);
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

        public double Size
        {
            get { return MatherialObject.Width; }
            set
            {
                MatherialObject.Width = value;
                MatherialObject.Height = value;
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
            MatherialObject = Tool.GenerateEllipse(size);
        }

        protected abstract void CustomConduct();
        #endregion

    }
}