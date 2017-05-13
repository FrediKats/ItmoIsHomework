using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhysProject.Source
{
    public abstract class PhysicalObject
    {
        public readonly PhysicalField PhysicField;
        public readonly Ellipse MatherialObject;
        public TwoDimesional Position;
        
        private TwoDimesional _accelerationVector;
        private TwoDimesional _speedVector, _newSpeedVector;
        

        protected PhysicalObject(PhysicalField field, double size, TwoDimesional position, TwoDimesional speedVector)
        {
            _speedVector = speedVector;
            PhysicField = field;
            Position = position;
            MatherialObject = Tool.GenerateEllipse(size);
            PhysicField.FieldCanvas.Children.Add(MatherialObject);
            UpdatePosition();
        }

        public void StopMoving()
        {
            _speedVector = new TwoDimesional(0, 0);
            _accelerationVector = new TwoDimesional(0, 0);
        }

        public void UpdatePosition()
        {
            Canvas.SetLeft(MatherialObject, Position.X - MatherialObject.Width / 2);
            Canvas.SetBottom(MatherialObject, Position.Y - MatherialObject.Height / 2);
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

        public void MoveObject(int timePassed)
        {
            Position += (_speedVector * timePassed);
            UpdatePosition();
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

        protected abstract void CustomConduct();
    }
}