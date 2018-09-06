using System;
using System.Collections.Generic;

namespace Lab1.Tools
{
    public class CountableMultiDimensionalFunc
    {
        public CountableMultiDimensionalFunc(ScalarField<double> function, List<double> startPoint, double functionEpsilon, List<double> parameterEpsilon)
        {
            _function = function;
            StartPoint = startPoint;
            FunctionEpsilon = functionEpsilon;
            ParameterEpsilon = parameterEpsilon;
        }

        private readonly ScalarField<double> _function;
        private int _callCount;

        public ScalarField<double> Function
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