using PhysProject.Source;

namespace PhysProject.TestingTool
{
    public class Balist : PhysicalObject
    {
        public Balist(PhysicalField field, TwoDimesional size, TwoDimesional position, TwoDimesional moveDirection) : base(field, size, position, moveDirection)
        {
        }

        protected override void CustomConduct()
        {
            AccelerationDirection = new TwoDimesional(0, -9.81);
        }
    }
}