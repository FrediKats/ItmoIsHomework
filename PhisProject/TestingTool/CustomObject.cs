using PhysProject.Source;

namespace PhysProject.TestingTool
{
    public class CustomObject : PhysicalObject
    {
        public CustomObject(PhysicalField field, TwoDimesional size, TwoDimesional position, TwoDimesional moveDirection) : base(field, size, position, moveDirection)
        {
        }

        protected override void SetAccelerationVector()
        {
            if (Position.Y >= 290)
            {
                StopMoving();
                return;
            }
            AccelerationDirection = new TwoDimesional(10, 7);
        }
    }
}