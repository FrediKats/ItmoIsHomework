using System;

namespace PhysProject.Source
{
    public class Tool
    {
        public static double Distance(TwoDimesional f, TwoDimesional s)
        {
            return  Math.Sqrt(Math.Pow(f.X - s.X, 2) + Math.Pow(f.Y - s.Y, 2));
        }
    }
}