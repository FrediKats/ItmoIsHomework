using System;

namespace Lab1.Models
{
    public class CountableMultiDimensionalFunc
    {
        public CountableMultiDimensionalFunc(Func<Dimensions, double> function, Dimensions startPoint, double functionEpsilon, Dimensions parameterEpsilon)
        {
            _function = function;
            StartPoint = startPoint;
            FunctionEpsilon = functionEpsilon;
            ParameterEpsilon = parameterEpsilon;
        }

        public void IncIteration()
        {
            _iterationCount++;
        }

        private readonly Func<Dimensions, double> _function;
        private int _callCount;
        private int _iterationCount;
        public Func<Dimensions, double> Function
        {
            get
            {
                _callCount++;
                return _function;
            }
        }

        public int IterationCount => _iterationCount;
        public int CallCount => _callCount;
        public Dimensions StartPoint { get; set; }
        public double FunctionEpsilon { get; }
        public Dimensions ParameterEpsilon { get; }


    }
}