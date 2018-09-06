using System;

namespace Lab1.Tools
{
    public class CountableFunc
    {
        public CountableFunc(Func<double, double> function, double left, double right, double epsilon)
        {
            _function = function;
            Left = left;
            Right = right;
            Epsilon = epsilon;

            _callCount = 0;
        }

        private readonly Func<double, double> _function;
        private int _callCount;

        public Func<double, double> Function
        {
            get
            {
                _callCount++;
                return _function;
            }
        }

        public int CallCount => _callCount;
        public double Left { get; }
        public double Right { get; }
        public double Epsilon { get; }

    }
}