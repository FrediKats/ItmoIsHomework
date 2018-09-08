using System;
using System.Collections.Generic;

namespace Lab1.Tools
{
    public class CountableMultiDimensionalFunc
    {
        public CountableMultiDimensionalFunc(Func<double[], double> function, List<double> startPoint, double functionEpsilon, List<double> parameterEpsilon)
        {
            _function = function;
            StartPoint = startPoint;
            FunctionEpsilon = functionEpsilon;
            ParameterEpsilon = parameterEpsilon;
        }

        private readonly Func<double[], double> _function;
        private int _callCount;

        public Func<double[], double> Function
        {
            get
            {
                _callCount++;
                return _function;
            }
        }

        public int CallCount => _callCount;
        public List<double> StartPoint { get; }
        public double FunctionEpsilon { get; }
        public List<double> ParameterEpsilon { get; }
    }
}