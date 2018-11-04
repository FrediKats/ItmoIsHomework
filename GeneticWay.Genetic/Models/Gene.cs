using System;
using System.Diagnostics;

namespace GeneticWay.Genetic.Models
{
    [DebuggerDisplay("{Value}")]
    public struct Gene<T>
    {
        public T Value { get; }

        //TODO: remove func for memory optimization
        private readonly Func<Gene<T>, Gene<T>> _mutationFunc;

        public Gene(T value, Func<Gene<T>, Gene<T>> mutationFuncFunc)
        {
            Value = value;
            _mutationFunc = mutationFuncFunc;
        }

        public Gene<T> CreateMutation()
        {
            return _mutationFunc(this);
        }
    }
}