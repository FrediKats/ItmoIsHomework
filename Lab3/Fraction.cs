using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    sealed class Fraction
    {
        public int Numerator { get; }
        public int Denominator { get; }
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException();
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        private Fraction Reduce()
        {
            var num = Numerator;
            var den = Denominator;

            while (num != 0 && den != 0)
            {
                if (den > num)
                {
                    den %= num;
                }
                else
                {
                    num %= den;
                }
            }

            return new Fraction(Numerator / (num + den), Denominator / (num + den));
        }

        public static Fraction operator -(Fraction fraction)
        {
            return new Fraction(-fraction.Numerator, fraction.Denominator);
        }

        public static Fraction operator +(Fraction left, Fraction right)
        {
            return new Fraction(left.Numerator * right.Denominator + right.Numerator * left.Denominator,
                                left.Denominator * right.Denominator)
                        .Reduce();
        }

        public static Fraction operator -(Fraction left, Fraction right)
        {
            return left + -right;
        }

        public static Fraction operator *(Fraction left, Fraction right)
        {
            return new Fraction(left.Numerator * right.Numerator, left.Denominator * right.Denominator).Reduce();
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            if (right.Numerator == 0)
            {
                throw new ArgumentException();
            }

            return left * new Fraction(right.Denominator, right.Numerator);
        }

        public static implicit operator Fraction(int number)
        {
            return new Fraction(number, 1);
        }

        public static explicit operator int(Fraction fraction)
        {
            return fraction.Numerator / fraction.Denominator;
        }

        public static explicit operator double(Fraction fraction)
        {
            return (double)fraction.Numerator / fraction.Denominator; 
        }

        public override string ToString()
        {
            return $"{{ {Numerator}, {Denominator} }}";
        }
    }
}
