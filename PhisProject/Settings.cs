using System;

namespace PhisProject
{
    public class Settings
    {
        public const double G = 209.81;
        public const int timePerTick = 30;
        public const double L = 100;
        public const double PI = 3.1415;
        public static double W = Math.Sqrt(G / L);
    }
}