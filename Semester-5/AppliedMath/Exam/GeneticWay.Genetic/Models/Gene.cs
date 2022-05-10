using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneticWay.Genetic.Models
{
    [DebuggerDisplay("{Value}")]
    public struct Gene<T> : IEquatable<Gene<T>>
    {
        public T Value { get; }

        public Gene(T value)
        {
            Value = value;
        }

        public bool Equals(Gene<T> other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Gene<T> other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : string.Empty;
        }
    }
}