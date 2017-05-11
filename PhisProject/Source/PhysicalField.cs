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
        private readonly DispatcherTimer _systemTime = new DispatcherTimer();
        private readonly List<PhysicalObject> _physicalObjects = new List<PhysicalObject>();
        private readonly List<PhysicalObject> _staticObjects = new List<PhysicalObject>();
        private readonly int _timePerTick;
        private readonly Canvas _canvas;

        public PhysicalField(Canvas fieldCanvas, int timePerTick)
        {
            _canvas = fieldCanvas;
            _timePerTick = timePerTick;
            _systemTime.Interval = TimeSpan.FromMilliseconds(timePerTick);
            _systemTime.Tick += Action;
        }

        private void Action(object sender, EventArgs e)
        {
            foreach (PhysicalObject obj in _physicalObjects)
            {
                obj.UpdateMoveDirection(_timePerTick);
            }
            foreach (PhysicalObject obj in _physicalObjects)
            {
                obj.MoveObject(_timePerTick);
            }
        }

        public void AddObject(PhysicalObject obj)
        {
            _physicalObjects.Add(obj);
        }

        public void AddStaticObject(PhysicalObject obj)
        {
            _staticObjects.Add(obj);
            obj.UpdatePosition();
        }

        public void Start(object sender, EventArgs e)
        {
            _systemTime.Start();
        }

        public List<PhysicalObject> PhysicalObjects => _physicalObjects;
        public List<PhysicalObject> StaticObjects => _staticObjects;
        public Canvas FieldCanvas => _canvas;
    }
}