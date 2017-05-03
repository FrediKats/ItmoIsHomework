using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhysProject.Source
{
    public abstract class PhysicalObject
    {
        private PhysicalField _physicField;
        private Ellipse _objectEllipse;
        private TwoDimesional _accelerationDirection;
        private TwoDimesional _moveDirection;
        private TwoDimesional _position;

        protected PhysicalObject(PhysicalField field, TwoDimesional size,
            TwoDimesional position, TwoDimesional moveDirection)
        {
            _physicField = field;
            _moveDirection = moveDirection;
            _position = position;
            _objectEllipse = new Ellipse()
            {
                Height = size.Y,
                Width = size.X,
                StrokeThickness = 2,
                Stroke = new SolidColorBrush() { Color = Colors.Black },
                Fill = new SolidColorBrush() { Color = Colors.Red }
            };
            _physicField.FieldCanvas.Children.Add(_objectEllipse);
            UpdatePosition();
        }

        protected void StopMoving()
        {
            _moveDirection = new TwoDimesional(0, 0);
            _accelerationDirection = new TwoDimesional(0, 0);
        }

        private void UpdatePosition()
        {
            Canvas.SetLeft(_objectEllipse, _position.X - _objectEllipse.Width / 2);
            Canvas.SetTop(_objectEllipse, 400 - (_position.Y - _objectEllipse.Height / 2));
        }

        public void UpdateMoveDirection(int timePassed)
        {
            SetAccelerationVector();
            _moveDirection += (_accelerationDirection * timePassed);
        }

        public void MoveObject(int timePassed)
        {
            _position += (_moveDirection * timePassed);
            UpdatePosition();
        }

        public Ellipse ObjectEllipse => _objectEllipse;
        public TwoDimesional Position => _position;
        public TwoDimesional AccelerationDirection
        {
            set { _accelerationDirection = value; }
        }

        protected abstract void SetAccelerationVector();
    }
}