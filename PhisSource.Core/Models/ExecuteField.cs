using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PhisSource.Core.Models
{
    public class ExecuteField
    {
        public readonly List<PhysicalBaseObject> PhysicalObjects = new List<PhysicalBaseObject>();
        public readonly Canvas FieldCanvas;

        public ExecuteField(int timePerTick, Canvas fieldCanvas)
        {
            FieldCanvas = fieldCanvas;
            _timePerTick = timePerTick;

            _systemTime.Interval = TimeSpan.FromMilliseconds(timePerTick);
            _systemTime.Tick += FieldUpdate;
        }

        public void AddObject(PhysicalBaseObject obj)
        {
            PhysicalObjects.Add(obj);

            //TODO: OnChildrenAdded
            FieldCanvas.Children.Add(obj.MaterialObject);

            //TODO: Event OnPositionChanged
            obj.UpdatePosition();
        }

        public void Start()
        {
            _systemTime.Start();
        }

        public void Stop()
        {
            _systemTime.Stop();
        }

        public void ClearCanvas()
        {
            //TODO: OnChildrenRemove
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                FieldCanvas.Children.Remove(obj.MaterialObject);
            }
            PhysicalObjects.Clear();
        }

        //TODO: use Stopwatch /Или нет?
        private readonly DispatcherTimer _systemTime = new DispatcherTimer();
        private readonly int _timePerTick;

        private void FieldUpdate(object sender, EventArgs e)
        {
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                obj.UpdateMoveDirection(_timePerTick);
            }
        }
    }
}