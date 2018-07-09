using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using PhisSource.Core.Tools;

namespace PhisSource.Core
{
    public class ExecuteField
    {
        public List<PhysicalBaseObject> PhysicalObjects = new List<PhysicalBaseObject>();
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
            //TODO: Draw direction in PhisObject
            foreach (Line l in _lines)
            {
                FieldCanvas.Children.Remove(l);
            }
            _lines = new List<Line>();

            //TODO: OnChildrenRemove
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                FieldCanvas.Children.Remove(obj.MaterialObject);
            }
            PhysicalObjects = new List<PhysicalBaseObject>();
        }

        //TODO: use Stopwatch
        private readonly DispatcherTimer _systemTime = new DispatcherTimer();
        private readonly int _timePerTick;
        //TODO: ??
        private List<Line> _lines = new List<Line>();

        private void FieldUpdate(object sender, EventArgs e)
        {
            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                obj.UpdateMoveDirection(_timePerTick);
            }

            foreach (PhysicalBaseObject obj in PhysicalObjects)
            {
                var lastPosition = obj.PositionList.Last();
                //TODO: /1000 ??
                var currentPosition = lastPosition + (obj.SpeedVector * _timePerTick / 1000);
                //obj.PrevPosition = new TwoDimensional(obj.PositionList.Last());
                obj.PositionList.Add(currentPosition);

                if (obj.IsWayDraw)
                {
                    Line line = Generator.GenerateLine(lastPosition, currentPosition);
                    FieldCanvas.Children.Add(line);
                    _lines.Add(line);
                }
            }
        }
    }
}