using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhysProject.Source
{
    public class PhysicalField
    {
        #region Public
        public List<PhysicalBaseObject> PhysicalObjects = new List<PhysicalBaseObject>();
        public readonly Canvas FieldCanvas;
        public int CurrentTime;

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
            FieldCanvas.Children.Add(obj.MaterialObject);
            UpdatePosition(obj);
        }
        
        public void Start(object sender, EventArgs e)
        {
            _systemTime.Start();
        }

        public void Stop(object sender, EventArgs e)
        {
            _systemTime.Stop();
        }

        public void ClearCanvas()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                FieldCanvas.Children.Remove(lines[i]);
            }
            lines = new List<Line>();

            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                FieldCanvas.Children.Remove(obj.MaterialObject);
            }
            PhysicalObjects = new List<PhysicalBaseObject>();
        }
        #endregion

        #region Private
        private readonly DispatcherTimer _systemTime = new DispatcherTimer();
        private readonly int _timePerTick;
        private List<Line> lines = new List<Line>();

        private void FieldUpdate(object sender, EventArgs e)
        {
            CurrentTime = (int) (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                if (obj.TimeAdded == -1)
                {
                    obj.TimeAdded = (int) (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                }
                obj.UpdateMoveDirection(_timePerTick);
            }
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                obj.PrevPosition = new TwoDimesional(obj.Position);
                obj.Position += (obj.SpeedVector * _timePerTick);
                UpdatePosition(obj);
                if (obj.IsWayDraw)
                {
                    Line line = Tool.GenerateLine(obj.PrevPosition, obj.Position);
                    FieldCanvas.Children.Add(line);
                    lines.Add(line);
                }
            }
        }

        private void UpdatePosition(PhysicalBaseObject obj)
        {
            Canvas.SetLeft(obj.MaterialObject, obj.Position.X - obj.MaterialObject.Width / 2);
            Canvas.SetTop(obj.MaterialObject, Config.WindowHeight - obj.Position.Y - obj.MaterialObject.Height / 2);
        }
        #endregion
    }
}