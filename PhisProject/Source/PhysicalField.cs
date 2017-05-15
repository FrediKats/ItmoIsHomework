using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PhysProject.Source
{
    public class PhysicalField
    {
        #region Public
        public readonly List<PhysicalBaseObject> PhysicalObjects = new List<PhysicalBaseObject>();
        public readonly List<PhysicalBaseObject> StaticObjects = new List<PhysicalBaseObject>();
        public readonly Canvas FieldCanvas;
 
        public PhysicalField(Canvas fieldFieldCanvas, int timePerTick)
        {
            FieldCanvas = fieldFieldCanvas;
            _timePerTick = timePerTick;
            _systemTime.Interval = TimeSpan.FromMilliseconds(timePerTick);
            _systemTime.Tick += FieldUpdate;
        }

        public void AddObject(PhysicalBaseObject obj)
        {
            PhysicalObjects.Add(obj);
            FieldCanvas.Children.Add(obj.MatherialObject);
            UpdatePosition(obj);
        }

        public void AddStaticObject(PhysicalBaseObject obj)
        {
            StaticObjects.Add(obj);
            FieldCanvas.Children.Add(obj.MatherialObject);
            UpdatePosition(obj);
        }

        public void Start(object sender, EventArgs e)
        {
            _systemTime.Start();
        }
        #endregion

        #region Private
        private readonly DispatcherTimer _systemTime = new DispatcherTimer();
        private readonly int _timePerTick;

        private void FieldUpdate(object sender, EventArgs e)
        {
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                obj.UpdateMoveDirection(_timePerTick);
            }
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                obj.Position += (obj.SpeedVector * _timePerTick);
                UpdatePosition(obj);
            }
        }

        private void UpdatePosition(PhysicalBaseObject obj)
        {
            Canvas.SetLeft(obj.MatherialObject, obj.Position.X - obj.MatherialObject.Width / 2);
            Canvas.SetBottom(obj.MatherialObject, obj.Position.Y - obj.MatherialObject.Height / 2);
        }
        #endregion
    }
}