using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.Models
{
    public class CountableMultiDimensionalFunc
    {
        public CountableMultiDimensionalFunc(Func<double[], double> function, double[] startPoint, double functionEpsilon, double[] parameterEpsilon)
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

        private readonly Func<double[], double> _function;
        private int _callCount;
        private int _iterationCount;
        public Func<double[], double> Function
        {
            get
            {
                _callCount++;
                return _function;
            }
        }

        public int IterationCount => _iterationCount;
        public int CallCount => _callCount;
        public double[] StartPoint { get; set; }
        public double FunctionEpsilon { get; }
        public double[] ParameterEpsilon { get; }


    }
}