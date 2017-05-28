using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using OxyPlot.Series;

namespace PhysProject.Source
{
    public abstract class PhysicalBaseObject
    {
        #region Public
        //public readonly List<CustomChart> GraphicList = new List<CustomChart>();

        public List<CustomSeries> SeriesList = new List<CustomSeries>();
        public readonly PhysicalField Field;
        public readonly Ellipse MaterialObject;
        public TextBlock InfoBlock;

        public TwoDimesional PrevPosition;
        public TwoDimesional AccelerationDirection;

        public bool IsStoped = false;
        public bool IsWayDraw = false;
        public int SelfTime = 0;

        public void StopMoving()
        {
            _speedVector = new TwoDimesional(0, 0);
            AccelerationDirection = new TwoDimesional(0, 0);
            IsStoped = true;
        }

        public void UpdateMoveDirection(int timePassed)
        {
            if (IsStoped)
            {
                return;
            }

            SelfTime += timePassed;
            _newSpeedVector = null;
            CustomConduct();

            if (_newSpeedVector != null)
            {
                _speedVector = _newSpeedVector;
            }
            if (AccelerationDirection != null)
            {
                _speedVector += (AccelerationDirection * timePassed / 1000);
            }
        }

        public void AddSeries(CustomChart chart)
        {
            SeriesList.Add(new CustomSeries(chart));
        }

        public void UpdatePosition()
        {
            Canvas.SetLeft(MaterialObject, Position.X - MaterialObject.Width / 2);
            Canvas.SetTop(MaterialObject, Position.Y - MaterialObject.Height / 2);
            CustomPositionUpdate();

        }

        public TwoDimesional Position
        {
            get { return _position; }
            set
            {
                _position = value;
                UpdatePosition();
            }
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

        public TwoDimesional SpeedVector
        {
            get { return _speedVector; }
            set { _newSpeedVector = value; }
        }
        #endregion

        #region Private
        private TwoDimesional _speedVector, _newSpeedVector;
        private TwoDimesional _position;
        #endregion

        #region Protected
        protected PhysicalBaseObject(PhysicalField field, double size, TwoDimesional position, TwoDimesional speedVector)
        {
            MaterialObject = Tool.GenerateEllipse(size);
            Field = field;
            Position = position;
            PrevPosition = position;
            _speedVector = speedVector;
        }

        protected abstract void CustomConduct();

        protected virtual void CustomPositionUpdate()
        {
            return;
        }
        #endregion

    }
}