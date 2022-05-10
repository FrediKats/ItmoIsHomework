using System;

namespace Lab1.Tools
{
    public class FiboGenerator
    {
        private readonly int[] _fibNumbers;
        public readonly int Last;

        public FiboGenerator(double left, double right, double epsilon)
        {
            int maxCount = (int) Math.Ceiling((right - left) / epsilon);
            maxCount = Math.Abs(maxCount);
            if (maxCount != 0)
            {
                Last = (int) Math.Round((Math.Log(maxCount) + Math.Log(5) / 2) / Math.Log(Constants.PHI));
                Last--;
            }
            else
            {
                Last = 0;
            }

            _fibNumbers = new int[Last + 1];
            _fibNumbers[1] = 1;

            for (int i = 2; i <= Last; i++)
            {
                _fibNumbers[i] = _fibNumbers[i - 1] + _fibNumbers[i - 2];
            }
        }

        public int this[int i]
        {
            get => _fibNumbers[i];
        }
    }
}