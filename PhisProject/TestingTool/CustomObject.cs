using PhysProject.Source;

namespace PhysProject.TestingTool
{
    public class CustomObject : PhysicalObject
    {
        public CustomObject(PhysicalField field, TwoDimesional size, TwoDimesional position, TwoDimesional moveDirection) : base(field, size, position, moveDirection)
        {
        }

        protected override void CustomConduct()
        {
            foreach (PhysicalObject physicalObject in Field.PhysicalObjects)
            {
                double distance = Tool.Distance(Position, physicalObject.Position);
                if (distance != 0)
                {
                    double a = 50 / distance;
                    double dX = Position.X - physicalObject.Position.X;
                    double dY = Position.Y - physicalObject.Position.Y;
                    dX = dX * a / distance;
                    dY = dY * a / distance;
                    AccelerationDirection = new TwoDimesional(dX, dY);
                }
            }

            if (Position.Y - Size.Y / 2 < 0)
            {
                TwoDimesional currentMove = MoveDirection;
                currentMove.Y = currentMove.Y * -1;
                MoveDirection = currentMove;
            }
        }
    }
}