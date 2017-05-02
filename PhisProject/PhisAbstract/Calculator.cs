using System;

namespace PhisProject.PhisAbstract
{
    public class Calculator
    {
        private static double PI = 3.1415;
        public static double getObjectPositionX(double w, double t, double phi0, double L)
        {
            double phi = w * t / 1000;
            double PhiR = PI / 2 - phi0;
            while (phi >= 4 * PhiR)
            {
                phi -= 4 * PhiR;
            }
            if (phi >= PhiR * 2)
            {
                phi = PhiR * 4 - phi;
            }
            return L * Math.Cos(phi + phi0);
        }

        public static double getObjectPositionY(double w, double t, double phi0, double L)
        {
            double phi = w * t / 1000;
            double PhiR = PI / 2 - phi0;
            while (phi >= 4 * PhiR)
            {
                phi -= 4 * PhiR;
            }
            if (phi >= PhiR * 2)
            {
                phi = PhiR * 4 - phi;
            }
            return L * Math.Sin(phi + phi0);

        }
    }
}