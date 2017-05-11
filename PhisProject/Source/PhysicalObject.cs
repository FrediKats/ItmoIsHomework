using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhysProject.Source
{
    public abstract class PhysicalObject
    {
        private readonly PhysicalField _physicField;
        private readonly Ellipse _matherialObject;
        private readonly int _size;

        private TwoDimesional _accelerationVector;
        private TwoDimesional _speedVector, _newSpeedVector;
        private TwoDimesional _position;

        protected PhysicalObject(PhysicalField field, int size,
            TwoDimesional position, TwoDimesional speedVector)
        {
            _speedVector = speedVector;
            _physicField = field;
            _position = position;
            _size = size;
            _matherialObject = new Ellipse()
            {
                Height = size,
                Width = size,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush() { Color = Colors.Navy },
                Fill = new SolidColorBrush() { Color = Colors.Yellow }
            };
            _physicField.FieldCanvas.Children.Add(_matherialObject);
            UpdatePosition();
        }

        public void StopMoving()
        {
            _speedVector = new TwoDimesional(0, 0);
            _accelerationVector = new TwoDimesional(0, 0);
        }

        public void UpdatePosition()
        {
            Canvas.SetLeft(_matherialObject, _position.X - _matherialObject.Width / 2);
            Canvas.SetBottom(_matherialObject, _position.Y - _matherialObject.Height / 2);
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
            _position += (_speedVector * timePassed);
            UpdatePosition();
        }

        public Ellipse MatherialObject => _matherialObject;
        public PhysicalField Field => _physicField;
        public TwoDimesional Position => _position;
        public int Size => _size;

        public TwoDimesional AccelerationDirection
        {
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