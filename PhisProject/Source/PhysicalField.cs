using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using PhysProject.TestingTool;

namespace PhysProject.Source
{
    public class PhysicalField
    {
        public readonly List<PhysicalObject> PhysicalObjects = new List<PhysicalObject>();
        public readonly List<PhysicalObject> StaticObjects = new List<PhysicalObject>();
        public readonly Canvas FieldCanvas;

        private readonly DispatcherTimer _systemTime = new DispatcherTimer();
        private readonly int _timePerTick;

        public PhysicalField(Canvas fieldFieldCanvas, int timePerTick)
        {
            FieldCanvas = fieldFieldCanvas;
            _timePerTick = timePerTick;
            _systemTime.Interval = TimeSpan.FromMilliseconds(timePerTick);
            _systemTime.Tick += Action;
        }

        private void Action(object sender, EventArgs e)
        {
            foreach (PhysicalObject obj in PhysicalObjects)
            {
                obj.UpdateMoveDirection(_timePerTick);
            }
            foreach (PhysicalObject obj in PhysicalObjects)
            {
                obj.MoveObject(_timePerTick);
            }
        }

        public void AddObject(PhysicalObject obj)
        {
            PhysicalObjects.Add(obj);
            obj.UpdatePosition();
        }

        public void AddStaticObject(PhysicalObject obj)
        {
            StaticObjects.Add(obj);
            obj.UpdatePosition();
        }

        public void Start(object sender, EventArgs e)
        {
            _systemTime.Start();
        }
        
    }
}