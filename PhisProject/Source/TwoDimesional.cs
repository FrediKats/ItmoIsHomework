namespace PhysProject.Source
{
    public class TwoDimesional
    {
        public double X, Y;

        public TwoDimesional(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static TwoDimesional operator +(TwoDimesional f, TwoDimesional s)
        {
            return new TwoDimesional(f.X + s.X, f.Y + s.Y);
        }

        public static TwoDimesional operator *(TwoDimesional td, int coef)
        {
            return new TwoDimesional(td.X * coef / 1000, td.Y * coef / 1000);
        }
    }
}