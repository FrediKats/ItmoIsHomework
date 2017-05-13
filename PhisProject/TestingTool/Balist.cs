using PhysProject.Source;

namespace PhysProject.TestingTool
{
    public class Balist : PhysicalObject
    {
        private TwoDimesional _stPos;
        public Balist(PhysicalField field, double size, TwoDimesional position, TwoDimesional speedVector) : base(field, size, position, speedVector)
        {
            _stPos = position;
        }

        protected override void CustomConduct()
        {
            Balist b = new Balist(PhysicField, Size, Position, SpeedVector);
            PhysicField.AddStaticObject(b);

            AccelerationDirection = new TwoDimesional(0, -9.81);

            if (Position.Y < 5)
            {
                StopMoving();
            }
        }
    }
}