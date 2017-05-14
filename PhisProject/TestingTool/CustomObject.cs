using PhysProject.Source;

namespace PhysProject.TestingTool
{
    public class CustomObject : PhysicalBaseObject
    {
        public CustomObject(PhysicalField field, int size, TwoDimesional position, TwoDimesional speedVector) : base(field, size, position, speedVector)
        {
        }

        protected override void CustomConduct()
        {
            TwoDimesional newAcceleration = new TwoDimesional(0, 0);
            foreach (PhysicalBaseObject physicalObject in Field.PhysicalObjects)
            {
                double distance = Tool.Distance(Position, physicalObject.Position);
                if (distance != 0)
                {
                    double a = 50 / distance;
                    double dX = Position.X - physicalObject.Position.X;
                    double dY = Position.Y - physicalObject.Position.Y;
                    dX = dX * a / distance;
                    dY = dY * a / distance;
                    newAcceleration += new TwoDimesional(dX, dY);
                }
            }

            AccelerationDirection = newAcceleration;

            if (Position.Y - Size / 2 < 0)
            {
                TwoDimesional currentMove = SpeedVector;
                currentMove.Y = currentMove.Y * -1;
                SpeedVector = currentMove;
            }
        }
    }
}