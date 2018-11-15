using System;

namespace Lab3
{
    public struct Fraction : IComparable<Fraction>
    {
        public int Numerator { get; }
        public int Denominator { get; }

        public Fraction(int numerator, int denominator = 1)
        {
            if (denominator == 0)
            {
                throw new ArgumentException();
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        //TODO: write cast from tuple

        public int CompareTo(Fraction other)
        {
            return Math.Sign((double)(this - other));
        }

        public Fraction Reduce()
        {
            var absNum = Math.Abs(Numerator);
            var absDen = Math.Abs(Denominator);

            while (absNum != 0 && absDen != 0)
            {
                if (absDen > absNum)
                {
                    absDen %= absNum;
                }
                else
                {
                    absNum %= absDen;
                }
            }

            var gcd = Math.Sign(Denominator) * (absNum + absDen);
            return new Fraction(Numerator / gcd, Denominator / gcd);
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
            return left * new Fraction(right.Denominator, right.Numerator);
        }

        public static bool operator ==(Fraction left, Fraction right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return !left.Equals(right);
        }

        public static bool operator < (Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator > (Fraction left, Fraction right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <= (Fraction left, Fraction right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >= (Fraction left, Fraction right)
        {
            return left.CompareTo(right) >= 0;
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
            return $"{Numerator}{(Denominator != 1 ? "/" + Denominator : string.Empty)}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                var reducedThis = Reduce();
                var reducedAnother = other.Reduce();

                return reducedThis.Numerator == reducedAnother.Numerator
                    && reducedThis.Denominator == reducedAnother.Denominator;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return int.Parse(Numerator.ToString() + Denominator.ToString());
        }
    }
}
