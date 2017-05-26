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

        public TwoDimesional(TwoDimesional td)
        {
            X = td.X;
            Y = td.Y;
        }

        public static TwoDimesional operator +(TwoDimesional f, TwoDimesional s)
        {
            return new TwoDimesional(f.X + s.X, f.Y + s.Y);
        }

        public static TwoDimesional operator -(TwoDimesional f, TwoDimesional s)
        {
            return new TwoDimesional(f.X - s.X, f.Y - s.Y);
        }

        public static TwoDimesional operator *(TwoDimesional td, double coef)
        {
            return new TwoDimesional(td.X * coef, td.Y * coef);
        }

        public static TwoDimesional operator /(TwoDimesional td, double coef)
        {
            return new TwoDimesional(td.X / coef, td.Y / coef);
        }
    }
}