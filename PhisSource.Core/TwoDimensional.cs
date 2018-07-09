namespace PhisSource.Core
{
    public class TwoDimensional
    {
        public readonly double X;
        public readonly double Y;

        public TwoDimensional(double x, double y)
        {
            X = x;
            Y = y;
        }

        public TwoDimensional(TwoDimensional td)
        {
            X = td.X;
            Y = td.Y;
        }

        public static TwoDimensional operator +(TwoDimensional f, TwoDimensional s)
        {
            return new TwoDimensional(f.X + s.X, f.Y + s.Y);
        }

        public static TwoDimensional operator -(TwoDimensional f, TwoDimensional s)
        {
            return new TwoDimensional(f.X - s.X, f.Y - s.Y);
        }

        public static TwoDimensional operator *(TwoDimensional td, double value)
        {
            return new TwoDimensional(td.X * value, td.Y * value);
        }

        public static TwoDimensional operator /(TwoDimensional td, double value)
        {
            return new TwoDimensional(td.X / value, td.Y / value);
        }
    }
}